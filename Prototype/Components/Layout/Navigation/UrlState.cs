// Services/UrlState.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;

namespace Services
{
    /// <summary>
    /// IUrlState implementation with debounced, circuit-safe navigation.
    /// Centralizes URL operations to eliminate duplication across components.
    /// </summary>
    public sealed class UrlState : IUrlState, IDisposable
    {
        private readonly NavigationManager _nav;
        private readonly Dictionary<string, string?> _current;
        private readonly object _gate = new();

        private CancellationTokenSource? _pendingCts;
        private Dictionary<string, string?>? _pendingQuery;
        private string? _pendingPathOverride;
        private bool _pendingReplaceHistory;
        private SynchronizationContext? _pendingSyncCtx;
        private bool _disposed;

        public UrlState(NavigationManager nav)
        {
            _nav = nav;
            _current = ReadQuerySnapshot(nav);
            _nav.LocationChanged += OnLocationChanged;
        }

        public IReadOnlyDictionary<string, string?> Current => _current;
        public event Action? Changed;

        // ===== Core IUrlState Operations =====

        /// <summary>
        /// Build an href by optionally starting from current query state, applying a base path/query,
        /// and then applying overrides. If navigation crosses the first path segment (module),
        /// current state is ignored to prevent filter leakage.
        /// </summary>
        public string BuildHref(
            string baseHref,
            IReadOnlyDictionary<string, string?>? overrides = null,
            bool preserveCurrentState = true)
        {
            if (string.IsNullOrWhiteSpace(baseHref))
                baseHref = "/";

            var cut = baseHref.IndexOfAny(new[] { '?', '#' });
            var pathOnly = cut >= 0 ? baseHref[..cut] : baseHref;
            pathOnly = pathOnly.StartsWith("/") ? pathOnly : "/" + pathOnly.TrimStart('/');

            // Detect cross-module nav (root segment change)
            var baseRoot = pathOnly.Split('/', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            var currentRoot = _nav.Uri.Split('/', StringSplitOptions.RemoveEmptyEntries).Skip(3).FirstOrDefault(); // skip scheme://host/
            var crossModule = !string.Equals(baseRoot, currentRoot, StringComparison.OrdinalIgnoreCase);

            var merged = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

            // Preserve current query only when staying within same root
            if (preserveCurrentState && !crossModule)
            {
                foreach (var kv in _current)
                    merged[kv.Key] = kv.Value;
            }

            // Parse query from baseHref (takes priority over preserved current)
            if (cut >= 0 && baseHref[cut] == '?')
            {
                var provided = QueryHelpers.ParseQuery(baseHref[cut..]);
                foreach (var kv in provided)
                    merged[kv.Key] = kv.Value.LastOrDefault();
            }

            // ---- ALWAYS-PRESERVE dealer ACROSS MODULES  ----
            if (_current.TryGetValue("dealer", out var dealerVal) && !string.IsNullOrWhiteSpace(dealerVal))
            {
                // Only set if nothing has set/overridden it yet (null/empty counts as "not set")
                if (!merged.TryGetValue("dealer", out var existing) || string.IsNullOrWhiteSpace(existing))
                {
                    merged["dealer"] = dealerVal;
                }
            }

            // Apply explicit overrides (highest priority — can change or remove dealer)
            if (overrides is not null)
            {
                foreach (var kv in overrides)
                {
                    if (kv.Value is null) merged.Remove(kv.Key);
                    else merged[kv.Key] = kv.Value;
                }
            }

            var qs = BuildQuery(merged);
            return string.IsNullOrEmpty(qs) ? pathOnly : $"{pathOnly}?{qs}";
        }


        public void Set(params (string key, string? value)[] overrides)
        {
            if (_disposed || overrides.Length == 0) return;

            var updates = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
            foreach (var (k, v) in overrides)
                updates[k] = v;

            Update(updates);
        }

        public void Navigate(string path, IReadOnlyDictionary<string, string?>? overrides = null)
        {
            if (_disposed) return;
            var syncCtx = SynchronizationContext.Current;

            var pathOnly = NormalizePathOnly(path);
            var target = new Dictionary<string, string?>(_current, StringComparer.OrdinalIgnoreCase);

            if (overrides is not null)
            {
                foreach (var kv in overrides)
                {
                    if (kv.Value is null) target.Remove(kv.Key);
                    else target[kv.Key] = kv.Value;
                }
            }

            ScheduleNavigation(query: target, pathOverride: pathOnly, replaceHistory: false, callerSyncCtx: syncCtx);
        }

        // ===== Convenience Helpers =====

        public string? Get(string key) => _current.TryGetValue(key, out var value) ? value : null;

        public bool Has(string key) => _current.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value);

