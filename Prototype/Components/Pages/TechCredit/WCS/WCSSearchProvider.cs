// Components/Services/Search/TechCreditBorrowersSearchProvider.cs
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prototype.Components.Pages.Collections.TechCredit;
using static Prototype.Components.Pages.Collections.TechCredit.WCSSeedData;

namespace Prototype.Components.Services.Search;

/// <summary>
/// Search provider for TechCredit Borrowers
/// </summary>
public sealed class WCSSearchProvider : ISearchProvider
{
    public string ProviderName => "TechCredit Borrowers";
    public SearchResultType ResultType => SearchResultType.Customer;
    public bool IsEnabled => true;
    public string? RequiredPolicy => Prototype.Models.AppPolicies.TechCredit;

    private readonly List<BorrowerRecord> _borrowers;

    public WCSSearchProvider()
    {
        // Load seed data - in production this would come from a repository/service
        _borrowers = WCSSeedData.GetBorrowers();
    }

    public Task<List<SearchResult>> SearchAsync(string query, int maxResults = 10)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Task.FromResult(new List<SearchResult>());

        var searchTerm = query.ToLowerInvariant();

        var results = _borrowers
            .Where(borrower =>
                borrower.Borrower.ToLowerInvariant().Contains(searchTerm) ||
                borrower.BorrowerID.ToLowerInvariant().Contains(searchTerm) ||
                borrower.Employer.ToLowerInvariant().Contains(searchTerm) ||
                borrower.HomePhone.ToLowerInvariant().Contains(searchTerm) ||
                borrower.WorkPhone.ToLowerInvariant().Contains(searchTerm))
            .OrderBy(borrower =>
            {
                // Prioritize exact borrower ID matches
                if (borrower.BorrowerID.Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 0;
                // Then name matches
                if (borrower.Borrower.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 1;
                // Then phone matches
                if (borrower.HomePhone.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    return 2;
                return 3;
            })
            .ThenByDescending(borrower => borrower.CurrentBalance) // Higher balance first
            .Take(maxResults)
            .Select(borrower => new SearchResult
            {
                Id = borrower.BorrowerID,
                Title = borrower.Borrower,
                Subtitle = borrower.Employer,
                Type = SearchResultType.Customer,
                NavigateUrl = $"/techcredit/borrowers?borrower={Uri.EscapeDataString(borrower.BorrowerID)}",
                Price = borrower.CurrentBalance,
                Badge = GetBorrowerBadge(borrower),
                Description = $"Available: {borrower.AvailCredit:C} • Balance: {borrower.CurrentBalance:C}",
                Metadata = new Dictionary<string, string>
                {
                    ["BorrowerID"] = borrower.BorrowerID,
                    ["HomePhone"] = borrower.HomePhone,
                    ["WorkPhone"] = borrower.WorkPhone,
                    ["CurrentBalance"] = borrower.CurrentBalance.ToString("C"),
                    ["AvailCredit"] = borrower.AvailCredit.ToString("C"),
                    ["WeeklyPmt"] = borrower.WeeklyPmt.ToString("C"),
                    ["PastDue"] = borrower.AmtPastDue.ToString("C"),
                    ["Employer"] = borrower.Employer
                }
            })
            .ToList();

        return Task.FromResult(results);
    }

    private static string? GetBorrowerBadge(BorrowerRecord borrower)
    {
        // Prioritize past due status
        if (borrower.AmtPastDue > 0)
            return $"Past Due: {borrower.AmtPastDue:C}";
        
        // Show if credit limit is reached
        if (borrower.AvailCredit <= 0)
            return "Limit Reached";
        
        // Show low credit warning
        if (borrower.AvailCredit < 1000 && borrower.CurrentBalance > 0)
            return "Low Credit";
        
        // Show preferred rate
        if (borrower.PrefRate == "PF")
            return "Preferred";
        
        // Show transfer status
        if (!string.IsNullOrEmpty(borrower.XFR) && borrower.XFR.Contains("x"))
            return "Transfer";
        
        return null;
    }
}
