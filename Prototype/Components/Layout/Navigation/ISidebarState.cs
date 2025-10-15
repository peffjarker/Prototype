namespace Prototype.Components.Layout.Navigation;

public interface ISidebarState
{
    IReadOnlyList<SidebarSection> Sections { get; }
    IReadOnlyDictionary<string, string?> SelectedBySection { get; }
    Func<SidebarItem, Task>? ItemSelectedHandler { get; set; }
    event Action? StateChanged;

    void NotifyStateChanged();

    void SetSections(IEnumerable<SidebarSection> sections, IDictionary<string, string?>? initialSelections = null);

    /// <summary>Set the selection for a given section key (stable, not the display title).</summary>
    void SetSelection(string sectionKey, string? selectedText);

    /// <summary>Convenience: get the currently selected text for a given section key.</summary>
    string? GetSelection(string sectionKey);
}
