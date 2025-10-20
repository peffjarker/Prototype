// Sidebar/PageWithSidebarBase.cs
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities; // QueryHelpers
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
    protected abstract void OnSidebarClick(SidebarItem item);                    // page-specific handling

    // === URL behavior ===

    /// Keys that should always be preserved across navigation (sticky app-wide).
    /// Default keeps "dealer". Override to add more (e.g., "env", "asof").
    protected virtual IReadOnlyCollection<string> PreservedKeys => new[] { "dealer" };

    /// The query key name used to serialize MultiValues. Override per page if needed.
    protected virtual string MultiKeyName => "items";

    /// Separator used when collapsing multi values into one parameter (default comma).
    protected virtual string MultiSeparator => ",";

    protected void NavigateWithPageState(bool replace = false)
        => NavigateWithState(replace, Scalars, MultiKeyName, MultiValues);

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

        // Remove the multi key entirely first
        dict.Remove(multiKeyName);

        // Collapse multi values into a single "items=a,b,c" (stable order helps for UX/deeplinks)
        var list = multiValues?.ToList() ?? new List<string>();
        if (list.Count > 0)
        {
            // optional: keep insertion order; if you prefer alpha, use: list = list.OrderBy(x => x).ToList();
            dict[multiKeyName] = string.Join(MultiSeparator, list);
        }

        // Build final URL
        var url = QueryHelpers.AddQueryString(curUri.GetLeftPart(UriPartial.Path), dict);
        Nav.NavigateTo(url, replace: replace);
    }

    /// Builds a target href that carries current PreservedKeys (e.g., dealer).
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
                OnSidebarClick(item);
            }
            else if (!string.IsNullOrWhiteSpace(item.Url))
            {
                // default navigate but keep sticky keys (dealer, etc.)
                Nav.NavigateTo(AppendPreserved(item.Url));
            }
            await Task.CompletedTask;
        };

        _sidebarStateChangedHandler = () => InvokeAsync(StateHasChanged);

        _locationChangedHandler = (_, __) =>
        {
            ReadFromUrl();
            RebuildSidebar();
            _ = InvokeAsync(StateHasChanged);
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
