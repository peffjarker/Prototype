using Prototype.Components.Pages.Product.Webcat;
using Prototype.Components.Pages.POXfer.PO;
using Prototype.Components.Pages.Collections.TechCredit;
using static Prototype.Components.Pages.Product.Webcat.WebcatSeedData;
using static Prototype.Components.Pages.POXfer.PO.PurchaseOrdersSeedData;
using static Prototype.Components.Pages.Collections.TechCredit.TechCreditPaymentsSeedData;

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

    // Payment-related state
    private PaymentRecord? _currentPayment;
    private PaymentSidePanelMode _paymentMode;

    private string? _errorMessage;

    /// <summary>
    /// Event raised when the side panel state changes
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Event raised when a payment action is completed (for refreshing parent)
    /// </summary>
    public event Func<Task>? OnPaymentActionCompleted;

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
    /// Gets the current payment being displayed
    /// </summary>
    public PaymentRecord? CurrentPayment => _currentPayment;

    /// <summary>
    /// Gets the current payment sidebar mode
    /// </summary>
    public PaymentSidePanelMode PaymentMode => _paymentMode;

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
        _currentPayment = null;
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
        _currentPayment = null;
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
    /// Opens the side panel with payment details
    /// </summary>
    /// <param name="payment">The payment record to display</param>
    /// <param name="mode">The mode (Details, RecordPayment, LogContact)</param>
    public void OpenPaymentPanel(PaymentRecord payment, PaymentSidePanelMode mode = PaymentSidePanelMode.Details)
    {
        if (payment == null)
        {
            return;
        }

        _contentType = SidePanelContentType.Payment;
        _currentPayment = payment;
        _paymentMode = mode;
        _currentItemNumber = null;
        _currentItem = null;
        _currentPoNumber = null;
        _currentPo = null;
        _errorMessage = null;

        _isOpen = true;
        NotifyStateChanged();
    }

    /// <summary>
    /// Switches the payment panel to a different mode without closing
    /// </summary>
    /// <param name="mode">The new mode to switch to</param>
    public void SwitchPaymentMode(PaymentSidePanelMode mode)
    {
        if (_contentType != SidePanelContentType.Payment || _currentPayment == null)
        {
            return;
        }

        _paymentMode = mode;
        NotifyStateChanged();
    }

    /// <summary>
    /// Notifies that a payment action was completed and closes the panel
    /// </summary>
    public async Task CompletePaymentAction()
    {
        if (OnPaymentActionCompleted != null)
        {
            await OnPaymentActionCompleted.Invoke();
        }
        Close();
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
        _currentPayment = null;
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
    PurchaseOrder,
    Payment
}

/// <summary>
/// Enum representing the mode of the payment side panel
/// </summary>
public enum PaymentSidePanelMode
{
    Details,
    RecordPayment,
    LogContact
}