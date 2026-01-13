using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prototype.Components.Services.Search;
using static Prototype.Components.Pages.POXfer.PO.PurchaseOrdersSeedData;

namespace Prototype.Components.Pages.POXfer.PO;

/// <summary>
/// Search provider for Purchase Order data
/// </summary>
public sealed class POSearchProvider : ISearchProvider
{
    public string ProviderName => "Purchase Orders";
    public SearchResultType ResultType => SearchResultType.PurchaseOrder;
    public bool IsEnabled => true;
    public string? RequiredPolicy => AppPolicies.CustomerService;

    private readonly List<PoSummary> _poSummaries;

    public POSearchProvider()
    {
        // Load seed data - in production this would come from a repository/service
        _poSummaries = GetPoSummaries();
    }

    public Task<List<SearchResult>> SearchAsync(string query, int maxResults = 10)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Task.FromResult(new List<SearchResult>());

        var searchTerm = query.ToLowerInvariant();

        var results = _poSummaries
            .Where(po =>
                po.Number.ToLowerInvariant().Contains(searchTerm) ||
                po.Status.ToLowerInvariant().Contains(searchTerm) ||
                po.Class.ToLowerInvariant().Contains(searchTerm))
            .OrderBy(po =>
            {
                // Prioritize exact PO number matches
                if (po.Number.Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 0;
                if (po.Number.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 1;
                // Then by date (most recent first)
                return 2;
            })
            .ThenByDescending(po => po.Date)
            .Take(maxResults)
            .Select(po => new SearchResult
            {
                Id = po.Number,
                Title = po.Number,
                Subtitle = $"{po.Status} - {po.Date:MM/dd/yyyy}",
                Type = SearchResultType.PurchaseOrder,
                NavigateUrl = $"/po-transfer/purchase-orders?po={Uri.EscapeDataString(po.Number)}",
                Badge = GetPoBadge(po),
                Description = po.Class,
                Metadata = new Dictionary<string, string>
                {
                    ["Status"] = po.Status,
                    ["Class"] = po.Class,
                    ["Date"] = po.Date.ToString("MM/dd/yyyy")
                }
            })
            .ToList();

        return Task.FromResult(results);
    }

    private static string? GetPoBadge(PoSummary po)
    {
        return po.Status switch
        {
            "Open – Acknowledged" => "Open",
            "Partial" => "Partial",
            "Closed" => "Closed",
            "Cancelled" => "Cancelled",
            _ => po.Status
        };
    }
}