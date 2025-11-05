// Components/Layout/Navigation/Sidebar/CommonFacets.cs
using Services;

public static class CommonFacets
{
    public static Facet OptionFacet(IUrlState url, params (string Text, string Path, string Status)[] options)
    {
        return new Facet(
            Key: "option",
            Title: "Option",
            Options: () => options.Select(o =>
                new FacetOption(o.Text, o.Text,
                    Href: url.BuildHref(o.Path,
                        new Dictionary<string, string?> { ["status"] = o.Status },
                        preserveCurrentState: false))
            )
        );
    }

    public static Facet ItemLegend => new(
        Key: "legend",
        Title: "Item Legend",
        Options: () => new[]
        {
            new FacetOption("Discontinued", "disc", ColorHex: "#e11d48"),
            new FacetOption("Price Confirm", "price", ColorHex: "#111827"),
            new FacetOption("CSI", "csi", ColorHex: "#2563eb"),
            new FacetOption("Drop Ship", "ds", ColorHex: "#f59e0b"),
            new FacetOption("Special Orders", "so", ColorHex: "#eab308")
        },
        IsLegend: true
    );

    public static Facet ClassFacet => new(
        Key: "class",
        Title: "Class",
        Options: () => new[]
        {
            new FacetOption("All Items", "All Items"),
            new FacetOption("CQT Stock", "CQT Stock"),
            new FacetOption("Local Only", "Local Only")
        }
    );
}