// Components/Services/Search/ItemsOnOrderSearchProvider.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prototype.Components.Pages.POXfer.ItemsOnOrder;
using static Prototype.Components.Pages.POXfer.ItemsOnOrder.ItemsOnOrderSeedData;

namespace Prototype.Components.Services.Search;

/// <summary>
/// Search provider for Items On Order
/// </summary>
public sealed class ItemsOnOrderSearchProvider : ISearchProvider
{
    public string ProviderName => "Items On Order";
    public SearchResultType ResultType => SearchResultType.Item;
    public bool IsEnabled => true;

    private readonly List<Row> _items;

    public ItemsOnOrderSearchProvider()
    {
        // Load seed data - in production this would come from a repository/service
        _items = ItemsOnOrderSeedData.GetItems();
    }

    public Task<List<SearchResult>> SearchAsync(string query, int maxResults = 10)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Task.FromResult(new List<SearchResult>());

        var searchTerm = query.ToLowerInvariant();

        var results = _items
            .Where(item =>
                item.Item.ToLowerInvariant().Contains(searchTerm) ||
                item.Description.ToLowerInvariant().Contains(searchTerm) ||
                item.Class.ToLowerInvariant().Contains(searchTerm))
            .OrderBy(item =>
            {
                // Prioritize exact item number matches
                if (item.Item.Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 0;
                if (item.Item.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 1;
                return 2;
            })
            .Take(maxResults)
            .Select(item => new SearchResult
            {
                Id = item.Item,
                Title = item.Item,
                Subtitle = item.Description,
                Type = SearchResultType.Item,
                NavigateUrl = $"/po-transfer/items-on-order?item={Uri.EscapeDataString(item.Item)}",
                Price = item.TotalOpenCost,
                Badge = GetItemBadge(item),
                Description = $"{item.Class} • Open: {item.OnOrderOpen} • Transit: {item.InTransit}",
                Metadata = new Dictionary<string, string>
                {
                    ["Class"] = item.Class,
                    ["OnOrderOpen"] = item.OnOrderOpen.ToString(),
                    ["InTransit"] = item.InTransit.ToString(),
                    ["BKO"] = item.BKO.ToString(),
                    ["UnitCost"] = item.UnitCost.ToString("C")
                }
            })
            .ToList();

        return Task.FromResult(results);
    }

    private static string? GetItemBadge(Row item)
    {
        if (item.BKO > 0) return "Backordered";
        if (item.IsUnacknowledged) return "Unacknowledged";
        if (item.InTransit > 0) return "In Transit";
        if (item.OnOrderOpen > 0) return "On Order";
        return null;
    }
}