// Prototype/Components/Layout/Navigation/SidebarSections.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;

namespace Prototype.Components.Layout.Navigation;

public sealed partial class SidebarSections : ComponentBase, IDisposable
{
    // ===== Parameters =====
    [Parameter] public IReadOnlyList<SidebarSection> Sections { get; set; } = Array.Empty<SidebarSection>();

    /// <summary>
    /// Map a section key -> query key (e.g., { "status":"status", "option":null, "asn":"asn" }).
    /// If null or missing, that section selection won’t be reflected in the query.
    /// </summary>
    [Parameter]
    public Dictionary<string, string?> SectionQueryKeys { get; set; } = new(StringComparer.OrdinalIgnoreCase)
    {
        ["option"] = null,
        ["status"] = "status",
        ["asn"] = "asn"   // <-- REQUIRED so ASN never falls back to path matching
    };

    /// <summary>Two-way selection bindings. Keys should be section keys, not titles.</summary>
    [Parameter] public Dictionary<string, string?> SelectedBySection { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    [Parameter] public EventCallback<Dictionary<string, string?>> SelectedBySectionChanged { get; set; }

    [Parameter] public EventCallback<SidebarItem> OnItemSelected { get; set; }

    [Parameter] public string Width { get; set; } = "255px";
    [Parameter] public string HeaderColor { get; set; } = "#0c5ea8";
    [Parameter] public string AccentColor { get; set; } = "#f0d400";
    [Parameter] public string BackgroundColor { get; set; } = "#f2f4f8";

    [Inject] private NavigationManager Nav { get; set; } = default!;

    // ===== Internals (URL snapshot) =====
    private string _path = "";
    private Dictionary<string, string?> _query = new(StringComparer.OrdinalIgnoreCase);

    protected override void OnInitialized()
    {
        ReadUrlIntoSnapshot();
        Nav.LocationChanged += HandleNavChanged;
    }

    protected override void OnParametersSet()
    {
        ReadUrlIntoSnapshot();
    }

    private void HandleNavChanged(object? sender, LocationChangedEventArgs e)
    {
        ReadUrlIntoSnapshot();
        _ = InvokeAsync(StateHasChanged);
    }

    // ===== Selection logic =====
    private bool IsSelected(SidebarSection section, SidebarItem item)
    {
        // Normalize the key we use for routing/selection
        var sk = (section.SectionKey ?? section.Title ?? "").Trim();
        var skKey = sk.ToLowerInvariant();

        if (section.IsLegend || string.IsNullOrWhiteSpace(sk)) return false;

        // ---- ASN: ALWAYS query-driven; never fall back to path ----
        if (skKey == "asn")
        {
            var hasQ = _query.TryGetValue("asn", out var currentAsn) && !string.IsNullOrWhiteSpace(currentAsn);
            var itemAsn = !string.IsNullOrWhiteSpace(item.Key) ? item.Key : ExtractAsnId(item.Text);

            if (hasQ)
                return string.Equals(itemAsn, currentAsn, StringComparison.OrdinalIgnoreCase);

            // No ?asn= in URL → select ONLY the first ASN item
            var firstAsnItem = Sections
                .FirstOrDefault(s => (s.SectionKey ?? s.Title ?? "").Trim().Equals("asn", StringComparison.OrdinalIgnoreCase))
                ?.Items.FirstOrDefault();
            var firstAsn = firstAsnItem is null
                ? null
                : (!string.IsNullOrWhiteSpace(firstAsnItem.Key) ? firstAsnItem.Key : ExtractAsnId(firstAsnItem.Text));

            return !string.IsNullOrWhiteSpace(firstAsn) &&
                   string.Equals(itemAsn, firstAsn, StringComparison.OrdinalIgnoreCase);
        }

        // ---- Query-driven sections (status or others mapped) ----
        if (SectionQueryKeys.TryGetValue(sk, out var qkeyRaw) && !string.IsNullOrWhiteSpace(qkeyRaw))
        {
            var qkey = qkeyRaw!;
            var hasQ = _query.TryGetValue(qkey, out var qval) && !string.IsNullOrWhiteSpace(qval);

            if (skKey == "status")
            {
                if (hasQ) return string.Equals(item.Text, qval, StringComparison.OrdinalIgnoreCase);
                // Default: highlight "All" when no status in URL
                return string.Equals(item.Text, "All", StringComparison.OrdinalIgnoreCase);
            }

            // Generic query-driven sections compare by Text unless you customize
            if (hasQ) return string.Equals(item.Text, qval, StringComparison.OrdinalIgnoreCase);
            return false;
        }

        // ---- Path-driven fallback (ONLY for sections that truly navigate by path) ----
        // To prevent “everything selected”, require that the item has NO query portion in its URL,
        // and compare only the path segments.
        if (!string.IsNullOrWhiteSpace(item.Url))
        {
            var itemPath = OnlyPath(item.Url);
            var itemHasQuery = HasQuery(item.Url);
            if (!itemHasQuery && !string.IsNullOrWhiteSpace(itemPath) &&
                string.Equals(Norm(itemPath), Norm(_path), StringComparison.Ordinal))
            {
                return true;
            }
        }

        // ---- Two-way bound fallback (only for non-option sections) ----
        if (!skKey.Equals("option", StringComparison.Ordinal) &&
            SelectedBySection.TryGetValue(sk, out var chosen) &&
            !string.IsNullOrWhiteSpace(chosen))
        {
            return string.Equals(item.Text, chosen, StringComparison.OrdinalIgnoreCase);
        }

        // ---- Legacy Selected flag ----
        return item.Selected;
    }

    // ===== Click handling (navigate + notify) =====
    private async Task HandleSelect(SidebarSection section, SidebarItem item)
    {
        var sk = (section.SectionKey ?? section.Title ?? "").Trim();
        var skKey = sk.ToLowerInvariant();

        // 1) Two-way binding update
        if (!string.IsNullOrWhiteSpace(sk))
        {
            var clone = new Dictionary<string, string?>(SelectedBySection, StringComparer.OrdinalIgnoreCase)
            {
                [sk] = item.Text
            };
            if (SelectedBySectionChanged.HasDelegate)
                await SelectedBySectionChanged.InvokeAsync(clone);
            SelectedBySection = clone; // local
        }

        // 2) Navigate
        if (skKey == "asn")
        {
            // Write the ASN ID to the query (not the label)
            var asn = !string.IsNullOrWhiteSpace(item.Key) ? item.Key : ExtractAsnId(item.Text);
            var hrefAsn = UpdateQuery(new Dictionary<string, string?> { ["asn"] = asn });
            Nav.NavigateTo(hrefAsn, replace: false);
        }
        else if (SectionQueryKeys.TryGetValue(sk, out var qkeyRaw) && !string.IsNullOrWhiteSpace(qkeyRaw))
        {
            var href = UpdateQuery(new Dictionary<string, string?> { [qkeyRaw!] = item.Text });
            Nav.NavigateTo(href, replace: false);
        }
        else if (!string.IsNullOrWhiteSpace(item.Url))
        {
            // Navigate by item Url (preserve mapped query keys)
            var href = WithPathKeepingQuery(item.Url!, SectionQueryKeys.Values.Where(v => !string.IsNullOrWhiteSpace(v))!);
            Nav.NavigateTo(href, replace: false);
        }

        if (OnItemSelected.HasDelegate)
            await OnItemSelected.InvokeAsync(item);
    }

    // ===== URL helpers =====
    private void ReadUrlIntoSnapshot()
    {
        var abs = Nav.ToAbsoluteUri(Nav.Uri);
        var rel = Nav.ToBaseRelativePath(abs.ToString()).TrimStart('/');

        var cut = rel.IndexOfAny(new[] { '?', '#' });
        _path = cut >= 0 ? rel[..cut] : rel;

        var q = QueryHelpers.ParseQuery(abs.Query);
        _query = q.ToDictionary(k => k.Key, v => v.Value.LastOrDefault(), StringComparer.OrdinalIgnoreCase);
        foreach (var k in _query.Keys.ToList())
            if (string.IsNullOrWhiteSpace(_query[k])) _query[k] = null;
    }

    private static string OnlyPath(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) return "";
        if (Uri.TryCreate(url, UriKind.Absolute, out var abs))
            return abs.AbsolutePath.TrimStart('/');

        var rel = url.TrimStart('/');
        var q = rel.IndexOf('?', StringComparison.Ordinal);
        return q >= 0 ? rel[..q] : rel;
    }

