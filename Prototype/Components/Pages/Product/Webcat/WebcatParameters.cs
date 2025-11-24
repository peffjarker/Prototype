// Pages/Product/WebcatParameters.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Services;

namespace Prototype.Pages.Product
{
    public sealed class WebcatParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string Class { get; set; } = "All Items";
        public string Option { get; set; } = "Webcat";
        public string? Category { get; set; }
        public string? Subcategory { get; set; }
        public string? Brand { get; set; }
        public string? ShipFrom { get; set; }
        public bool DisplayDiscontinued { get; set; } = false;
        public bool OnlyShowInventory { get; set; } = false;
        public string? SelectedItem { get; set; }

        // === Factory: read directly from the URL service ===
        public static WebcatParameters FromUrl(IUrlState url)
        {
            return new WebcatParameters
            {
                Dealer = url.Get("dealer"),
                Class = url.Get("class") ?? "All Items",
                Category = url.Get("category"),
                Subcategory = url.Get("subcategory"),
                Brand = url.Get("brand"),
                ShipFrom = url.Get("shipfrom"),
                DisplayDiscontinued = url.Get("discontinued") == "true",
                OnlyShowInventory = url.Get("inventory") == "true",
                SelectedItem = url.Get("item")
            };
        }

        // === Factory: rebuild from validated scalars ===
        public static WebcatParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("class", out var itemClass);
            scalars.TryGetValue("category", out var category);
            scalars.TryGetValue("subcategory", out var subcategory);
            scalars.TryGetValue("brand", out var brand);
            scalars.TryGetValue("shipfrom", out var shipFrom);
            scalars.TryGetValue("discontinued", out var discontinued);
            scalars.TryGetValue("inventory", out var inventory);
            scalars.TryGetValue("item", out var item);

            return new WebcatParameters
            {
                Dealer = dealer,
                Class = !string.IsNullOrWhiteSpace(itemClass) ? itemClass! : "All Items",
                Category = category,
                Subcategory = subcategory,
                Brand = brand,
                ShipFrom = shipFrom,
                DisplayDiscontinued = discontinued == "true",
                OnlyShowInventory = inventory == "true",
                SelectedItem = item
            };
        }

        // === Encode back to scalars for navigation ===
        public Dictionary<string, string?> ToScalars()
        {
            return new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
            {
                ["dealer"] = Dealer,
                ["option"] = "Webcat",
                ["class"] = Class,
                ["category"] = !string.IsNullOrEmpty(Category) ? Category : null,
                ["subcategory"] = !string.IsNullOrEmpty(Subcategory) ? Subcategory : null,
                ["brand"] = !string.IsNullOrEmpty(Brand) ? Brand : null,
                ["shipfrom"] = !string.IsNullOrEmpty(ShipFrom) ? ShipFrom : null,
                ["discontinued"] = DisplayDiscontinued ? "true" : null,
                ["inventory"] = OnlyShowInventory ? "true" : null,
                ["item"] = SelectedItem
            };
        }

        // === Helper to validate subcategory against category ===
        public void ValidateSubcategory(IReadOnlyList<string> validSubcategories)
        {
            if (!string.IsNullOrEmpty(Subcategory) &&
                !validSubcategories.Contains(Subcategory, StringComparer.OrdinalIgnoreCase))
            {
                Subcategory = null;
            }
        }

        public WebcatParameters Clone()
        {
            return new WebcatParameters
            {
                Class = this.Class,
                Category = this.Category,
                Subcategory = this.Subcategory,
                Brand = this.Brand,
                ShipFrom = this.ShipFrom,
                DisplayDiscontinued = this.DisplayDiscontinued,
                OnlyShowInventory = this.OnlyShowInventory,
                SelectedItem = this.SelectedItem
            };
        }
    }
}