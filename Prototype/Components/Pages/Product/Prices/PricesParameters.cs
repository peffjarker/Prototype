// Pages/Product/PricesParameters.cs
using System;
using System.Collections.Generic;
using Services;

namespace Prototype.Pages.Product
{
    public sealed class PricesParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string Class { get; set; } = "All Items";
        public string? SelectedItem { get; set; }

        // === Factory: read directly from the URL service ===
        public static PricesParameters FromUrl(IUrlState url)
        {
            return new PricesParameters
            {
                Dealer = url.Get("dealer"),
                Class = url.Get("class") ?? "All Items",
                SelectedItem = url.Get("item")
            };
        }

        // === Factory: rebuild from validated scalars ===
        public static PricesParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("class", out var itemClass);
            scalars.TryGetValue("item", out var item);

            return new PricesParameters
            {
                Dealer = dealer,
                Class = !string.IsNullOrWhiteSpace(itemClass) ? itemClass! : "All Items",
                SelectedItem = item
            };
        }

        // === Encode back to scalars for navigation ===
        public Dictionary<string, string?> ToScalars()
        {
            return new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
            {
                ["dealer"] = Dealer,
                ["option"] = "View/Change Prices",  // Hard-coded - derived from page, not URL
                ["class"] = Class,
                ["item"] = SelectedItem
            };
        }
    }
}