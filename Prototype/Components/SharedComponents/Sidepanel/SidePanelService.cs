using Prototype.Components.Pages.Product.Webcat;
using static Prototype.Components.Pages.Product.Webcat.WebcatSeedData;

namespace Prototype.Components.Services;

/// <summary>
/// Service for managing side panel state and content
/// </summary>
public class SidePanelService
{
    private bool _isOpen;
    private string? _currentItemNumber;
    private WebcatItem? _currentItem;
    private string? _errorMessage;

    /// <summary>
    /// Event raised when the side panel state changes
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Gets whether the side panel is currently open
    /// </summary>
    public bool IsOpen => _isOpen;

    /// <summary>
    /// Gets the current item number being displayed
    /// </summary>
    public string? CurrentItemNumber => _currentItemNumber;

    /// <summary>
    /// Gets the current item being displayed
    /// </summary>
    public WebcatItem? CurrentItem => _currentItem;

    /// <summary>
    /// Gets any error message to display
    /// </summary>
    public string? ErrorMessage => _errorMessage;

    /// <summary>
    /// Opens the side panel with an item preview
    /// </summary>
    /// <param name="itemNumber">The item number to display</param>
    public void OpenItemPreview(string itemNumber)
    {
        if (string.IsNullOrWhiteSpace(itemNumber))
        {
            return;
        }

        _currentItemNumber = itemNumber;
        _errorMessage = null;

        // Fetch item data
        var allItems = WebcatSeedData.GetItems();
        _currentItem = allItems.FirstOrDefault(x => 
            string.Equals(x.Item, itemNumber, StringComparison.OrdinalIgnoreCase));

        if (_currentItem == null)
        {
            _errorMessage = $"Item '{itemNumber}' not found in the catalog.";
        }

        _isOpen = true;
        NotifyStateChanged();
    }

    /// <summary>
    /// Closes the side panel
    /// </summary>
    public void Close()
    {
        _isOpen = false;
        _currentItemNumber = null;
        _currentItem = null;
        _errorMessage = null;
        NotifyStateChanged();
    }

    /// <summary>
    /// Notifies subscribers that the state has changed
    /// </summary>
    private void NotifyStateChanged() => OnChange?.Invoke();
}
