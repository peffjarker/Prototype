// Utils/QueryStringMap.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Utils
{
    public static class QueryStringMap
    {
        /// <summary>Read the current query string into a case-insensitive dictionary.</summary>
        public static Dictionary<string, string?> Read(NavigationManager nav)
        {
            var abs = nav.ToAbsoluteUri(nav.Uri);
            var parsed = QueryHelpers.ParseQuery(abs.Query);
            return parsed.ToDictionary(kv => kv.Key, kv => kv.Value.LastOrDefault(), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Writes a query composed of <paramref name="scalars"/> and optional repeated keys in <paramref name="multi"/> to the current path.
        /// Passing null/empty values in scalars will remove the key from the query.
        /// </summary>
        public static void Write(
            NavigationManager nav,
            IReadOnlyDictionary<string, string?> scalars,
            IEnumerable<(string key, string val)>? multi = null,
            bool replace = false)
        {
            var abs = nav.ToAbsoluteUri(nav.Uri);
            var rel = nav.ToBaseRelativePath(abs.ToString()).TrimStart('/');
            var cut = rel.IndexOfAny(new[] { '?', '#' });
            var pathOnly = cut >= 0 ? rel[..cut] : rel;

            var dict = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
            foreach (var kv in scalars)
            {
                if (!string.IsNullOrWhiteSpace(kv.Key) && !string.IsNullOrWhiteSpace(kv.Value))
                    dict[kv.Key] = kv.Value!;
            }

            // Build single-value qs from dict (using QueryHelpers to avoid hand-encoding)
            var qs = dict.Count > 0 ? QueryHelpers.AddQueryString("", dict!) : "";

            // Add any multi-value pairs (already encoded by AddQueryString? No -> we append safely)
            var extra = "";
            if (multi != null)
            {
                var parts = new List<string>();
                foreach (var (k, v) in multi)
                {
                    if (string.IsNullOrWhiteSpace(k) || string.IsNullOrWhiteSpace(v)) continue;
                    parts.Add($"{Uri.EscapeDataString(k)}={Uri.EscapeDataString(v)}");
                }
                if (parts.Count > 0)
                {
                    var sep = string.IsNullOrEmpty(qs) ? "?" : "&";
                    extra = sep + string.Join("&", parts);
                }
            }

            nav.NavigateTo("/" + pathOnly + qs + extra, replace);
        }

        /// <summary>
        /// Merge the current query with <paramref name="overrides"/> (null removes a key), then write.
        /// </summary>
        public static void MergeWrite(
            NavigationManager nav,
            IReadOnlyDictionary<string, string?> overrides,
            bool replace = true)
        {
            var current = Read(nav);
            foreach (var kv in overrides)
            {
                if (kv.Value is null || kv.Value.Length == 0)
                    current.Remove(kv.Key);
                else
                    current[kv.Key] = kv.Value;
            }
            Write(nav, current, replace: replace);
        }

        /// <summary>Return path + query from an absolute URI string.</summary>
        public static string PathAndQuery(string absoluteUri)
        {
            var u = new Uri(absoluteUri, UriKind.Absolute);
            return string.Concat(u.AbsolutePath, u.Query);
        }
    }
}
