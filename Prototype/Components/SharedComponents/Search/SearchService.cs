using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
    Task<SearchResponse> SearchAsync(string query, string searchType = "All", int maxResultsPerProvider = 5);

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
    private readonly IAuthorizationService _authorizationService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public SearchService(
        IAuthorizationService authorizationService,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _authorizationService = authorizationService;
        _authenticationStateProvider = authenticationStateProvider;
    }

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

    public async Task<SearchResponse> SearchAsync(string query, string searchType = "All", int maxResultsPerProvider = 5)
    {
        Console.WriteLine($"[SearchService] Starting search for: '{query}' with type: '{searchType}'");

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

        // Get current user for authorization checks
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        // Get enabled providers, filter by search type, and filter by authorization
        var enabledProviders = _providers.Where(p => p.IsEnabled).ToList();
        var typeFilteredProviders = FilterProvidersBySearchType(enabledProviders, searchType);
        var authorizedProviders = await FilterProvidersByAuthorizationAsync(typeFilteredProviders, user);

        Console.WriteLine($"[SearchService] Found {authorizedProviders.Count} authorized providers matching '{searchType}': {string.Join(", ", authorizedProviders.Select(p => p.ProviderName))}");

        var searchTasks = authorizedProviders.Select(p => SearchProviderAsync(p, query, maxResultsPerProvider));
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

    private async Task<List<ISearchProvider>> FilterProvidersByAuthorizationAsync(List<ISearchProvider> providers, ClaimsPrincipal user)
    {
        var authorizedProviders = new List<ISearchProvider>();

        foreach (var provider in providers)
        {
            // If provider has no policy requirement, it's available to everyone
            if (string.IsNullOrEmpty(provider.RequiredPolicy))
            {
                authorizedProviders.Add(provider);
                continue;
            }

            // Check if user has authorization for this provider's policy
            var authResult = await _authorizationService.AuthorizeAsync(user, provider.RequiredPolicy);
            if (authResult.Succeeded)
            {
                authorizedProviders.Add(provider);
                Console.WriteLine($"[SearchService] User authorized for provider: {provider.ProviderName} (Policy: {provider.RequiredPolicy})");
            }
            else
            {
                Console.WriteLine($"[SearchService] User NOT authorized for provider: {provider.ProviderName} (Policy: {provider.RequiredPolicy})");
            }
        }

        return authorizedProviders;
    }

    private List<ISearchProvider> FilterProvidersBySearchType(List<ISearchProvider> providers, string searchType)
    {
        if (string.IsNullOrWhiteSpace(searchType) || searchType.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            return providers;
        }

        var targetTypes = GetResultTypesForSearchType(searchType);
        return providers.Where(p => targetTypes.Contains(p.ResultType)).ToList();
    }

    private HashSet<SearchResultType> GetResultTypesForSearchType(string searchType)
    {
        return searchType.ToLowerInvariant() switch
        {
            "items" => new HashSet<SearchResultType> { SearchResultType.Item },
            "pos" => new HashSet<SearchResultType> { SearchResultType.PurchaseOrder },
            "customers" => new HashSet<SearchResultType> { SearchResultType.Customer },
            "vendors" => new HashSet<SearchResultType> { SearchResultType.Vendor },
            "receipts" => new HashSet<SearchResultType> { SearchResultType.Receipt },
            _ => new HashSet<SearchResultType>() // Empty set for unknown types
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
            SearchResultType.Vendor => "Vendors",
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
            SearchResultType.Vendor => 5,
            _ => 99
        };
    }
}