// Utils/QueryStringMap.cs
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

public static class QueryStringMap
{
    public static Dictionary<string, string?> Read(NavigationManager nav)
    {
        var abs = nav.ToAbsoluteUri(nav.Uri);
        var parsed = QueryHelpers.ParseQuery(abs.Query);
        return parsed.ToDictionary(kv => kv.Key, kv => kv.Value.LastOrDefault(), StringComparer.OrdinalIgnoreCase);
    }

    public static void Write(NavigationManager nav, IReadOnlyDictionary<string, string?> scalars, IEnumerable<(string key, string val)> multi = default!, bool replace = false)
    {
        var abs = nav.ToAbsoluteUri(nav.Uri);
        var rel = nav.ToBaseRelativePath(abs.ToString()).TrimStart('/');
        var cut = rel.IndexOfAny(new[] { '?', '#' });
        var pathOnly = cut >= 0 ? rel[..cut] : rel;

        var qsParts = new List<string>();
        foreach (var kv in scalars.Where(kv => !string.IsNullOrWhiteSpace(kv.Value)))
            qsParts.Add($"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}".Replace("%20", "+"));

        if (multi != null)
        {
            foreach (var (k, v) in multi)
                qsParts.Add($"{Uri.EscapeDataString(k)}={Uri.EscapeDataString(v)}");
        }

        var qs = string.Join("&", qsParts);
        nav.NavigateTo("/" + pathOnly + (qs.Length == 0 ? "" : "?" + qs), replace);
    }
}
