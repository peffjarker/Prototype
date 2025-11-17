// Pages/Collections/TechCreditBorrowersParameters.cs
using Services;

namespace Prototype.Pages.Collections
{
    public sealed class WCSParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string? FilterStatus { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }

        // === Factory: read directly from the URL service ===
        public static WCSParameters FromUrl(IUrlState url)
        {
            return new WCSParameters
            {
                Dealer = url.Get("dealer"),
                FilterStatus = url.Get("filter"),
                SortColumn = url.Get("sort"),
                SortDirection = url.Get("dir")
            };
        }

        // === Factory: rebuild from validated scalars ===
        public static WCSParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("filter", out var filter);
            scalars.TryGetValue("sort", out var sort);
            scalars.TryGetValue("dir", out var dir);

            return new WCSParameters
            {
                Dealer = dealer,
                FilterStatus = filter,
                SortColumn = sort,
                SortDirection = dir
            };
        }

        // === Encode back to scalars for navigation ===
        public Dictionary<string, string?> ToScalars()
        {
            return new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
            {
                ["dealer"] = Dealer,
                ["filter"] = FilterStatus,
                ["sort"] = SortColumn,
                ["dir"] = SortDirection
            };
        }
    }
}
