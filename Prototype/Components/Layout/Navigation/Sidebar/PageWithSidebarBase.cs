// Sidebar/PageWithSidebarBase.cs
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Prototype.Components.Layout.Navigation.Sidebar;

public abstract class PageWithSidebarBase : ComponentBase, IDisposable
{
    [Inject] protected NavigationManager Nav { get; set; } = default!;
    [Inject] protected ISidebarState Sidebar { get; set; } = default!;

    protected abstract string BasePath { get; }    // e.g. "/po-transfer/items-on-order"
    protected abstract IEnumerable<Facet> Facets(); // declare your schema
    protected virtual bool HandleMultiFacetClick => false; // let page override if it supports multi

    // Page supplies: current scalar map + multi-selection values (for one multi facet like "items")
    protected abstract IReadOnlyDictionary<string, string?> Scalars { get; }
    protected abstract IReadOnlyCollection<string> MultiValues { get; } // possibly empty
    protected abstract void ReadFromUrl();     // page parses Scalars into its fields
    protected abstract void OnSidebarClick(SidebarItem item); // optional page-specific handling

    protected override void OnInitialized()
    {
        Sidebar.ItemSelectedHandler = item =>
        {
            // default behavior: navigate to item.Url, or let page handle multi-facet clicks
            if (HandleMultiFacetClick) { OnSidebarClick(item); return Task.CompletedTask; }
            if (!string.IsNullOrWhiteSpace(item.Url)) Nav.NavigateTo(item.Url);
            return Task.CompletedTask;
        };

        Sidebar.StateChanged += () => InvokeAsync(StateHasChanged);
        Nav.LocationChanged += OnLocationChanged;
        ReadFromUrl();
        RebuildSidebar();
    }

    private void OnLocationChanged(object? s, LocationChangedEventArgs e)
    {
        ReadFromUrl();
        RebuildSidebar();
        InvokeAsync(StateHasChanged);
    }

    protected void RebuildSidebar()
    {
        var sections = FacetSections.Build(Facets(), Scalars, MultiValues, BasePath);
        Sidebar.SetSections(sections, new Dictionary<string, string?>(Scalars, StringComparer.OrdinalIgnoreCase));
    }

    public virtual void Dispose()
    {
        Sidebar.StateChanged -= () => InvokeAsync(StateHasChanged);
        if (ReferenceEquals(Sidebar.ItemSelectedHandler, (Func<SidebarItem, Task>)(item => { OnSidebarClick(item); return Task.CompletedTask; })))
            Sidebar.ItemSelectedHandler = null;
        Nav.LocationChanged -= OnLocationChanged;
    }
}
