// Pages/techcredit/customerinformationParameters.cs
using Services;

namespace Prototype.Pages.Collections
{
    public sealed class WCSParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string Option { get; set; } = "WCS";
        public string CustomerStatus { get; set; } = "All";
        public string? SelectedCustomer { get; set; }

        // === Factory: read directly from the URL service ===
        public static WCSParameters FromUrl(IUrlState url)
        {
            return new WCSParameters
            {
                Dealer = url.Get("dealer"),
                Option = url.Get("option") ?? "WCS",
                CustomerStatus = url.Get("status") ?? "All",
                SelectedCustomer = url.Get("customer")
            };
        }

        // === Factory: rebuild from validated scalars ===
        public static WCSParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("option", out var option);
            scalars.TryGetValue("status", out var status);
            scalars.TryGetValue("customer", out var customer);

            return new WCSParameters
            {
                Dealer = dealer,
                Option = !string.IsNullOrWhiteSpace(option) ? option! : "WCS",
                CustomerStatus = !string.IsNullOrWhiteSpace(status) ? status! : "All",
                SelectedCustomer = customer
            };
        }

        // === Encode back to scalars for navigation ===
        public Dictionary<string, string?> ToScalars()
        {
            return new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
            {
                ["dealer"] = Dealer,
                ["option"] = "WCS",
                ["status"] = CustomerStatus,
                ["customer"] = SelectedCustomer
            };
        }
    }
}