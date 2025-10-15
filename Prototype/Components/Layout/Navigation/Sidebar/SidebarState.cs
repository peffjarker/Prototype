using System.Collections.ObjectModel;

namespace Prototype.Components.Layout.Navigation.Sidebar;

/// <summary>
/// App-wide sidebar state. Reusable across many pages.
/// Register as Scoped in DI.
/// </summary>
public sealed class SidebarState : ISidebarState
{
    private readonly List<SidebarSection> _sections = new();
    private readonly Dictionary<string, string?> _selectedBySection =
        new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyList<SidebarSection> Sections => _sections;
    public IReadOnlyDictionary<string, string?> SelectedBySection =>
        new ReadOnlyDictionary<string, string?>(_selectedBySection);

    public Func<SidebarItem, Task>? ItemSelectedHandler { get; set; }
    public event Action? StateChanged;

    public void NotifyStateChanged() => StateChanged?.Invoke();

    public void SetSections(IEnumerable<SidebarSection> sections, IDictionary<string, string?>? initialSelections = null)
    {
        _sections.Clear();
        _sections.AddRange(sections?.Select(CloneSection) ?? Enumerable.Empty<SidebarSection>());

        _selectedBySection.Clear();
        if (initialSelections is not null)
        {
            foreach (var kv in initialSelections)
                _selectedBySection[kv.Key] = kv.Value;
        }

        // Reflect initial selections into item models (optional, for legacy Selected UI)
        foreach (var s in _sections)
        {
            if (s.IsLegend) continue;
            var key = s.SectionKey ?? s.Title ?? "";
            _selectedBySection.TryGetValue(key, out var selectedText);

            foreach (var it in s.Items)
            {
                it.Selected = selectedText != null &&
                              string.Equals(it.Text, selectedText, StringComparison.OrdinalIgnoreCase);
            }
        }

        StateChanged?.Invoke();
    }

    public void SetSelection(string sectionKey, string? selectedText)
    {
        if (string.IsNullOrWhiteSpace(sectionKey)) return;

        _selectedBySection[sectionKey] = selectedText;

        var target = _sections.FirstOrDefault(s =>
            string.Equals(s.SectionKey ?? s.Title, sectionKey, StringComparison.OrdinalIgnoreCase));

        if (target is not null && !target.IsLegend)
        {
            foreach (var it in target.Items)
                it.Selected = selectedText != null &&
                              string.Equals(it.Text, selectedText, StringComparison.OrdinalIgnoreCase);
        }

        StateChanged?.Invoke();
    }

    public string? GetSelection(string sectionKey)
        => _selectedBySection.TryGetValue(sectionKey, out var v) ? v : null;

    private static SidebarSection CloneSection(SidebarSection src)
        => new()
        {
            SectionKey = src.SectionKey, // NEW: stable key
            Title = src.Title,
            IsLegend = src.IsLegend,
            Items = src.Items.Select(CloneItem).ToList()
        };

    private static SidebarItem CloneItem(SidebarItem it)
        => new()
        {
            Text = it.Text,
            Key = it.Key,
            Icon = it.Icon,
            ColorHex = it.ColorHex,
            Selected = it.Selected,
            Url = it.Url
        };
}
