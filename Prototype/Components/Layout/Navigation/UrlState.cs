// Services/UrlState.cs
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Utils;

namespace Services
{
    /// <summary>
    /// Centralized URL/query manager:
    /// - Keeps a canonical query snapshot
    /// - Prevents feedback loops when we write to the URL
    /// - Exposes BuildHref/Set/Navigate helpers
    /// - Broadcasts a Changed event
    /// </summary>
    public sealed class UrlState : IUrlState, IDisposable
    {
        private readonly NavigationManager _nav;
        private Dictionary<string, string?> _current = new(StringComparer.OrdinalIgnoreCase);
        private bool _inFlight;

        public UrlState(NavigationManager nav)
        {
            _nav = nav;
            _current = QueryStringMap.Read(_nav);
            _nav.LocationChanged += OnLocationChanged;
        }

        public IReadOnlyDictionary<string, string?> Current => _current;

        public event Action? Changed;

        public string BuildHref(string baseHref, IReadOnlyDictionary<string, string?>? overrides = null)
        {
            // Split baseHref into path and existing query; merge in Current + overrides.
            var path = baseHref;
            var merged = new Dictionary<string, string?>(_current, StringComparer.OrdinalIgnoreCase);

            if (baseHref.Contains('?', StringComparison.Ordinal))
            {
                var parts = baseHref.Split('?', 2);
                path = parts[0];
                var qsDict = QueryHelpers.ParseQuery("?" + parts[1])
                    .ToDictionary(kv => kv.Key, kv => kv.Value.LastOrDefault(), StringComparer.OrdinalIgnoreCase);
                foreach (var kv in qsDict)
                    merged[kv.Key] = kv.Value;
            }

            if (overrides is not null)
            {
                foreach (var kv in overrides)
                {
                    if (string.IsNullOrEmpty(kv.Value)) merged.Remove(kv.Key);
                    else merged[kv.Key] = kv.Value!;
                }
            }

            var qs = merged.Count == 0 ? "" : QueryHelpers.AddQueryString("", merged!);
            return string.IsNullOrEmpty(qs) ? path : path + qs;
        }

        public void Set(params (string key, string? value)[] overrides)
        {
            if (overrides == null || overrides.Length == 0) return;
            var dict = overrides.ToDictionary(t => t.key, t => t.value, StringComparer.OrdinalIgnoreCase);
            WriteMerge(dict);
        }

        public void Navigate(string path, IReadOnlyDictionary<string, string?>? overrides = null)
        {
            var href = BuildHref(path, overrides);
            SafeNavigate(href, replace: false);
        }

        private void WriteMerge(IReadOnlyDictionary<string, string?> overrides)
        {
            if (_inFlight) return;
            try
            {
                _inFlight = true;
                QueryStringMap.MergeWrite(_nav, overrides, replace: true);
                // _current will refresh on LocationChanged -> we do not set it here
            }
            finally
            {
                _inFlight = false;
            }
        }

        private void SafeNavigate(string href, bool replace)
        {
            if (_inFlight) return;
            try
            {
                _inFlight = true;
                _nav.NavigateTo(href, replace);
            }
            finally
            {
                _inFlight = false;
            }
        }

        private void OnLocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            _current = QueryStringMap.Read(_nav);
            Changed?.Invoke();
        }

        public void Dispose()
        {
            _nav.LocationChanged -= OnLocationChanged;
        }
    }
}
