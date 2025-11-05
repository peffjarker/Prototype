// Sidebar/Facets.cs
using Prototype.Components.Layout.Navigation.Sidebar;
using Services;

public record FacetOption(
    string Text,
    string Value,
    string? Icon = null,
    string? ColorHex = null,
    string? Href = null
);

public record Facet(
    string Key,
    string Title,
    Func<IEnumerable<FacetOption>> Options,
    bool IsLegend = false,
    bool IsMulti = false,
    string? DependsOn = null,
    Func<string?, bool>? IsValidForParent = null
);

public static class FacetSections
{
    public static List<SidebarSection> Build(
        IEnumerable<Facet> facets,
        IReadOnlyDictionary<string, string?> scalars,
        IReadOnlyCollection<string> multiSelections,
        string basePath,
        IUrlState urlState)
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
                    Items = f.Options().Select(o => new SidebarItem
                    {
                        Text = o.Text,
                        ColorHex = o.ColorHex
                    }).ToList()
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
                    url = opt.Href!;
                }
                else if (f.IsMulti)
                {
                    var updated = new Dictionary<string, string?>(scalars, StringComparer.OrdinalIgnoreCase);
                    var currentList = multiSelections.ToList();

                    if (currentList.Contains(opt.Value, StringComparer.OrdinalIgnoreCase))
                        currentList.RemoveAll(v => string.Equals(v, opt.Value, StringComparison.OrdinalIgnoreCase));
                    else
                        currentList.Add(opt.Value);

                    updated[f.Key] = currentList.Count > 0 ? string.Join(",", currentList) : null;
                    url = urlState.BuildHref(basePath, updated, preserveCurrentState: false);
                }
                else
                {
                    var updated = new Dictionary<string, string?>(scalars, StringComparer.OrdinalIgnoreCase)
                    {
                        [f.Key] = opt.Value
                    };
                    url = urlState.BuildHref(basePath, updated, preserveCurrentState: false);
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