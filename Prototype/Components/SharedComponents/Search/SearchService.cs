using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Prototype.Components.Services.Search;

/// <summary>
/// Main search service that coordinates all registered search providers
/// </summary>
public interface ISearchService
{
    /// <summary>
    /// Search across all enabled providers
    /// </summary>
    Task<SearchResponse> SearchAsync(string query, int maxResultsPerProvider = 5);

    /// <summary>
    /// Register a search provider
    /// </summary>
    void RegisterProvider(ISearchProvider provider);

    /// <summary>
    /// Get all registered providers
    /// </summary>
    IReadOnlyList<ISearchProvider> GetProviders();
}

/// <summary>
/// Default implementation of the search service
/// </summary>
public sealed class SearchService : ISearchService
{
    private readonly List<ISearchProvider> _providers = new();

    public void RegisterProvider(ISearchProvider provider)
    {
        if (provider == null) throw new ArgumentNullException(nameof(provider));

        // Don't register duplicates
        if (_providers.Any(p => p.ProviderName == provider.ProviderName))
            return;

        _providers.Add(provider);
    }

    public IReadOnlyList<ISearchProvider> GetProviders()
    {
        return _providers.AsReadOnly();
    }

    public async Task<SearchResponse> SearchAsync(string query, int maxResultsPerProvider = 5)
    {
        Console.WriteLine($"[SearchService] Starting search for: '{query}'");

        if (string.IsNullOrWhiteSpace(query))
        {
            Console.WriteLine("[SearchService] Query is empty, returning empty response");
            return new SearchResponse
            {
                Groups = new List<SearchResultGroup>(),
                TotalCount = 0,
                SearchDuration = TimeSpan.Zero
            };
        }

        var stopwatch = Stopwatch.StartNew();

        // Search all enabled providers in parallel
        var enabledProviders = _providers.Where(p => p.IsEnabled).ToList();
        Console.WriteLine($"[SearchService] Found {enabledProviders.Count} enabled providers: {string.Join(", ", enabledProviders.Select(p => p.ProviderName))}");

        var searchTasks = enabledProviders.Select(p => SearchProviderAsync(p, query, maxResultsPerProvider));
        var results = await Task.WhenAll(searchTasks);

        Console.WriteLine($"[SearchService] Search completed. Results by provider:");
        foreach (var result in results)
        {
            Console.WriteLine($"  - {result.Type}: {result.Results.Count} results");
        }

        // Group results by type
        var groups = results
            .Where(r => r.Results.Any())
            .Select(r => new SearchResultGroup
            {
                Type = r.Type,
                Label = GetLabelForType(r.Type),
                Results = r.Results
            })
            .OrderBy(g => GetTypePriority(g.Type))
            .ToList();

        stopwatch.Stop();

        Console.WriteLine($"[SearchService] Returning {groups.Sum(g => g.Results.Count)} total results in {stopwatch.ElapsedMilliseconds}ms");

        return new SearchResponse
        {
            Groups = groups,
            TotalCount = groups.Sum(g => g.Results.Count),
            SearchDuration = stopwatch.Elapsed
        };
    }

    private async Task<(SearchResultType Type, List<SearchResult> Results)> SearchProviderAsync(
        ISearchProvider provider,
        string query,
        int maxResults)
    {
        try
        {
            Console.WriteLine($"[SearchService] Searching provider: {provider.ProviderName}");
            var results = await provider.SearchAsync(query, maxResults);
            Console.WriteLine($"[SearchService] Provider {provider.ProviderName} returned {results.Count} results");
            return (provider.ResultType, results);
        }
        catch (Exception ex)
        {
            // Log error but don't fail entire search
            Console.WriteLine($"[SearchService] ERROR in provider {provider.ProviderName}: {ex.Message}");
            Console.WriteLine($"[SearchService] Stack trace: {ex.StackTrace}");
            return (provider.ResultType, new List<SearchResult>());
        }
    }

    private static string GetLabelForType(SearchResultType type)
    {
        return type switch
        {
            SearchResultType.Item => "Items",
            SearchResultType.PurchaseOrder => "Purchase Orders",
            SearchResultType.Receipt => "Receipts",
            SearchResultType.Customer => "Customers",
            _ => "Other"
        };
    }

    private static int GetTypePriority(SearchResultType type)
    {
        // Order search results by relevance priority
        return type switch
        {
            SearchResultType.Item => 1,
            SearchResultType.PurchaseOrder => 2,
            SearchResultType.Receipt => 3,
            SearchResultType.Customer => 4,
            _ => 99
        };
    }
}