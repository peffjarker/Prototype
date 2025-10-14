using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Prototype.Components.Layout.Models;

namespace Prototype.Components.Layout;

public sealed partial class SidebarSections : ComponentBase, IDisposable
{
    // ===== Parameters =====
    [Parameter] public IReadOnlyList<SidebarSection> Sections { get; set; } = Array.Empty<SidebarSection>();

    /// <summary>
    /// Map a section key -> query key (e.g., { "status":"status", "option":null }).
    /// If null or missing, that section selection won’t be reflected in the query.
    /// </summary>
    [Parameter]
    public Dictionary<string, string?> SectionQueryKeys { get; set; } = new(StringComparer.OrdinalIgnoreCase)
    {
        // sensible defaults; override per page
        ["option"] = null,         // options navigate by path/Url
        ["status"] = "status"      // status rides in query string
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
        // Keep snapshot current; render will use these
        ReadUrlIntoSnapshot();
    }

    private void HandleNavChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
    {
        ReadUrlIntoSnapshot();
        _ = InvokeAsync(StateHasChanged);
    }

    // ===== Selection logic =====
    private bool IsSelected(SidebarSection section, SidebarItem item)
    {
        var sk = section.SectionKey ?? section.Title ?? "";
        if (section.IsLegend || string.IsNullOrWhiteSpace(sk)) return false;

        // If this section is mapped to a query key, selection is query-driven.
        if (SectionQueryKeys.TryGetValue(sk, out var qkey) && !string.IsNullOrWhiteSpace(qkey))
        {
            if (!_query.TryGetValue(qkey!, out var qval) || string.IsNullOrWhiteSpace(qval))
            {
                // Default to first item or one named "All"
                if (string.Equals(item.Text, "All", StringComparison.OrdinalIgnoreCase))
                    return true;
                // If no "All", nothing selected by query—fall back to two-way binding below
            }
            else
            {
                return string.Equals(item.Text, qval, StringComparison.OrdinalIgnoreCase);
            }
        }

        // Otherwise, selection is path-driven if Url matches
        if (!string.IsNullOrWhiteSpace(item.Url))
        {
            var itemPath = OnlyPath(item.Url);
            if (!string.IsNullOrWhiteSpace(itemPath) &&
                string.Equals(Norm(itemPath), Norm(_path), StringComparison.Ordinal))
            {
                return true;
            }
        }

        // Finally, fall back to two-way bound SelectedBySection (if provided)
        if (SelectedBySection.TryGetValue(sk, out var chosen) && !string.IsNullOrWhiteSpace(chosen))
            return string.Equals(item.Text, chosen, StringComparison.OrdinalIgnoreCase);

        // And lastly, legacy Selected flag (if callers set it)
        return item.Selected;
    }

    // ===== Click handling (navigate + notify) =====
    private async Task HandleSelect(SidebarSection section, SidebarItem item)
    {
        var sk = section.SectionKey ?? section.Title ?? "";

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

        // 2) Navigate (path or query depending on mapping)
        if (SectionQueryKeys.TryGetValue(sk, out var qkey) && !string.IsNullOrWhiteSpace(qkey))
        {
            // Update query only
            var href = UpdateQuery(new Dictionary<string, string?>
            {
                [qkey!] = string.Equals(item.Text, "All", StringComparison.OrdinalIgnoreCase) ? null : item.Text
            });
            Nav.NavigateTo(href, replace: false);
        }
        else if (!string.IsNullOrWhiteSpace(item.Url))
        {
            // Navigate by item Url (preserve all mapped query keys)
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
        // Normalize blanks to null
        foreach (var k in _query.Keys.ToList())
        {
            if (string.IsNullOrWhiteSpace(_query[k])) _query[k] = null;
        }
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
        {
            if (_query.TryGetValue(k!, out var v) && !string.IsNullOrWhiteSpace(v))
                kept[k!] = v;
        }

        var path = OnlyPath(targetPath);
        if (kept.Count == 0) return "/" + path;

        var qs = string.Join("&", kept.Select(kv =>
            $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}".Replace("%20", "+")));
        return "/" + path + "?" + qs;
    }

    private string UpdateQuery(IDictionary<string, string?> updates)
    {
        var rel = _path; // we already computed path-only
        var query = new Dictionary<string, string?>(_query, StringComparer.OrdinalIgnoreCase);

        foreach (var kv in updates)
        {
            if (string.IsNullOrWhiteSpace(kv.Value))
                query.Remove(kv.Key);
            else
                query[kv.Key] = kv.Value;
        }

        var qs = string.Join("&", query
            .Where(kv => !string.IsNullOrWhiteSpace(kv.Value))
            .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}".Replace("%20", "+")));

        return "/" + rel + (string.IsNullOrEmpty(qs) ? "" : "?" + qs);
    }

    public void Dispose()
    {
        Nav.LocationChanged -= HandleNavChanged;
    }
}
