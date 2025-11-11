// Components/Layout/Navigation/Sidebar/ISidebarState.cs
namespace Prototype.Components.Layout.Navigation.Sidebar;

public interface ISidebarState
{
    IReadOnlyList<SidebarSection> Sections { get; }
    IReadOnlyDictionary<string, string?> SelectedBySection { get; }
    bool IsCollapsed { get; }

    Func<SidebarItem, Task>? ItemSelectedHandler { get; set; }

    event Action? StateChanged;

    void NotifyStateChanged();
    void SetSections(IEnumerable<SidebarSection> sections, IDictionary<string, string?>? initialSelections = null);
    void SetSelection(string sectionKey, string? selectedText);
    string? GetSelection(string sectionKey);
    void ResetAll(bool hide = true);
    void ToggleCollapsed();
    void SetCollapsed(bool collapsed);
}