// Components/PageWithSidebarBase.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Prototype.Components.Layout.Navigation.Sidebar;
using Services;

/// <summary>
/// Base class for pages with sidebar navigation.
/// Centralizes URL state management and sidebar synchronization.
/// </summary>
public abstract class PageWithSidebarBase : ComponentBase, IDisposable
{
    [Inject] protected NavigationManager Nav { get; set; } = default!;
    [Inject] protected ISidebarState Sidebar { get; set; } = default!;
    [Inject] protected IFranchiseService FranchiseService { get; set; } = default!;
    [Inject] protected IUrlState Url { get; set; } = default!;

    // ===== Abstract Configuration =====

    /// <summary>Base path for this page (e.g., "/po-transfer/items-on-order")</summary>
    protected abstract string BasePath { get; }

    /// <summary>Define the facets (filters) for the sidebar</summary>
    protected abstract IEnumerable<Facet> Facets();

    /// <summary>Whether clicking multi-facet items triggers custom logic (vs. auto-navigation)</summary>
    protected virtual bool HandleMultiFacetClick => false;

    /// <summary>Whether to show the franchise selector in the sidebar</summary>
    protected virtual bool ShowFranchiseSelector => true;

    /// <summary>Query keys that should be preserved during navigation</summary>
    protected virtual IReadOnlyCollection<string> PreservedKeys => new[] { "dealer" };

    /// <summary>Query key name for multi-select values</summary>
    protected virtual string MultiKeyName => "items";

    /// <summary>Separator for multi-select values in URL</summary>
    protected virtual string MultiSeparator => ",";

    // ===== State Properties (derived from URL) =====

    /// <summary>Current scalar (single-value) query parameters</summary>
    protected abstract IReadOnlyDictionary<string, string?> Scalars { get; }

    /// <summary>Current multi-select values</summary>
    protected abstract IReadOnlyCollection<string> MultiValues { get; }

    /// <summary>Read URL into component state (called on init and URL changes)</summary>
    protected abstract void ReadFromUrl();

    /// <summary>Handle sidebar item clicks (for custom multi-facet logic)</summary>
    protected abstract void OnSidebarClick(SidebarItem item);

    // ===== Private State =====

    private Func<SidebarItem, Task>? _itemSelectedHandler;
    private Action? _sidebarStateChangedHandler;
    private EventHandler<LocationChangedEventArgs>? _locationChangedHandler;
    private CancellationTokenSource? _rebuildCts;
    private bool _isNavigating;

    // ===== Lifecycle =====

    protected override void OnInitialized()
    {
        _itemSelectedHandler = HandleSidebarItemSelected;
        _sidebarStateChangedHandler = () => InvokeAsync(StateHasChanged);
        _locationChangedHandler = (_, __) => { _ = HandleLocationChangedAsync(); };

        Sidebar.ItemSelectedHandler = _itemSelectedHandler;
        Sidebar.StateChanged += _sidebarStateChangedHandler!;
        Nav.LocationChanged += _locationChangedHandler;

        ReadFromUrl();
        RebuildSidebar();
    }

    public virtual void Dispose()
    {
        _rebuildCts?.Cancel();
        _rebuildCts?.Dispose();
        if (_sidebarStateChangedHandler is not null)
            Sidebar.StateChanged -= _sidebarStateChangedHandler;
        if (ReferenceEquals(Sidebar.ItemSelectedHandler, _itemSelectedHandler))
            Sidebar.ItemSelectedHandler = null;
        if (_locationChangedHandler is not null)
            Nav.LocationChanged -= _locationChangedHandler;
    }

    // ===== Navigation Helpers (simplified using UrlState) =====
    /// <summary>Navigate with current page state</summary>
    protected void NavigateWithPageState(bool replace = false)
    {
        var updates = new Dictionary<string, string?>(Scalars, StringComparer.OrdinalIgnoreCase);

        var multiList = MultiValues?.ToList() ?? new List<string>();
        if (multiList.Count > 0)
            updates[MultiKeyName] = string.Join(MultiSeparator, multiList);
        else
            updates[MultiKeyName] = null;

        if (replace)
            Url.Update(updates);
        else
            Url.Navigate(BasePath, updates);
    }

