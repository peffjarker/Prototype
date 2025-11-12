using Prototype.Components.Pages.Product.Webcat;
using Prototype.Components.Pages.POXfer.PO;
using static Prototype.Components.Pages.Product.Webcat.WebcatSeedData;
using static Prototype.Components.Pages.POXfer.PO.PurchaseOrdersSeedData;

namespace Prototype.Components.Services;

/// <summary>
/// Service for managing side panel state and content
/// </summary>
public class SidePanelService
{
    private bool _isOpen;
    private SidePanelContentType _contentType;

    // Item-related state
    private string? _currentItemNumber;
    private WebcatItem? _currentItem;

    // PO-related state
    private string? _currentPoNumber;
    private PurchaseOrder? _currentPo;

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
    /// Gets the type of content currently displayed
    /// </summary>
    public SidePanelContentType ContentType => _contentType;

    /// <summary>
    /// Gets the current item number being displayed
    /// </summary>
    public string? CurrentItemNumber => _currentItemNumber;

    /// <summary>
    /// Gets the current item being displayed
    /// </summary>
    public WebcatItem? CurrentItem => _currentItem;

    /// <summary>
    /// Gets the current PO number being displayed
    /// </summary>
    public string? CurrentPoNumber => _currentPoNumber;

    /// <summary>
    /// Gets the current PO being displayed
    /// </summary>
    public PurchaseOrder? CurrentPo => _currentPo;

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

        _contentType = SidePanelContentType.Item;
        _currentItemNumber = itemNumber;
        _currentPoNumber = null;
        _currentPo = null;
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
    /// Opens the side panel with a PO preview
    /// </summary>
    /// <param name="poNumber">The PO number to display</param>
    /// <param name="status">Optional status filter for loading the PO</param>
    public void OpenPoPreview(string poNumber, string? status = null)
    {
        if (string.IsNullOrWhiteSpace(poNumber))
        {
            return;
        }

        _contentType = SidePanelContentType.PurchaseOrder;
        _currentPoNumber = poNumber;
        _currentItemNumber = null;
        _currentItem = null;
        _errorMessage = null;

        // Fetch PO data
        _currentPo = PurchaseOrdersSeedData.GetPoDetail(poNumber, status ?? "All");

        // Check if PO was actually found (has a number and has lines)
        if (_currentPo == null || string.IsNullOrEmpty(_currentPo.PoNumber) || _currentPo.Lines.Count == 0)
        {
            _errorMessage = $"Purchase Order '{poNumber}' not found.";
            _currentPo = null;
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
        _contentType = SidePanelContentType.None;
        _currentItemNumber = null;
        _currentItem = null;
        _currentPoNumber = null;
        _currentPo = null;
        _errorMessage = null;
        NotifyStateChanged();
    }

    /// <summary>
    /// Notifies subscribers that the state has changed
    /// </summary>
    private void NotifyStateChanged() => OnChange?.Invoke();
}

/// <summary>
/// Enum representing the type of content displayed in the side panel
/// </summary>
public enum SidePanelContentType
{
    None,
    Item,
    PurchaseOrder
}