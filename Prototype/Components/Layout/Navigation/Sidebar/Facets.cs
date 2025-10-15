// Sidebar/Facets.cs
using Prototype.Components.Layout.Navigation.Sidebar;

// Sidebar/Facets.cs
public record FacetOption(
    string Text,
    string Value,
    string? Icon = null,
    string? ColorHex = null,
    string? Href = null 
);
public record Facet(
    string Key,                // e.g. "status", "asn", "items"
    string Title,              // section header
    Func<IEnumerable<FacetOption>> Options, // supplier
    bool IsLegend = false,
    bool IsMulti = false       // true => repeated key in URL (e.g., items=A&items=B)
);

// Sidebar/Facets.cs
// Sidebar/Facets.cs
public static class FacetSections
{
    public static List<SidebarSection> Build(
        IEnumerable<Facet> facets,
        IReadOnlyDictionary<string, string?> scalars,
        IReadOnlyCollection<string> multiSelections,
        string basePath)
    {
        var sections = new List<SidebarSection>();

        foreach (var f in facets)
        {
            if (f.IsLegend)
            {
                sections.Add(new SidebarSection
                {
                    SectionKey = f.Key,
                    Title = f.Title,
                    IsLegend = true,
                    Items = f.Options().Select(o => new SidebarItem { Text = o.Text, ColorHex = o.ColorHex }).ToList()
                });
                continue;
            }

            var selectedScalar = scalars.TryGetValue(f.Key, out var s) ? s : null;

            var sec = new SidebarSection { SectionKey = f.Key, Title = f.Title };
            foreach (var opt in f.Options())
            {
                var isSelected =
                    f.IsMulti
                        ? multiSelections.Contains(opt.Value, StringComparer.OrdinalIgnoreCase)
                        : string.Equals(selectedScalar, opt.Value, StringComparison.OrdinalIgnoreCase);

                string url;

                if (!string.IsNullOrWhiteSpace(opt.Href))
                {
                    // Absolute override (used for cross-page "Option" facet)
                    url = opt.Href!;
                }
                else
                {
                    // ✅ Write the clicked option into this facet key
                    var newScalars = new Dictionary<string, string?>(scalars, StringComparer.OrdinalIgnoreCase);
                    if (!f.IsMulti)
                    {
                        newScalars[f.Key] = opt.Value;
                    }
                    // (Multi facets usually toggle via page code; we leave them alone here.)

                    var qs = string.Join("&", newScalars
                        .Where(kv => !string.IsNullOrWhiteSpace(kv.Value))
                        .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}"));

                    url = $"{basePath}{(string.IsNullOrEmpty(qs) ? "" : "?" + qs)}";
                }

                sec.Items.Add(new SidebarItem
                {
                    Text = opt.Text,
                    Key = opt.Value,
                    Url = url,
                    Icon = opt.Icon,
                    Selected = isSelected
                });
            }

            sections.Add(sec);
        }

        return sections;
    }
}