    /// <summary>Build href with preserved query parameters</summary>
    protected string AppendPreserved(string href)
    {
        var preservedValues = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        foreach (var key in PreservedKeys)
        {
            if (Url.Has(key))
                preservedValues[key] = Url.Get(key);
        }

        return Url.BuildHref(href, preservedValues, preserveCurrentState: false);
    }

    // ===== Franchise Selection =====

    public virtual void OnFranchiseSelected(string? dealerId)
    {
        if (string.IsNullOrWhiteSpace(dealerId))
        {
            Url.Set(("dealer", null));
        }
        else
        {
            var franchise = FranchiseService.FindByDealerId(dealerId);
            if (franchise != null)
                Url.Set(("dealer", franchise.DealerId));
        }
    }

    // ===== Sidebar Management =====

    private async Task HandleSidebarItemSelected(SidebarItem item)
    {
        // Handle franchise selection
        if (item.Key?.StartsWith("franchise:") == true)
        {
            var dealerId = item.Key.Substring("franchise:".Length);
            OnFranchiseSelected(dealerId);
            return;
        }

        // Handle multi-facet clicks
        if (HandleMultiFacetClick)
        {
            OnSidebarClick(item);
        }
        else if (!string.IsNullOrWhiteSpace(item.Url))
        {
            _isNavigating = true;
            Nav.NavigateTo(item.Url);
        }

        await Task.CompletedTask;
    }

    private async Task HandleLocationChangedAsync()
    {
        _rebuildCts?.Cancel();
        _rebuildCts?.Dispose();
        _rebuildCts = new CancellationTokenSource();
        var token = _rebuildCts.Token;

        try
        {
            if (!_isNavigating)
                await Task.Delay(25, token);

            _isNavigating = false;

            if (token.IsCancellationRequested) return;

            await InvokeAsync(() =>
            {
                if (!token.IsCancellationRequested)
                {
                    ReadFromUrl();
                    RebuildSidebar();
                    StateHasChanged();
                }
            });
        }
        catch (OperationCanceledException) { }
    }

    protected void RebuildSidebar()
    {
        // SAFETY CHECK: Ensure we're still on the correct path for this page
        var currentPath = GetCurrentPathOnly(Nav);
        if (!currentPath.StartsWith(BasePath, StringComparison.OrdinalIgnoreCase))
            return; // We've navigated away from this page, don't rebuild sidebar

        var sections = new List<SidebarSection>();

        // Add franchise selector section if enabled
        if (ShowFranchiseSelector)
        {
            sections.Add(new SidebarSection
            {
                SectionKey = "franchise",
                Title = "Franchise",
                IsFranchiseSelector = true,
                Items = new List<SidebarItem>()
            });
        }

        // Add facet sections (URLs will be built by FacetSections using current state)
        var facetSections = FacetSections.Build(Facets(), Scalars, MultiValues, BasePath);
        sections.AddRange(facetSections);

        // Build initial selections including dealer parameter
        var initialSelections = new Dictionary<string, string?>(Scalars, StringComparer.OrdinalIgnoreCase);

        if (Url.Has("dealer"))
            initialSelections["dealer"] = Url.Get("dealer");

        Sidebar.SetSections(sections, initialSelections);
    }

    private static string GetCurrentPathOnly(NavigationManager nav)
    {
        var abs = nav.ToAbsoluteUri(nav.Uri);
        var rel = nav.ToBaseRelativePath(abs.ToString());
        var cut = rel.IndexOfAny(new[] { '?', '#' });
        var path = cut >= 0 ? "/" + rel[..cut].TrimStart('/') : "/" + rel.TrimStart('/');
        return path;
    }
}