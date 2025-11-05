// Pages/POTransfer/ReceiptOfProductParameters.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Services;

namespace Prototype.Pages.POTransfer
{
    public sealed class ReceiptOfProductParameters
    {
        // === URL-backed scalars ===
        public string? Dealer { get; set; }
        public string Option { get; set; } = "Receipt of Product";
        public ReceiptStatusKind Status { get; set; } = ReceiptStatusKind.Open;
        public string Class { get; set; } = "All";
        public string? SelectedAsn { get; set; }

        // === URL-backed multi (comma-separated) ===
        public List<string> SelectedLines { get; set; } = new();

        // === Factory: read directly from the URL service ===
        public static ReceiptOfProductParameters FromUrl(IUrlState url)
        {
            return new ReceiptOfProductParameters
            {
                Dealer = url.Get("dealer"),
                Option = url.Get("option") ?? "Receipt of Product",
                Status = ParseStatus(url.Get("status")),
                Class = url.Get("class") ?? "All",
                SelectedAsn = url.Get("asn"),
                SelectedLines = url.GetMulti("asnlines")
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList()
            };
        }

        // === Factory: rebuild from validated scalars (+ optional multi) ===
        public static ReceiptOfProductParameters FromScalars(
            IReadOnlyDictionary<string, string?> scalars,
            IReadOnlyCollection<string>? multiValues = null)
        {
            scalars.TryGetValue("dealer", out var dealer);
            scalars.TryGetValue("option", out var option);
            scalars.TryGetValue("status", out var statusRaw);
            scalars.TryGetValue("class", out var itemClass);
            scalars.TryGetValue("asn", out var asn);

            var selectedLines = multiValues?
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList()
                ?? new List<string>();

            return new ReceiptOfProductParameters
            {
                Dealer = dealer,
                Option = !string.IsNullOrWhiteSpace(option) ? option! : "Receipt of Product",
                Status = ParseStatus(statusRaw),
                Class = !string.IsNullOrWhiteSpace(itemClass) ? itemClass! : "All",
                SelectedAsn = asn,
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
                ["class"] = Class,
                ["asn"] = SelectedAsn
                // NOTE: SelectedLines is NOT included here - it's handled via MultiValues
            };
        }

        // === Status enum and mapping ===
        public enum ReceiptStatusKind
        {
            All,
            Open,
            Closed
        }

        private static ReceiptStatusKind ParseStatus(string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return ReceiptStatusKind.Open;

            return s.Trim() switch
            {
                var x when x.Equals("All", StringComparison.OrdinalIgnoreCase) => ReceiptStatusKind.All,
                var x when x.Equals("Open", StringComparison.OrdinalIgnoreCase) => ReceiptStatusKind.Open,
                var x when x.Equals("Closed", StringComparison.OrdinalIgnoreCase) => ReceiptStatusKind.Closed,
                _ => ReceiptStatusKind.Open
            };
        }

        public static string StatusToString(ReceiptStatusKind status) => status switch
        {
            ReceiptStatusKind.All => "All",
            ReceiptStatusKind.Open => "Open",
            ReceiptStatusKind.Closed => "Closed",
            _ => "Open"
        };
    }
}