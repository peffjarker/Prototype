// Components/Services/Search/SalesAuthorizationsSearchProvider.cs
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prototype.Components.Pages.Collections.SalesAuthorizations;
using static Prototype.Components.Pages.Collections.SalesAuthorizations.SalesAuthorizationsSeedData;

namespace Prototype.Components.Services.Search;

/// <summary>
/// Search provider for Sales Authorizations
/// </summary>
public sealed class SalesAuthorizationsSearchProvider : ISearchProvider
{
    public string ProviderName => "Sales Authorizations";
    public SearchResultType ResultType => SearchResultType.Customer;
    public bool IsEnabled => true;
    public string? RequiredPolicy => AppPolicies.TechCredit;

    private readonly List<SalesAuth> _salesAuths;

    public SalesAuthorizationsSearchProvider()
    {
        // Load seed data - in production this would come from a repository/service
        _salesAuths = SalesAuthorizationsSeedData.GetSalesAuthorizations();
    }

    public Task<List<SearchResult>> SearchAsync(string query, int maxResults = 10)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Task.FromResult(new List<SearchResult>());

        var searchTerm = query.ToLowerInvariant();

        var results = _salesAuths
            .Where(auth =>
                auth.CustomerName.ToLowerInvariant().Contains(searchTerm) ||
                auth.CustomerId.ToLowerInvariant().Contains(searchTerm) ||
                auth.Phone.ToLowerInvariant().Contains(searchTerm) ||
                auth.IBNSalesAuthNumber.ToLowerInvariant().Contains(searchTerm) ||
                auth.CQTSalesAuthNumber.ToLowerInvariant().Contains(searchTerm))
            .OrderBy(auth =>
            {
                // Prioritize exact customer ID matches
                if (auth.CustomerId.Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 0;
                if (auth.CustomerName.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 1;
                // Then by auth number matches
                if (auth.IBNSalesAuthNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 2;
                return 3;
            })
            .ThenByDescending(auth => auth.IBNDate)
            .Take(maxResults)
            .Select(auth => new SearchResult
            {
                Id = auth.CustomerId,
                Title = auth.CustomerName,
                Subtitle = $"Auth: {auth.IBNSalesAuthNumber}",
                Type = SearchResultType.Customer,
                NavigateUrl = $"/techcredit/salesauthorizations?customer={Uri.EscapeDataString(auth.CustomerId)}&status=All",
                Price = auth.NewCTCBalance,
                Badge = GetAuthBadge(auth),
                Description = $"{auth.Status} • {auth.IBNDate:MM/dd/yyyy} • APR: {auth.APR:F2}%",
                Metadata = new Dictionary<string, string>
                {
                    ["CustomerId"] = auth.CustomerId,
                    ["Status"] = auth.Status,
                    ["Balance"] = auth.NewCTCBalance.ToString("C"),
                    ["Phone"] = auth.Phone,
                    ["Date"] = auth.IBNDate.ToString("MM/dd/yyyy")
                }
            })
            .ToList();

        return Task.FromResult(results);
    }

    private static string? GetAuthBadge(SalesAuth auth)
    {
        if (auth.Status == "Reversed") return "Reversed";
        if (auth.IsPastDue) return "Past Due";
        if (auth.Status == "Pending") return "Pending";
        if (auth.Status == "Processed") return "Processed";
        return null;
    }
}