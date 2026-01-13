// Components/Pages/Collections/SalesAuthorizations/SalesAuthorizationsParameters.cs
using Services;

namespace Prototype.Components.Pages.Collections.SalesAuthorizations
{
    public sealed class SalesAuthorizationsParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string Option { get; set; } = "Sales Authorizations";
        public string CustomerStatus { get; set; } = "All";
        public string? SelectedCustomer { get; set; }
        public string HistoryView { get; set; } = "recap";

        // === Factory: read directly from the URL service ===
        public static SalesAuthorizationsParameters FromUrl(IUrlState url)
        {
            return new SalesAuthorizationsParameters
            {
                Dealer = url.Get("dealer"),
                Option = url.Get("option") ?? "Sales Authorizations",
                CustomerStatus = url.Get("status") ?? "All",
                SelectedCustomer = url.Get("customer"),
                HistoryView = url.Get("view") ?? "recap"
            };
        }

        // === Factory: rebuild from validated scalars ===
        public static SalesAuthorizationsParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("option", out var option);
            scalars.TryGetValue("status", out var status);
            scalars.TryGetValue("customer", out var customer);
            scalars.TryGetValue("view", out var view);

            return new SalesAuthorizationsParameters
            {
                Dealer = dealer,
                Option = !string.IsNullOrWhiteSpace(option) ? option! : "Sales Authorizations",
                CustomerStatus = !string.IsNullOrWhiteSpace(status) ? status! : "All",
                SelectedCustomer = customer,
                HistoryView = !string.IsNullOrWhiteSpace(view) ? view! : "recap"
            };
        }

        // === Encode back to scalars for navigation ===
        public Dictionary<string, string?> ToScalars()
        {
            return new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
            {
                ["dealer"] = Dealer,
                ["option"] = "Sales Authorizations",
                ["status"] = CustomerStatus,
                ["customer"] = SelectedCustomer,
                ["view"] = HistoryView
            };
        }
    }
}