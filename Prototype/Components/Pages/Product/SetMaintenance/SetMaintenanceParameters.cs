// Pages/Product/SetMaintenanceParameters.cs
using System;
using System.Collections.Generic;
using Services;

namespace Prototype.Pages.Product
{
    public sealed class SetMaintenanceParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string Class { get; set; } = "All Items";
        public string? SelectedItem { get; set; }

        // === Factory: read directly from the URL service ===
        public static SetMaintenanceParameters FromUrl(IUrlState url)
        {
            return new SetMaintenanceParameters
            {
                Dealer = url.Get("dealer"),
                Class = url.Get("class") ?? "All Items",
                SelectedItem = url.Get("item")
            };
        }

        // === Factory: rebuild from validated scalars ===
        public static SetMaintenanceParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("class", out var itemClass);
            scalars.TryGetValue("item", out var item);

            return new SetMaintenanceParameters
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
                ["option"] = "Set Maintenance",  // Hard-coded - derived from page, not URL
                ["class"] = Class,
                ["item"] = SelectedItem
            };
        }
    }
}