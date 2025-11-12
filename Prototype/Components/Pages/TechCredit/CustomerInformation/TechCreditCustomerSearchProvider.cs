// Components/Services/Search/TechCreditSearchProvider.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prototype.Components.Pages.Collections.TechCredit;
using static Prototype.Components.Pages.Collections.TechCredit.CustomerInformationSeedData;

namespace Prototype.Components.Services.Search;

/// <summary>
/// Search provider for TechCredit customer information
/// </summary>
public sealed class TechCreditSearchProvider : ISearchProvider
{
    public string ProviderName => "TechCredit Customers";
    public SearchResultType ResultType => SearchResultType.Customer;
    public bool IsEnabled => true;

    private readonly List<CustomerInfo> _customers;

    public TechCreditSearchProvider()
    {
        // Load seed data - in production this would come from a repository/service
        _customers = CustomerInformationSeedData.GetCustomers();
    }

    public Task<List<SearchResult>> SearchAsync(string query, int maxResults = 10)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Task.FromResult(new List<SearchResult>());

        var searchTerm = query.ToLowerInvariant();

        var results = _customers
            .Where(cust =>
                cust.Name.ToLowerInvariant().Contains(searchTerm) ||
                cust.CustomerId.ToLowerInvariant().Contains(searchTerm) ||
                cust.Phone.ToLowerInvariant().Contains(searchTerm) ||
                cust.Profile.ToLowerInvariant().Contains(searchTerm))
            .OrderBy(cust =>
            {
                // Prioritize exact customer ID matches
                if (cust.CustomerId.Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 0;
                if (cust.Name.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 1;
                return 2;
            })
            .ThenByDescending(cust => cust.Balance) // Highest balance first for secondary sort
            .Take(maxResults)
            .Select(cust => new SearchResult
            {
                Id = cust.CustomerId,
                Title = cust.Name,
                Subtitle = $"ID: {cust.CustomerId}",
                Type = SearchResultType.Customer,
                NavigateUrl = $"/techcredit/customerinformation?customer={Uri.EscapeDataString(cust.CustomerId)}&status=All",
                Price = cust.Balance,
                Badge = GetCustomerBadge(cust),
                Description = $"Credit Limit: {cust.CreditLimit:C} • Available: {cust.AvailableCredit:C}",
                Metadata = new Dictionary<string, string>
                {
                    ["CustomerId"] = cust.CustomerId,
                    ["Balance"] = cust.Balance.ToString("C"),
                    ["CreditLimit"] = cust.CreditLimit.ToString("C"),
                    ["AvailableCredit"] = cust.AvailableCredit.ToString("C"),
                    ["Phone"] = cust.Phone,
                    ["IsPastDue"] = cust.IsPastDue.ToString()
                }
            })
            .ToList();

        return Task.FromResult(results);
    }

    private static string? GetCustomerBadge(CustomerInfo cust)
    {
        if (cust.IsPastDue && cust.PastDue > 0) return $"Past Due: {cust.PastDue:C}";
        if (cust.AvailableCredit <= 0) return "Limit Reached";
        if (cust.AvailableCredit < cust.CreditLimit * 0.1m) return "Low Credit";
        if (cust.IsTCBorrower) return "TC Borrower";
        return null;
    }
}