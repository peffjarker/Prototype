// Pages/Collections/TechCreditPaymentsParameters.cs
using Services;

namespace Prototype.Pages.Collections
{
    public sealed class TechCreditPaymentsParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string View { get; set; } = "PastDue"; // PastDue, Scheduled, History
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // === Factory: read directly from the URL service ===
        public static TechCreditPaymentsParameters FromUrl(IUrlState url)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;

            if (DateTime.TryParse(url.Get("startDate"), out var parsedStart))
                startDate = parsedStart;
            
            if (DateTime.TryParse(url.Get("endDate"), out var parsedEnd))
                endDate = parsedEnd;

            return new TechCreditPaymentsParameters
            {
                Dealer = url.Get("dealer"),
                View = url.Get("view") ?? "PastDue",
                StartDate = startDate,
                EndDate = endDate
            };
        }

        // === Factory: rebuild from validated scalars ===
        public static TechCreditPaymentsParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("view", out var view);
            scalars.TryGetValue("startDate", out var startDateStr);
            scalars.TryGetValue("endDate", out var endDateStr);

            DateTime? startDate = null;
            DateTime? endDate = null;

            if (DateTime.TryParse(startDateStr, out var parsedStart))
                startDate = parsedStart;
            
            if (DateTime.TryParse(endDateStr, out var parsedEnd))
                endDate = parsedEnd;

            return new TechCreditPaymentsParameters
            {
                Dealer = dealer,
                View = !string.IsNullOrWhiteSpace(view) ? view! : "PastDue",
                StartDate = startDate,
                EndDate = endDate
            };
        }

        // === Encode back to scalars for navigation ===
        public Dictionary<string, string?> ToScalars()
        {
            return new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
            {
                ["dealer"] = Dealer,
                ["view"] = View,
                ["startDate"] = StartDate?.ToString("yyyy-MM-dd"),
                ["endDate"] = EndDate?.ToString("yyyy-MM-dd")
            };
        }
    }
}
