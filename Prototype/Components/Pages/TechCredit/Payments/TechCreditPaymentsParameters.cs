// Pages/techcredit/customerinformationParameters.cs
using Services;

namespace Prototype.Pages.Collections
{
    public sealed class TechCreditPaymentParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string Option { get; set; } = "Payments";
        public string? SelectedCustomer { get; set; }

        // === Factory: read directly from the URL service ===
        public static TechCreditPaymentParameters FromUrl(IUrlState url)
        {
            return new TechCreditPaymentParameters
            {
                Dealer = url.Get("dealer"),
                Option = url.Get("option") ?? "Payments",
                SelectedCustomer = url.Get("customer")
            };
        }

        // === Factory: rebuild from validated scalars ===
        public static TechCreditPaymentParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("option", out var option);
            scalars.TryGetValue("customer", out var customer);

            return new TechCreditPaymentParameters
            {
                Dealer = dealer,
                Option = !string.IsNullOrWhiteSpace(option) ? option! : "Payments",
                SelectedCustomer = customer
            };
        }

        // === Encode back to scalars for navigation ===
        public Dictionary<string, string?> ToScalars()
        {
            return new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
            {
                ["dealer"] = Dealer,
                ["option"] = "Payments",
                ["customer"] = SelectedCustomer
            };
        }
    }
}