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

public abstract class PageWithSidebarBase : ComponentBase, IDisposable
{
    [Inject] protected NavigationManager Nav { get; set; } = default!;
    [Inject] protected ISidebarState Sidebar { get; set; } = default!;
    [Inject] protected IFranchiseService FranchiseService { get; set; } = default!;
    [Inject] protected IUrlState Url { get; set; } = default!;

    protected abstract string BasePath { get; }
    protected abstract IEnumerable<Facet> Facets();
    protected virtual IReadOnlyCollection<string> AlwaysPreservedKeys => new[] { "dealer" };
    protected virtual IReadOnlyCollection<string> SamePagePreservedKeys => Array.Empty<string>();
    protected virtual IReadOnlyCollection<string> SameModulePreservedKeys => Array.Empty<string>();
    protected virtual string MultiKeyName => "items";
    protected virtual string MultiSeparator => ",";

    protected abstract IReadOnlyDictionary<string, string?> Scalars { get; }
    protected abstract IReadOnlyCollection<string> MultiValues { get; }
    protected abstract void ReadFromUrl();

    private Func<SidebarItem, Task>? _itemSelectedHandler;
    private Action? _sidebarStateChangedHandler;
    private EventHandler<LocationChangedEventArgs>? _locationChangedHandler;
    private CancellationTokenSource? _rebuildCts;
    private bool _isNavigating;

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

    protected string AppendPreserved(string href, bool crossModule = false)
    {
        var preserved = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        foreach (var key in AlwaysPreservedKeys)
        {
            if (Url.Has(key))
                preserved[key] = Url.Get(key);
        }

        if (!crossModule)
        {
            foreach (var key in SameModulePreservedKeys)
            {
                if (Url.Has(key))
                    preserved[key] = Url.Get(key);
            }
        }

        return Url.BuildHref(href, preserved, preserveCurrentState: false);
    }

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

    protected Dictionary<string, string?> ValidateFacetDependencies(
        IReadOnlyDictionary<string, string?> proposedScalars)
    {
        var result = new Dictionary<string, string?>(proposedScalars, StringComparer.OrdinalIgnoreCase);
        var facetList = Facets().ToList();

        foreach (var facet in facetList.Where(f => f.DependsOn != null))
        {
            var parentKey = facet.DependsOn!;
            var parentValue = result.TryGetValue(parentKey, out var pv) ? pv : null;

            if (facet.IsValidForParent != null && !facet.IsValidForParent(parentValue))
            {
                result[facet.Key] = null;
                continue;
            }

            if (result.TryGetValue(facet.Key, out var currentVal) && !string.IsNullOrEmpty(currentVal))
            {
                var validOptions = facet.Options().Select(o => o.Value).ToHashSet(StringComparer.OrdinalIgnoreCase);
                if (!validOptions.Contains(currentVal))
                {
                    result[facet.Key] = null;
                }
            }
        }

        return result;
    }

    private async Task HandleSidebarItemSelected(SidebarItem item)
    {
        if (item.Key?.StartsWith("franchise:") == true)
        {
            var dealerId = item.Key.Substring("franchise:".Length);
            OnFranchiseSelected(dealerId);
            return;
        }

        var facet = Facets().FirstOrDefault(f =>
            f.IsMulti && f.Options().Any(o => o.Value == item.Key));

        if (facet != null)
        {
            Url.Toggle(facet.Key, item.Key!, MultiSeparator);
            return;
        }

        if (!string.IsNullOrWhiteSpace(item.Url))
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
        var currentPath = GetCurrentPathOnly(Nav);

        Console.WriteLine($"=== RebuildSidebar for {BasePath} ===");
        Console.WriteLine($"Current path: {currentPath}");
        Console.WriteLine($"BasePath: {BasePath}");

        if (!currentPath.StartsWith(BasePath, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Path doesn't match BasePath, returning");
            return;
        }

        var sections = new List<SidebarSection>();

        var facetSections = FacetSections.Build(Facets(), Scalars, MultiValues, BasePath, Url);
        sections.AddRange(facetSections);

        var initialSelections = new Dictionary<string, string?>(Scalars, StringComparer.OrdinalIgnoreCase);

        if (Url.Has("dealer"))
            initialSelections["dealer"] = Url.Get("dealer");

        Console.WriteLine("Initial selections for sidebar:");
        foreach (var kvp in initialSelections)
        {
            Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
        }
        Console.WriteLine("=================");

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