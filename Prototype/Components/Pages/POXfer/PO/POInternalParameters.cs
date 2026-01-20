// Pages/POTransfer/POInternalParameters.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Services;

namespace Prototype.Pages.POTransfer
{
    public sealed class POInternalParameters
    {
        // === URL-backed scalars ===
        public SearchMode Mode { get; set; } = SearchMode.SalesOrder;
        public string? Dealer { get; set; }
        public string? PoNumber { get; set; }
        public string? SalesOrder { get; set; }

        // === URL-backed multi (comma-separated) ===
        public List<string> SelectedLines { get; set; } = new();

        // === Factory: read directly from the URL service ===
        public static POInternalParameters FromUrl(IUrlState url)
        {
            return new POInternalParameters
            {
                Mode = ParseMode(url.Get("mode")),
                Dealer = url.Get("dealer"),
                PoNumber = url.Get("po"),
                SalesOrder = url.Get("so"),
                SelectedLines = url.GetMulti("lines")
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList()
            };
        }

        // === Factory: rebuild from validated scalars (+ optional multi) ===
        public static POInternalParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars,
            IReadOnlyCollection<string>? multiValues = null)
        {
            scalars.TryGetValue("mode", out var mode);
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("po", out var po);
            scalars.TryGetValue("so", out var so);

            var selectedLines = multiValues?
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList()
                ?? new List<string>();

            return new POInternalParameters
            {
                Mode = ParseMode(mode),
                Dealer = dealer,
                PoNumber = po,
                SalesOrder = so,
                SelectedLines = selectedLines
            };
        }

        // === Encode back to scalars for navigation ===
        public Dictionary<string, string?> ToScalars()
        {
            return new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
            {
                ["mode"] = ModeToString(Mode),
                ["dealer"] = Dealer,
                ["po"] = PoNumber,
                ["so"] = SalesOrder
            };
        }

        // === Search mode enum and mapping ===
        public enum SearchMode
        {
            SalesOrder,
            PurchaseOrder
        }

        private static SearchMode ParseMode(string? raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return SearchMode.SalesOrder;

            var s = raw.Trim().ToLowerInvariant();

            return s switch
            {
                "so" or "salesorder" or "sales-order" => SearchMode.SalesOrder,
                "po" or "purchaseorder" or "purchase-order" => SearchMode.PurchaseOrder,
                _ => SearchMode.SalesOrder
            };
        }

        public static string ModeToString(SearchMode mode) => mode switch
        {
            SearchMode.SalesOrder => "so",
            SearchMode.PurchaseOrder => "po",
            _ => "so"
        };
    }
}
