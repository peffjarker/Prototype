using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Prototype.Components.Layout.Navigation.Sidebar;
using Services;
using Telerik.SvgIcons;

public abstract class PageWithSidebarBase : ComponentBase, IDisposable
{
    [Inject] protected NavigationManager Nav { get; set; } = default!;
    [Inject] protected ISidebarState Sidebar { get; set; } = default!;
    [Inject] protected IFranchiseService FranchiseService { get; set; } = default!;
    [Inject] protected IUrlState Url { get; set; } = default!;

    protected abstract string BasePath { get; }
    protected abstract IEnumerable<Facet> Facets();
    protected virtual bool HandleMultiFacetClick => false;
    protected virtual bool ShowFranchiseSelector => true;

    protected abstract IReadOnlyDictionary<string, string?> Scalars { get; }
    protected abstract IReadOnlyCollection<string> MultiValues { get; }
    protected abstract void ReadFromUrl();
    protected abstract void OnSidebarClick(SidebarItem item);

    protected virtual IReadOnlyCollection<string> PreservedKeys => new[] { "dealer" };
    protected virtual string MultiKeyName => "items";
    protected virtual string MultiSeparator => ",";

    protected void NavigateWithPageState(bool replace = false)
        => NavigateWithState(replace, Scalars, MultiKeyName, MultiValues);

    protected void NavigateWithState(bool replace, IReadOnlyDictionary<string, string?> scalars, string multiKeyName, IEnumerable<string> multiValues)
    {
        var curUri = new Uri(Nav.Uri);
        var curQ = QueryHelpers.ParseQuery(curUri.Query);
        var dict = curQ.ToDictionary(kv => kv.Key, kv => kv.Value.ToString(), StringComparer.OrdinalIgnoreCase);

        foreach (var k in PreservedKeys)
            if (curQ.TryGetValue(k, out var v)) dict[k] = v.ToString();

        foreach (var (k, v) in scalars)
            if (string.IsNullOrWhiteSpace(v)) dict.Remove(k); else dict[k] = v;

        dict.Remove(multiKeyName);
        var list = multiValues?.ToList() ?? new List<string>();
        if (list.Count > 0) dict[multiKeyName] = string.Join(MultiSeparator, list);

        var url = QueryHelpers.AddQueryString(curUri.GetLeftPart(UriPartial.Path), dict);
        Nav.NavigateTo(url, replace: replace);
    }

    protected string AppendPreserved(string href)
    {
        var abs = href.StartsWith("http", StringComparison.OrdinalIgnoreCase) ? href : Nav.ToAbsoluteUri(href).ToString();
        var target = new Uri(abs);
        var targetQ = QueryHelpers.ParseQuery(target.Query).ToDictionary(kv => kv.Key, kv => kv.Value.ToString(), StringComparer.OrdinalIgnoreCase);
        var curQ = QueryHelpers.ParseQuery(new Uri(Nav.Uri).Query);

        foreach (var k in PreservedKeys)
            if (!targetQ.ContainsKey(k) && curQ.TryGetValue(k, out var v))
                targetQ[k] = v.ToString();

        var with = QueryHelpers.AddQueryString(target.GetLeftPart(UriPartial.Path), targetQ);
        var rel = Nav.ToBaseRelativePath(with);
        return "/" + rel.TrimStart('/');
    }

    private Func<SidebarItem, Task>? _itemSelectedHandler;
    private Action? _sidebarStateChangedHandler;
    private EventHandler<LocationChangedEventArgs>? _locationChangedHandler;
    private CancellationTokenSource? _rebuildCts;
    private bool _isNavigating;

    protected override void OnInitialized()
    {
        _itemSelectedHandler = async item =>
        {
            if (item.Key?.StartsWith("franchise:") == true)
            {
                var dealerId = item.Key.Substring("franchise:".Length);
                OnFranchiseSelected(dealerId);
                return;
            }

            if (HandleMultiFacetClick) OnSidebarClick(item);
            else if (!string.IsNullOrWhiteSpace(item.Url))
            {
                _isNavigating = true;
                Nav.NavigateTo(AppendPreserved(item.Url));
            }
            await Task.CompletedTask;
        };

        _sidebarStateChangedHandler = () => InvokeAsync(StateHasChanged);
        _locationChangedHandler = (_, __) => { _ = HandleLocationChangedAsync(); };

        Sidebar.ItemSelectedHandler = _itemSelectedHandler;
        Sidebar.StateChanged += _sidebarStateChangedHandler!;
        Nav.LocationChanged += _locationChangedHandler;

        ReadFromUrl();
        RebuildSidebar();
    }

    public virtual void OnFranchiseSelected(string? dealerId)
    {
        if (string.IsNullOrWhiteSpace(dealerId)) Url.Set(("dealer", null));
        else
        {
            var franchise = FranchiseService.FindByDealerId(dealerId);
            if (franchise != null) Url.Set(("dealer", franchise.DealerId));
        }
    }

    private async Task HandleLocationChangedAsync()
    {
        _rebuildCts?.Cancel();
        _rebuildCts?.Dispose();
        _rebuildCts = new CancellationTokenSource();
        var token = _rebuildCts.Token;

        try
        {
            if (!_isNavigating) await Task.Delay(25, token);
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
        var sections = new List<SidebarSection>();

        // Add franchise selector section if enabled
        if (ShowFranchiseSelector)
        {
            var franchiseSection = new SidebarSection
            {
                SectionKey = "franchise",
                Title = "Franchise",
                IsFranchiseSelector = true,
                Items = new List<SidebarItem>()
            };
            sections.Add(franchiseSection);
        }

        // Add facet sections
        var facetSections = FacetSections.Build(Facets(), Scalars, MultiValues, BasePath);
        sections.AddRange(facetSections);

        // Build initial selections including dealer parameter
        var initialSelections = new Dictionary<string, string?>(Scalars, StringComparer.OrdinalIgnoreCase);

        // Get dealer from URL if present
        var uri = new Uri(Nav.Uri);
        var query = QueryHelpers.ParseQuery(uri.Query);
        if (query.TryGetValue("dealer", out var dealerValue) && !string.IsNullOrWhiteSpace(dealerValue))
        {
            initialSelections["dealer"] = dealerValue.ToString();
        }

        Sidebar.SetSections(sections, initialSelections);
    }

    public virtual void Dispose()
    {
        _rebuildCts?.Cancel();
        _rebuildCts?.Dispose();
        if (_sidebarStateChangedHandler is not null) Sidebar.StateChanged -= _sidebarStateChangedHandler;
        if (ReferenceEquals(Sidebar.ItemSelectedHandler, _itemSelectedHandler)) Sidebar.ItemSelectedHandler = null;
        if (_locationChangedHandler is not null) Nav.LocationChanged -= _locationChangedHandler;
    }
}