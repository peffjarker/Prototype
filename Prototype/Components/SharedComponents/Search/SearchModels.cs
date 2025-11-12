using System;
using System.Collections.Generic;

namespace Prototype.Components.Services.Search;

/// <summary>
/// Represents a single search result
/// </summary>
public sealed class SearchResult
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Subtitle { get; init; }
    public required SearchResultType Type { get; init; }
    public required string NavigateUrl { get; init; }
    public string? Description { get; init; }
    public string? Badge { get; init; }
    public decimal? Price { get; init; }
    public Dictionary<string, string> Metadata { get; init; } = new();
}

/// <summary>
/// Type of search result for categorization and display
/// </summary>
public enum SearchResultType
{
    Item,
    PurchaseOrder,
    Receipt,
    Customer,
    Unknown
}

/// <summary>
/// Groups search results by type
/// </summary>
public sealed class SearchResultGroup
{
    public required SearchResultType Type { get; init; }
    public required string Label { get; init; }
    public required List<SearchResult> Results { get; init; }
}

/// <summary>
/// Complete search response
/// </summary>
public sealed class SearchResponse
{
    public required List<SearchResultGroup> Groups { get; init; }
    public required int TotalCount { get; init; }
    public required TimeSpan SearchDuration { get; init; }
}

/// <summary>
/// Interface that data providers must implement to be searchable
/// </summary>
public interface ISearchProvider
{
    /// <summary>
    /// Name of this provider (e.g., "Items", "Purchase Orders")
    /// </summary>
    string ProviderName { get; }

    /// <summary>
    /// Type of results this provider returns
    /// </summary>
    SearchResultType ResultType { get; }

    /// <summary>
    /// Search this provider's data
    /// </summary>
    Task<List<SearchResult>> SearchAsync(string query, int maxResults = 10);

    /// <summary>
    /// Whether this provider is currently enabled
    /// </summary>
    bool IsEnabled { get; }
}