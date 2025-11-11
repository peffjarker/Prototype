// Pages/techcredit/customerinformationParameters.cs
using Services;

namespace Prototype.Pages.Collections
{
    public sealed class TechCreditParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string Option { get; set; } = "Customer Information";
        public string CustomerStatus { get; set; } = "All";
        public string? SelectedCustomer { get; set; }

        // === Factory: read directly from the URL service ===
        public static TechCreditParameters FromUrl(IUrlState url)
        {
            return new TechCreditParameters
            {
                Dealer = url.Get("dealer"),
                Option = url.Get("option") ?? "Customer Information",
                CustomerStatus = url.Get("status") ?? "All",
                SelectedCustomer = url.Get("customer")
            };
        }

        // === Factory: rebuild from validated scalars ===
        public static TechCreditParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("option", out var option);
            scalars.TryGetValue("status", out var status);
            scalars.TryGetValue("customer", out var customer);

            return new TechCreditParameters
            {
                Dealer = dealer,
                Option = !string.IsNullOrWhiteSpace(option) ? option! : "Customer Information",
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