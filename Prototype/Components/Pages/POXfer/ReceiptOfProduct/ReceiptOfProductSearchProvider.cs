using Prototype.Components.Services.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Prototype.Components.Pages.POXfer.ReceiptOfProduct.ReceiptOfProductSeedData;
using static Prototype.Pages.POTransfer.ReceiptOfProductParameters;

namespace Prototype.Components.Pages.POXfer.ReceiptOfProduct;

/// <summary>
/// EXAMPLE: Search provider for Receipt of Product data
/// 
/// This is a template showing how to create a new search provider.
/// To use this:
/// 1. Update the data source (currently uses seed data)
/// 2. Uncomment this file
/// 3. Register in SearchServiceExtensions.cs
/// </summary>
public sealed class ReceiptOfProductSearchProvider : ISearchProvider
{
    public string ProviderName => "Receipts";
    public SearchResultType ResultType => SearchResultType.Receipt;
    public bool IsEnabled => true;

    // TODO: Replace with actual repository/service injection
    // private readonly IReceiptRepository _receiptRepo;
    // public ReceiptSearchProvider(IReceiptRepository receiptRepo)
    // {
    //     _receiptRepo = receiptRepo;
    // }

    private readonly List<AsnSummary> _receipts;

    public ReceiptOfProductSearchProvider()
    {
        // Load seed data - REPLACE THIS with repository call in production
        _receipts = GetAsnSummaries();
    }

    public Task<List<SearchResult>> SearchAsync(string query, int maxResults = 10)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Task.FromResult(new List<SearchResult>());

        // TODO: In production, this would be:
        // var receipts = await _receiptRepo.SearchAsync(query, maxResults);

        var searchTerm = query.ToLowerInvariant();

        var results = _receipts
            .Where(receipt =>
                // Search by ASN ID
                receipt.AsnId.ToLowerInvariant().Contains(searchTerm) ||
                // Search by ship from
                receipt.ShipFrom.ToLowerInvariant().Contains(searchTerm) ||
                // Search by class
                receipt.Class.ToLowerInvariant().Contains(searchTerm) ||
                // Search by status
                receipt.Status.ToString().ToLowerInvariant().Contains(searchTerm))
            .OrderBy(receipt =>
            {
                // Prioritize exact ASN ID matches
                if (receipt.AsnId.Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 0;
                if (receipt.AsnId.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 1;
                return 2;
            })
            .ThenByDescending(receipt => receipt.ShipDate) // Most recent first
            .Take(maxResults)
            .Select(receipt => new SearchResult
            {
                Id = receipt.AsnId,
                Title = receipt.Display,
                Subtitle = $"{receipt.Status} - {receipt.ShipDate:MM/dd/yyyy}",
                Type = SearchResultType.Receipt,
                NavigateUrl = $"/po-transfer/receipt-of-product?asn={Uri.EscapeDataString(receipt.AsnId)}",
                Price = receipt.Total, // Show total as "price"
                Badge = GetReceiptBadge(receipt),
                Description = receipt.ShipFrom,
                Metadata = new Dictionary<string, string>
                {
                    ["Status"] = receipt.Status.ToString(),
                    ["Class"] = receipt.Class,
                    ["ShipFrom"] = receipt.ShipFrom,
                    ["ShipDate"] = receipt.ShipDate.ToString("MM/dd/yyyy")
                }
            })
            .ToList();

        return Task.FromResult(results);
    }

    private static string? GetReceiptBadge(AsnSummary receipt)
    {
        return receipt.Status switch
        {
            ReceiptStatusKind.Open => "Open",
            ReceiptStatusKind.Closed => "Closed",
            _ => null
        };
    }
}

/* 
 * TO ENABLE THIS PROVIDER:
 * 
 * 1. Uncomment this entire file
 * 
 * 2. In SearchServiceExtensions.cs, add:
 * 
 *    searchService.RegisterProvider(new ReceiptSearchProvider());
 * 
 * 3. If using ERP repository instead of seed data:
 * 
 *    public sealed class ReceiptSearchProvider : ISearchProvider
 *    {
 *        private readonly IReceiptRepository _repo;
 *        
 *        public ReceiptSearchProvider(IReceiptRepository repo)
 *        {
 *            _repo = repo;
 *        }
 *        
 *        public async Task<List<SearchResult>> SearchAsync(string query, int maxResults = 10)
 *        {
 *            var receipts = await _repo.SearchAsync(query, maxResults);
 *            return receipts.Select(MapToSearchResult).ToList();
 *        }
 *    }
 * 
 *    Then in SearchServiceExtensions.cs:
 * 
 *    var receiptRepo = sp.GetRequiredService<IReceiptRepository>();
 *    searchService.RegisterProvider(new ReceiptSearchProvider(receiptRepo));
 */