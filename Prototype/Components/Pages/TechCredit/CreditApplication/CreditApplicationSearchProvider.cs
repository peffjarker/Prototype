// Components/Services/Search/CreditApplicationSearchProvider.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prototype.Components.Pages.Collections.CreditApplication;
using static Prototype.Components.Pages.Collections.CreditApplication.CreditApplicationSeedData;

namespace Prototype.Components.Services.Search;

/// <summary>
/// Search provider for Credit Applications
/// </summary>
public sealed class CreditApplicationSearchProvider : ISearchProvider
{
    public string ProviderName => "Credit Applications";
    public SearchResultType ResultType => SearchResultType.Customer;
    public bool IsEnabled => true;

    private readonly List<CreditApp> _applications;

    public CreditApplicationSearchProvider()
    {
        // Load seed data - in production this would come from a repository/service
        _applications = CreditApplicationSeedData.GetApplications();
    }

    public Task<List<SearchResult>> SearchAsync(string query, int maxResults = 10)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Task.FromResult(new List<SearchResult>());

        var searchTerm = query.ToLowerInvariant();

        var results = _applications
            .Where(app =>
                app.CustomerName.ToLowerInvariant().Contains(searchTerm) ||
                app.BorrowerId.ToLowerInvariant().Contains(searchTerm) ||
                app.Phone.ToLowerInvariant().Contains(searchTerm) ||
                app.CellPhone.ToLowerInvariant().Contains(searchTerm) ||
                app.CTCAppId.ToLowerInvariant().Contains(searchTerm) ||
                app.FirstName.ToLowerInvariant().Contains(searchTerm) ||
                app.LastName.ToLowerInvariant().Contains(searchTerm) ||
                app.Email.ToLowerInvariant().Contains(searchTerm))
            .OrderBy(app =>
            {
                // Prioritize exact borrower ID matches
                if (app.BorrowerId.Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 0;
                if (app.CustomerName.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 1;
                // Then by app ID matches
                if (app.CTCAppId.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 2;
                return 3;
            })
            .ThenByDescending(app => app.ApplicationDate)
            .Take(maxResults)
            .Select(app => new SearchResult
            {
                Id = app.BorrowerId,
                Title = app.CustomerName,
                Subtitle = $"App: {app.CTCAppId}",
                Type = SearchResultType.Customer,
                NavigateUrl = $"/techcredit/creditapplication?customer={Uri.EscapeDataString(app.BorrowerId)}&status=All",
                Price = app.CreditLimit,
                Badge = GetApplicationBadge(app),
                Description = $"{app.Decision} • Applied: {app.ApplicationDate:MM/dd/yyyy}",
                Metadata = new Dictionary<string, string>
                {
                    ["BorrowerId"] = app.BorrowerId,
                    ["Decision"] = app.Decision,
                    ["CreditLimit"] = app.CreditLimit.ToString("C"),
                    ["ApplicationDate"] = app.ApplicationDate.ToString("MM/dd/yyyy"),
                    ["Phone"] = app.Phone,
                    ["Email"] = app.Email
                }
            })
            .ToList();

        return Task.FromResult(results);
    }

    private static string? GetApplicationBadge(CreditApp app)
    {
        return app.Decision switch
        {
            "Approved" => "Approved",
            "Expired" => "Expired",
            "Denied" => "Denied",
            "Pending" => "Pending",
            _ => app.Decision
        };
    }
}