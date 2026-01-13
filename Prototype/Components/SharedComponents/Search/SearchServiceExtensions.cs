// Components/Services/SearchServiceExtensions.cs
using Prototype.Components.Pages.POXfer.PO;
using Prototype.Components.Services.Search;

namespace Prototype.Components.Services;

/// <summary>
/// Extension methods for registering search services
/// </summary>
public static class SearchServiceExtensions
{
    /// <summary>
    /// Registers all search-related services and providers
    /// </summary>
    public static IServiceCollection AddSearchServices(this IServiceCollection services)
    {
        // Register the main search service as scoped (needs access to auth services)
        services.AddScoped<ISearchService>(sp =>
        {
            var authService = sp.GetRequiredService<Microsoft.AspNetCore.Authorization.IAuthorizationService>();
            var authStateProvider = sp.GetRequiredService<Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider>();
            var searchService = new SearchService(authService, authStateProvider);

            // Register all search providers
            searchService.RegisterProvider(new ItemsOnOrderSearchProvider());
            searchService.RegisterProvider(new POSearchProvider());

            // TechCredit/Collections providers
            searchService.RegisterProvider(new TechCreditSearchProvider());
            searchService.RegisterProvider(new CreditApplicationSearchProvider());
            searchService.RegisterProvider(new SalesAuthorizationsSearchProvider());

            // Add more providers here as they're created:
            // searchService.RegisterProvider(new ReceiptSearchProvider());

            return searchService;
        });

        return services;
    }
}