        public IReadOnlyList<string> GetMulti(string key, string separator = ",")
        {
            if (!_current.TryGetValue(key, out var value) || string.IsNullOrWhiteSpace(value))
                return Array.Empty<string>();

            return value.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        public void Toggle(string key, string value, string separator = ",")
        {
            if (_disposed || string.IsNullOrWhiteSpace(value)) return;

            var current = GetMulti(key, separator);
            var list = current.ToList();

            if (list.Contains(value, StringComparer.OrdinalIgnoreCase))
                list.RemoveAll(v => string.Equals(v, value, StringComparison.OrdinalIgnoreCase));
            else
                list.Add(value);

            var newValue = list.Count > 0 ? string.Join(separator, list) : null;
            Set((key, newValue));
        }

        public void Remove(params string[] keys)
        {
            if (_disposed || keys.Length == 0) return;

            var updates = keys.ToDictionary(k => k, k => (string?)null, StringComparer.OrdinalIgnoreCase);
            Update(updates);
        }

        public void Update(IReadOnlyDictionary<string, string?> updates)
        {
            if (_disposed) return;
            var syncCtx = SynchronizationContext.Current;

            var target = new Dictionary<string, string?>(_current, StringComparer.OrdinalIgnoreCase);
            foreach (var kv in updates)
            {
                if (kv.Value is null) target.Remove(kv.Key);
                else target[kv.Key] = kv.Value;
            }

            if (SameQuery(_current, target))
                return;

            ScheduleNavigation(query: target, pathOverride: null, replaceHistory: true, callerSyncCtx: syncCtx);
        }

        // ===== Internal Navigation Logic =====

        private void ScheduleNavigation(Dictionary<string, string?> query, string? pathOverride, bool replaceHistory, SynchronizationContext? callerSyncCtx)
        {
            CancellationToken token;

            lock (_gate)
            {
                _pendingCts?.Cancel();
                _pendingCts?.Dispose();
                _pendingCts = new CancellationTokenSource();
                token = _pendingCts.Token;

                _pendingQuery = query;
                _pendingPathOverride = pathOverride;
                _pendingReplaceHistory = replaceHistory;
                _pendingSyncCtx = callerSyncCtx;
            }

            _ = NavigateDebouncedAsync(token);
        }

        private async Task NavigateDebouncedAsync(CancellationToken token)
        {
            try
            {
                await Task.Yield();
                token.ThrowIfCancellationRequested();

                Dictionary<string, string?> query;
                string? pathOverride;
                bool replaceHistory;
                SynchronizationContext? syncCtx;

                lock (_gate)
                {
                    query = _pendingQuery ?? new(StringComparer.OrdinalIgnoreCase);
                    pathOverride = _pendingPathOverride;
                    replaceHistory = _pendingReplaceHistory;
                    syncCtx = _pendingSyncCtx;
                }

                var pathOnly = pathOverride ?? GetCurrentPathOnly(_nav);
                var qs = BuildQuery(query);
                var href = string.IsNullOrEmpty(qs) ? pathOnly : $"{pathOnly}?{qs}";

                token.ThrowIfCancellationRequested();

                var opts = new NavigationOptions
                {
                    ReplaceHistoryEntry = replaceHistory,
                    ForceLoad = false
                };

                void NavigateAction(object? _) => _nav.NavigateTo(href, opts);

                if (syncCtx is not null)
                    syncCtx.Post(NavigateAction, null);
                else
                    _nav.NavigateTo(href, opts);
            }
            catch (OperationCanceledException) { }
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (_disposed) return;

            var next = ReadQuerySnapshot(_nav);

            _current.Clear();
            foreach (var kv in next)
                _current[kv.Key] = kv.Value;

            Changed?.Invoke();
        }

        // ===== Utilities =====

        private static Dictionary<string, string?> ReadQuerySnapshot(NavigationManager nav)
        {
            var abs = nav.ToAbsoluteUri(nav.Uri);
            var parsed = QueryHelpers.ParseQuery(abs.Query);
            var dict = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
            foreach (var kv in parsed)
                dict[kv.Key] = kv.Value.LastOrDefault();
            return dict;
        }

        private static string NormalizePathOnly(string hrefOrPath)
        {
            var cut = hrefOrPath.IndexOfAny(new[] { '?', '#' });
            var basePart = cut >= 0 ? hrefOrPath[..cut] : hrefOrPath;
            return basePart.StartsWith("/") ? basePart : "/" + basePart.TrimStart('/');
        }

        private static string GetCurrentPathOnly(NavigationManager nav)
        {
            var abs = nav.ToAbsoluteUri(nav.Uri);
            var rel = nav.ToBaseRelativePath(abs.ToString());
            var cut = rel.IndexOfAny(new[] { '?', '#' });
            return cut >= 0 ? "/" + rel[..cut].TrimStart('/') : "/" + rel.TrimStart('/');
        }

        private static bool SameQuery(Dictionary<string, string?> a, Dictionary<string, string?> b)
        {
            if (a.Count != b.Count) return false;
            foreach (var kv in a)
            {
                if (!b.TryGetValue(kv.Key, out var bv)) return false;
                if (!string.Equals(kv.Value, bv, StringComparison.Ordinal)) return false;
            }
            return true;
        }

        private static string BuildQuery(Dictionary<string, string?> map)
        {
            if (map.Count == 0) return string.Empty;

            var qp = new List<string>(map.Count);
            foreach (var kv in map.OrderBy(k => k.Key, StringComparer.OrdinalIgnoreCase))
            {
                if (kv.Value is null) continue;
                var k = Uri.EscapeDataString(kv.Key);
                var v = Uri.EscapeDataString(kv.Value);
                qp.Add($"{k}={v}");
            }
            return string.Join("&", qp);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            _nav.LocationChanged -= OnLocationChanged;

            lock (_gate)
            {
                _pendingCts?.Cancel();
                _pendingCts?.Dispose();
                _pendingCts = null;
                _pendingQuery = null;
                _pendingPathOverride = null;
                _pendingSyncCtx = null;
            }
        }
    }
}