    private static bool HasQuery(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;
        if (Uri.TryCreate(url, UriKind.Absolute, out var abs))
            return !string.IsNullOrEmpty(abs.Query);
        return url.Contains('?', StringComparison.Ordinal);
    }

    private static string Norm(string s)
    {
        s = s.Trim();
        if (s.EndsWith("/")) s = s.TrimEnd('/');
        return s.ToLowerInvariant();
    }

    private string WithPathKeepingQuery(string targetPath, IEnumerable<string?> keepKeys)
    {
        var kept = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        foreach (var k in keepKeys.Where(k => !string.IsNullOrWhiteSpace(k))!)
            if (_query.TryGetValue(k!, out var v) && !string.IsNullOrWhiteSpace(v))
                kept[k!] = v;

        var path = OnlyPath(targetPath);
        if (kept.Count == 0) return "/" + path;

        var qs = string.Join("&", kept.Select(kv =>
            $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}".Replace("%20", "+")));
        return "/" + path + "?" + qs;
    }

    private string UpdateQuery(IDictionary<string, string?> updates)
    {
        var rel = _path;
        var query = new Dictionary<string, string?>(_query, StringComparer.OrdinalIgnoreCase);

        foreach (var kv in updates)
        {
            if (string.IsNullOrWhiteSpace(kv.Value)) query.Remove(kv.Key);
            else query[kv.Key] = kv.Value;
        }

        var qs = string.Join("&", query
            .Where(kv => !string.IsNullOrWhiteSpace(kv.Value))
            .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}".Replace("%20", "+")));

        return "/" + rel + (string.IsNullOrEmpty(qs) ? "" : "?" + qs);
    }

    private static string ExtractAsnId(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;
        var left = text.Split('-', 2)[0].Trim();
        return left.Replace(" ", "", StringComparison.Ordinal);
    }

    public void Dispose()
    {
        Nav.LocationChanged -= HandleNavChanged;
    }
}
