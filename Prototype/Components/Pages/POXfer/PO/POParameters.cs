// Pages/POTransfer/PurchaseOrdersParameters.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Services;

namespace Prototype.Pages.POTransfer
{
    public sealed class PurchaseOrdersParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string Option { get; set; } = "Purchase Orders";
        public PoStatusKind Status { get; set; } = PoStatusKind.All;
        public string? PoNumber { get; set; }

        // === URL-backed multi (comma-separated) ===
        public List<string> SelectedLines { get; set; } = new();

        // === Factory: read directly from the URL service ===
        public static PurchaseOrdersParameters FromUrl(IUrlState url)
        {
            return new PurchaseOrdersParameters
            {
                Dealer = url.Get("dealer"),
                Option = url.Get("option") ?? "Purchase Orders",
                Status = ParseStatus(url.Get("status")),
                PoNumber = url.Get("po"),
                SelectedLines = url.GetMulti("lines")
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList()
            };
        }

        // === Factory: rebuild from validated scalars (+ optional multi) ===
        public static PurchaseOrdersParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars,
            IReadOnlyCollection<string>? multiValues = null)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("option", out var option);
            scalars.TryGetValue("status", out var statusRaw);
            scalars.TryGetValue("po", out var po);

            var selectedLines = multiValues?
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList()
                ?? new List<string>();

            return new PurchaseOrdersParameters
            {
                Dealer = dealer,
                Status = ParseStatus(statusRaw),
                PoNumber = po,
                SelectedLines = selectedLines
            };
        }

        // === Encode back to scalars for navigation ===
        public Dictionary<string, string?> ToScalars()
        {
            return new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
            {
                ["dealer"] = Dealer,
                ["option"] = Option,
                ["status"] = StatusToString(Status),
                ["po"] = PoNumber
            };
        }

        // === Status enum and mapping ===
        public enum PoStatusKind
        {
            All,
            Pending,
            InProcess,
            IssuedUnAcknowledged,
            OpenAcknowledged,
            Closed,
            Cancelled
        }

        private static PoStatusKind ParseStatus(string? raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return PoStatusKind.All;

            var s = Normalize(raw);

            return s switch
            {
                "all" => PoStatusKind.All,
                "pending" => PoStatusKind.Pending,
                "inprocess" or "in process" => PoStatusKind.InProcess,
                "issuedunacknowledged" or "issued-unacknowledged" => PoStatusKind.IssuedUnAcknowledged,
                "openacknowledged" or "open-acknowledged" => PoStatusKind.OpenAcknowledged,
                "closed" => PoStatusKind.Closed,
                "cancelled" or "canceled" => PoStatusKind.Cancelled,
                _ => PoStatusKind.All
            };
        }

        public static string StatusToString(PoStatusKind status) => status switch
        {
            PoStatusKind.All => "All",
            PoStatusKind.Pending => "Pending",
            PoStatusKind.InProcess => "In Process",
            PoStatusKind.IssuedUnAcknowledged => "Issued – UnAcknowledged",
            PoStatusKind.OpenAcknowledged => "Open – Acknowledged",
            PoStatusKind.Closed => "Closed",
            PoStatusKind.Cancelled => "Cancelled",
            _ => "All"
        };

        private static string Normalize(string s)
        {
            s = s.Trim().ToLowerInvariant();
            s = s.Replace('–', '-').Replace('—', '-');
            s = s.Replace(" ", string.Empty);
            return s;
        }
    }
}