// BlazorApp1/Services/UrlState.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Prototype.Components.Layout.Navigation
{
    public static class UrlState
    {
        public static string CurrentPath(NavigationManager nav)
        {
            var abs = nav.ToAbsoluteUri(nav.Uri);
            var rel = nav.ToBaseRelativePath(abs.ToString());
            // Strip query/fragment from the relative URL, keep just the path
            var qIndex = rel.IndexOf('?', StringComparison.Ordinal);
            var hIndex = rel.IndexOf('#', StringComparison.Ordinal);
            var cut = new[] { qIndex, hIndex }.Where(i => i >= 0).DefaultIfEmpty(rel.Length).Min();
            return rel[..cut].TrimStart('/');
        }

        public static IDictionary<string, string?> GetQuery(NavigationManager nav)
        {
            var abs = nav.ToAbsoluteUri(nav.Uri);
            var query = QueryHelpers.ParseQuery(abs.Query);
            // Normalize to dictionary<string,string?>
            return query.ToDictionary(k => k.Key, v => v.Value.LastOrDefault());
        }

        /// <summary>
        /// Build a URL to the given base-relative targetPath, keeping only the specified query keys (if present).
        /// targetPath can be absolute (starts with "/") or base-relative ("po-transfer/...").
        /// </summary>
        public static string WithPathKeepingQuery(NavigationManager nav, string targetPath, params string[] keepKeys)
        {
            var path = NormalizeTargetPath(targetPath);
            var currentQuery = GetQuery(nav);
            var keep = new Dictionary<string, string?>();
            foreach (var k in keepKeys.Distinct(StringComparer.OrdinalIgnoreCase))
            {
                if (currentQuery.TryGetValue(k, out var v)) keep[k] = v;
            }

            if (keep.Count == 0) return path;

            var uri = new UriBuilder { Path = path, Query = BuildQueryString(keep) };
            // Strip leading '/' from Path in some hosts; NavigationManager expects relative paths for NavigateTo
            return uri.Path.TrimStart('/') + (string.IsNullOrEmpty(uri.Query) ? "" : uri.Query);
        }

        /// <summary>
        /// Return the current path with updated query (added/replaced) and optional removals.
        /// </summary>
        public static string UpdateQuery(NavigationManager nav, IDictionary<string, string?> updates, params string[] removeKeys)
        {
            var path = CurrentPath(nav);
            var q = GetQuery(nav);

            foreach (var k in removeKeys.Distinct(StringComparer.OrdinalIgnoreCase))
                q.Remove(q.Keys.FirstOrDefault(x => string.Equals(x, k, StringComparison.OrdinalIgnoreCase)));

            foreach (var kv in updates)
            {
                // Null or empty removes the key
                if (string.IsNullOrWhiteSpace(kv.Value))
                {
                    q.Remove(q.Keys.FirstOrDefault(x => string.Equals(x, kv.Key, StringComparison.OrdinalIgnoreCase)));
                }
                else
                {
                    // Upsert (case-insensitive)
                    var existingKey = q.Keys.FirstOrDefault(x => string.Equals(x, kv.Key, StringComparison.OrdinalIgnoreCase));
                    if (existingKey is null) q[kv.Key] = kv.Value;
                    else q[existingKey] = kv.Value;
                }
            }

            var qs = BuildQueryString(q);
            return path + (string.IsNullOrEmpty(qs) ? "" : "?" + qs);
        }

        private static string NormalizeTargetPath(string targetPath)
        {
            if (string.IsNullOrWhiteSpace(targetPath)) return "";
            // If caller passed a full absolute URL, convert to base-relative path
            // Otherwise, trim leading slash for consistency.
            if (Uri.TryCreate(targetPath, UriKind.Absolute, out var abs))
            {
                // Use only path and query (but this helper composes its own query)
                return abs.AbsolutePath.TrimStart('/');
            }
            return targetPath.TrimStart('/');
        }

        private static string BuildQueryString(IDictionary<string, string?> map)
        {
            // QueryHelpers.AddQueryString requires a base and returns a full string; we just need the string after '?'
            if (map.Count == 0) return "";
            var pairs = map
                .Where(kv => kv.Value != null)
                .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}");
            return string.Join("&", pairs);
        }
    }
}
