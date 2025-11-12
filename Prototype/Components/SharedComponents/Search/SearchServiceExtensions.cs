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
        // Register the main search service as singleton
        services.AddSingleton<ISearchService>(sp =>
        {
            var searchService = new SearchService();

            // Register all search providers
            searchService.RegisterProvider(new ItemsOnOrderSearchProvider());
            searchService.RegisterProvider(new ItemsOnOrderSearchProvider()); // NEW
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