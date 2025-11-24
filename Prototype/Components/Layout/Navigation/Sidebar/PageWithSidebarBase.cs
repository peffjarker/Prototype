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
    private bool _isDisposed;
    private bool _isInitialized;
    private string? _myBasePath; // Cached to detect changes
    private string? _lastProcessedPath;
    private readonly string _pageTypeName; // For logging

    protected PageWithSidebarBase()
    {
        _pageTypeName = GetType().Name;
    }

    protected override void OnInitialized()
    {
        _myBasePath = BasePath; // Cache it

        Console.WriteLine($"[{_pageTypeName}] OnInitialized");
        Console.WriteLine($"  BasePath: {_myBasePath}");
        Console.WriteLine($"  Current URI: {Nav.Uri}");

        _itemSelectedHandler = HandleSidebarItemSelected;
        _sidebarStateChangedHandler = () => InvokeAsync(StateHasChanged);
        _locationChangedHandler = HandleLocationChanged;

        Sidebar.ItemSelectedHandler = _itemSelectedHandler;
        Sidebar.StateChanged += _sidebarStateChangedHandler!;
        Nav.LocationChanged += _locationChangedHandler;

        // CRITICAL: Only rebuild if we're actually on this page
        var currentPath = GetCurrentPathOnly(Nav);
        if (IsPathMatch(currentPath, _myBasePath))
        {
            Console.WriteLine($"  Path matches, initializing sidebar");
            ReadFromUrl();
            RebuildSidebar();
            _lastProcessedPath = currentPath;
        }
        else
        {
            Console.WriteLine($"  Path doesn't match, skipping initialization");
            Console.WriteLine($"    Current: {currentPath}");
            Console.WriteLine($"    Expected: {_myBasePath}");
        }

        _isInitialized = true;
        Console.WriteLine($"[{_pageTypeName}] Initialized");
    }

    public virtual void Dispose()
    {
        if (_isDisposed) return;

        Console.WriteLine($"[{_pageTypeName}] Disposing");
        _isDisposed = true;

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
        if (_isDisposed) return;

        Console.WriteLine($"[{_pageTypeName}] Sidebar item clicked: {item.Text}");

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
            Console.WriteLine($"  Navigating to: {item.Url}");
            Nav.NavigateTo(item.Url);
        }

        await Task.CompletedTask;
    }

    private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        Console.WriteLine($"[{_pageTypeName}] LocationChanged event received");
        Console.WriteLine($"   New location: {e.Location}");
        _ = HandleLocationChangedAsync();
    }

    private async Task HandleLocationChangedAsync()
    {
        if (_isDisposed)
        {
            Console.WriteLine($"   [{_pageTypeName}] Already disposed, ignoring");
            return;
        }

        if (!_isInitialized)
        {
            Console.WriteLine($"   [{_pageTypeName}] Not initialized yet, ignoring");
            return;
        }

        // Cancel any pending rebuild
        _rebuildCts?.Cancel();
        _rebuildCts?.Dispose();
        _rebuildCts = new CancellationTokenSource();
        var token = _rebuildCts.Token;

        try
        {
            var currentPath = GetCurrentPathOnly(Nav);

            Console.WriteLine($"   [{_pageTypeName}] Checking path ownership");
            Console.WriteLine($"      Current path: {currentPath}");
            Console.WriteLine($"      My BasePath:  {_myBasePath}");

            // CRITICAL CHECK: Is this page responsible for the current path?
            if (!IsPathMatch(currentPath, _myBasePath!))
            {
                Console.WriteLine($"   [{_pageTypeName}] Not my path, cleaning up and ignoring");

                // 🔧 NEW: Unsubscribe from location changes to prevent further interference
                if (_locationChangedHandler is not null)
                {
                    Nav.LocationChanged -= _locationChangedHandler;
                    _locationChangedHandler = null;
                }

                // 🔧 NEW: Clear sidebar handler if we still own it
                if (ReferenceEquals(Sidebar.ItemSelectedHandler, _itemSelectedHandler))
                {
                    Sidebar.ItemSelectedHandler = null;
                }

                _isDisposed = true; // Mark as disposed to prevent further processing
                return;
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"   [{_pageTypeName}] Operation cancelled");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"   [{_pageTypeName}] Error: {ex.Message}");
        }
    }

    protected void RebuildSidebar()
    {
        if (_isDisposed)
        {
            Console.WriteLine($"[{_pageTypeName}] RebuildSidebar called but disposed");
            return;
        }

        var currentPath = GetCurrentPathOnly(Nav);

        Console.WriteLine($"[{_pageTypeName}] RebuildSidebar");
        Console.WriteLine($"   Current path: {currentPath}");
        Console.WriteLine($"   BasePath:     {_myBasePath}");

        // Double-check path ownership (defensive)
        if (!IsPathMatch(currentPath, _myBasePath!))
        {
            Console.WriteLine($"   Path mismatch in RebuildSidebar! This shouldn't happen!");
            Console.WriteLine($"   Aborting rebuild");
            return;
        }

        Console.WriteLine($"   Building sidebar sections...");

        var sections = new List<SidebarSection>();

        var facetSections = FacetSections.Build(Facets(), Scalars, MultiValues, BasePath, Url);
        sections.AddRange(facetSections);

        var initialSelections = new Dictionary<string, string?>(Scalars, StringComparer.OrdinalIgnoreCase);

        if (Url.Has("dealer"))
            initialSelections["dealer"] = Url.Get("dealer");

        Console.WriteLine($"   Initial selections:");
        foreach (var kvp in initialSelections)
        {
            Console.WriteLine($"     {kvp.Key} = {kvp.Value}");
        }

        Sidebar.SetSections(sections, initialSelections);
        Console.WriteLine($"   Sidebar rebuilt successfully");
    }

    /// <summary>
    /// Check if the current path matches this page's base path.
    /// Handles both exact matches and proper prefix matches.
    /// </summary>
    private static bool IsPathMatch(string currentPath, string basePath)
    {
        if (string.IsNullOrEmpty(currentPath) || string.IsNullOrEmpty(basePath))
            return false;

        // Normalize paths
        var current = currentPath.TrimStart('/').ToLowerInvariant();
        var baseNormalized = basePath.TrimStart('/').ToLowerInvariant();

        // Exact match
        if (current == baseNormalized)
            return true;

        // Prefix match - but only if followed by a slash or end of string
        if (current.StartsWith(baseNormalized))
        {
            // Make sure we're not matching partial segments
            // e.g., "/techcredit" should match "/techcredit/customers" but not "/techcreditxxx"
            if (current.Length == baseNormalized.Length)
                return true;

            if (current.Length > baseNormalized.Length && current[baseNormalized.Length] == '/')
                return true;
        }

        return false;
    }

    public static string GetCurrentPathOnly(NavigationManager nav)
    {
        var abs = nav.ToAbsoluteUri(nav.Uri);
        var rel = nav.ToBaseRelativePath(abs.ToString());
        var cut = rel.IndexOfAny(new[] { '?', '#' });
        var path = cut >= 0 ? "/" + rel[..cut].TrimStart('/') : "/" + rel.TrimStart('/');
        return path;
    }
}