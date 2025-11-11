// Pages/Collections/CreditApplicationParameters.cs
using Services;

namespace Prototype.Pages.Collections
{
    public sealed class CreditApplicationParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string Option { get; set; } = "Credit Application";
        public string CustomerStatus { get; set; } = "All";
        public string? SelectedCustomer { get; set; }

        // === Factory: read directly from the URL service ===
        public static CreditApplicationParameters FromUrl(IUrlState url)
        {
            return new CreditApplicationParameters
            {
                Dealer = url.Get("dealer"),
                Option = url.Get("option") ?? "Credit Application",
                CustomerStatus = url.Get("status") ?? "All",
                SelectedCustomer = url.Get("customer")
            };
        }

        // === Factory: rebuild from validated scalars ===
        public static CreditApplicationParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("option", out var option);
            scalars.TryGetValue("status", out var status);
            scalars.TryGetValue("customer", out var customer);

            return new CreditApplicationParameters
            {
                Dealer = dealer,
                Option = !string.IsNullOrWhiteSpace(option) ? option! : "Credit Application",
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
                ["option"] = Option,
                ["status"] = CustomerStatus,
                ["customer"] = SelectedCustomer
            };
        }
    }
}