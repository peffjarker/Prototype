// Pages/POTransfer/ItemsOnOrderParameters.cs
using Services;

namespace Prototype.Pages.POTransfer
{
    public sealed class ItemsOnOrderParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public StatusKind Status { get; set; } = StatusKind.All;

        // === URL-backed multi (comma-separated) ===
        public List<string> SelectedItems { get; set; } = new();

        // === Factory: read directly from the URL service ===
        public static ItemsOnOrderParameters FromUrl(IUrlState url)
        {
            return new ItemsOnOrderParameters
            {
                Dealer = url.Get("dealer"),
                Status = ParseStatus(url.Get("status")),
                SelectedItems = url.GetMulti("items")
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList()
            };
        }

        // === Factory: rebuild from validated scalars (+ optional multi) ===
        public static ItemsOnOrderParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars,
            IReadOnlyCollection<string>? multiValues = null)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("status", out var statusRaw);

            var selectedItems = multiValues?
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList()
                ?? new List<string>();

            return new ItemsOnOrderParameters
            {
                Dealer = dealer,
                Status = ParseStatus(statusRaw),
                SelectedItems = selectedItems
            };
        }

        // === Encode back to scalars for navigation ===
        public Dictionary<string, string?> ToScalars()
        {
            return new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
            {
                ["dealer"] = Dealer,
                ["option"] = "Items On Order",  // Hard-coded - derived from page, not URL
                ["status"] = StatusToString(Status)
            };
        }

        // === Status enum and mapping ===
        public enum StatusKind { All, Backorders, InTransit, Unacknowledged }

        private static StatusKind ParseStatus(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return StatusKind.All;
            return s switch
            {
                "Backorders" => StatusKind.Backorders,
                "In Transit" => StatusKind.InTransit,
                "Unacknowledged" => StatusKind.Unacknowledged,
                _ => StatusKind.All
            };
        }

        private static string StatusToString(StatusKind status) => status switch
        {
            StatusKind.All => "All",
            StatusKind.Backorders => "Backorders",
            StatusKind.InTransit => "In Transit",
            StatusKind.Unacknowledged => "Unacknowledged",
            _ => "All"
        };
    }
}