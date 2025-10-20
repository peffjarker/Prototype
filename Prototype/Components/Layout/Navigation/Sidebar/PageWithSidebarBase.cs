// Sidebar/PageWithSidebarBase.cs
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities; // for QueryHelpers
using Prototype.Components.Layout.Navigation.Sidebar;

public abstract class PageWithSidebarBase : ComponentBase, IDisposable
{
    [Inject] protected NavigationManager Nav { get; set; } = default!;
    [Inject] protected ISidebarState Sidebar { get; set; } = default!;

    // === Page contract ===
    protected abstract string BasePath { get; }                 // e.g. "/po-transfer/items-on-order"
    protected abstract IEnumerable<Facet> Facets();             // declare your sidebar schema
    protected virtual bool HandleMultiFacetClick => false;      // page handles multi facet clicks itself?

    // Pages declare current URL state:
    protected abstract IReadOnlyDictionary<string, string?> Scalars { get; }     // singletons
    protected abstract IReadOnlyCollection<string> MultiValues { get; }          // repeatable values
    protected abstract void ReadFromUrl();                                       // parse URL -> page fields
    protected abstract void OnSidebarClick(SidebarItem item);                    // page-specific handling (optional)

    // === NEW: centralized URL helpers ===

    /// <summary>
    /// Keys that should always be preserved across navigation (sticky app-wide).
    /// Default keeps "dealer". Override to add more (e.g., "env", "asof").
    /// </summary>
    protected virtual IReadOnlyCollection<string> PreservedKeys => new[] { "dealer" };

    /// <summary>
    /// The query key name used to serialize <see cref="MultiValues"/>. Override per page if needed.
    /// </summary>
    // Default name for repeatable values (your pages can override)
    protected virtual string MultiKeyName => "items";

    protected void NavigateWithPageState(bool replace = false)
    {
        NavigateWithState(
            replace: replace,
            scalars: Scalars,
            multiKeyName: MultiKeyName,
            multiValues: MultiValues // may be empty — we'll still purge the key
        );
    }

    protected void NavigateWithState(
        bool replace,
        IReadOnlyDictionary<string, string?> scalars,
        string multiKeyName,
        IEnumerable<string> multiValues)
    {
        var curUri = new Uri(Nav.Uri);
        var curQ = QueryHelpers.ParseQuery(curUri.Query);

        // Start from current query so unrelated keys survive
        var dict = curQ.ToDictionary(kv => kv.Key, kv => kv.Value.ToString(), StringComparer.OrdinalIgnoreCase);

        // Preserve sticky keys (dealer, etc.)
        foreach (var k in PreservedKeys)
            if (curQ.TryGetValue(k, out var v)) dict[k] = v.ToString();

        // Apply scalars (null/empty removes)
        foreach (var (k, v) in scalars)
            if (string.IsNullOrWhiteSpace(v)) dict.Remove(k); else dict[k] = v;

        // IMPORTANT: always remove the multi key entirely before re-adding,
        // so clearing the selection actually clears the query string.
        dict.Remove(multiKeyName);

        // Build base URL with updated scalars
        var url = QueryHelpers.AddQueryString(curUri.GetLeftPart(UriPartial.Path), dict);

        // Append current multi values (if any)
        foreach (var v in multiValues)
            url = QueryHelpers.AddQueryString(url, multiKeyName, v);

        Nav.NavigateTo(url, replace: replace);
    }


    /// <summary>
    /// Builds a target href that carries current <see cref="PreservedKeys"/> (e.g., dealer).
    /// Use for facets/links so you don't hand-roll sticky params.
    /// </summary>
    protected string AppendPreserved(string href)
    {
        var abs = href.StartsWith("http", StringComparison.OrdinalIgnoreCase)
            ? href
            : Nav.ToAbsoluteUri(href).ToString();

        var target = new Uri(abs);
        var targetQ = QueryHelpers.ParseQuery(target.Query)
            .ToDictionary(kv => kv.Key, kv => kv.Value.ToString(), StringComparer.OrdinalIgnoreCase);

        var curQ = QueryHelpers.ParseQuery(new Uri(Nav.Uri).Query);

        foreach (var k in PreservedKeys)
            if (!targetQ.ContainsKey(k) && curQ.TryGetValue(k, out var v))
                targetQ[k] = v.ToString();

        var with = QueryHelpers.AddQueryString(target.GetLeftPart(UriPartial.Path), targetQ);
        var rel = Nav.ToBaseRelativePath(with);
        return "/" + rel.TrimStart('/');
    }

    // === Sidebar + lifecycle wiring ===

    private Func<SidebarItem, Task>? _itemSelectedHandler;
    private Action? _sidebarStateChangedHandler;
    private EventHandler<LocationChangedEventArgs>? _locationChangedHandler;

    protected override void OnInitialized()
    {
        _itemSelectedHandler = async item =>
        {
            if (HandleMultiFacetClick)
            {
                OnSidebarClick(item); // page handles multi facet behavior
                return;
            }

            if (!string.IsNullOrWhiteSpace(item.Url))
            {
                // default navigate but keep sticky keys (dealer, etc.)
                Nav.NavigateTo(AppendPreserved(item.Url));
            }
        };

        _sidebarStateChangedHandler = () => InvokeAsync(StateHasChanged);
        _locationChangedHandler = (_, __) =>
        {
            ReadFromUrl();
            RebuildSidebar();
            InvokeAsync(StateHasChanged);
        };

        Sidebar.ItemSelectedHandler = _itemSelectedHandler;
        Sidebar.StateChanged += _sidebarStateChangedHandler!;
        Nav.LocationChanged += _locationChangedHandler;

        ReadFromUrl();
        RebuildSidebar();
    }

    protected void RebuildSidebar()
    {
        var sections = FacetSections.Build(Facets(), Scalars, MultiValues, BasePath);
        Sidebar.SetSections(sections, new Dictionary<string, string?>(Scalars, StringComparer.OrdinalIgnoreCase));
    }

    public virtual void Dispose()
    {
        if (_sidebarStateChangedHandler is not null)
            Sidebar.StateChanged -= _sidebarStateChangedHandler;

        if (ReferenceEquals(Sidebar.ItemSelectedHandler, _itemSelectedHandler))
            Sidebar.ItemSelectedHandler = null;

        if (_locationChangedHandler is not null)
            Nav.LocationChanged -= _locationChangedHandler;
    }
}
