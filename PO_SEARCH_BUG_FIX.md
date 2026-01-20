# Purchase Order Internal Search - Bug Fix Task

## Problem Statement
The Purchase Order Internal Search page (`/po-transfer/purchase-orders-internal`) has a non-functional search feature. When users enter search criteria and click the "Search" button, it returns "No purchase orders found" even though matching data exists. However, deep-linking via URL parameters works correctly.

## Expected Behavior
Users should be able to search for purchase orders using:

### Sales Order Search Mode:
- **Input**: Sales Order number (full or partial)
- **Example**: Entering "864" should find "SO-2024-10847" and its associated PO
- **Requirement**: Partial matching supported (user can search "864" instead of full "SO-2024-10847")

### Purchase Order Search Mode:
- **Input Option 1**: Dealer only (from dropdown) → Returns all POs for that dealer
- **Input Option 2**: PO number only (partial ok) → Returns matching POs across all dealers
- **Input Option 3**: Both dealer + PO number → Returns POs matching both criteria
- **Example**: Entering "807" should find "PO-0000100807"
- **Requirement**: Flexible search - either field can be used independently or together

## Current Behavior
- Deep-linking works: `/po-transfer/purchase-orders-internal?mode=po&dealer=88d9&po=807` correctly loads results
- Search panel fails: Same criteria entered via UI returns no results
- This indicates the backend search logic is correct, but parameter passing from UI to search handler is broken

## Root Cause Analysis
The issue is with Blazor component parameter binding. The text input fields use `@bind` with `@bind:event="oninput"`, but this does **NOT automatically invoke the corresponding EventCallback** handlers (`SalesOrderNumberChanged`, `PoNumberChanged`).

### What's Happening:
1. User types "807" into PO number field
2. Local `PoNumber` property updates in `PurchaseOrderSearchPanel` component
3. `PoNumberChanged.InvokeAsync()` is **NEVER called**
4. Parent component (`POInternal.razor`) never receives the value
5. `Params.PoNumber` remains null/empty
6. Search executes with empty parameters → no results

### Why Deep-linking Works:
URL parameters are read directly via `POInternalParameters.FromUrl()`, completely bypassing the search panel component and its broken bindings.

## Files Involved

### 1. `Components/Pages/POXfer/PO/PurchaseOrderSearchPanel.razor`
**Current Problem Lines 24-26 (SO input):**
```razor
<input id="so-input"
       type="text"
       placeholder="Enter sales order number (partial ok)"
       @bind="SalesOrderNumber"
       @bind:event="oninput" />
```

**Current Problem Lines 46-52 (PO input):**
```razor
<input id="po-input"
       type="text"
       placeholder="Enter PO number (partial ok)"
       @bind="PoNumber"
       @bind:event="oninput" />
```

**Dealer dropdown (already fixed, lines 33-42):**
```razor
<select id="dealer-select"
        value="@Dealer"
        @onchange="@(e => HandleDealerChange(e.Value?.ToString()))">
```
This was fixed to use explicit event handler that invokes the callback.

### 2. `Components/Pages/POXfer/PO/POInternal.razor`
**Lines 27-35** - Parent component bindings:
```razor
<PurchaseOrderSearchPanel Mode="@Params.Mode"
                          ModeChanged="@HandleModeChanged"
                          SalesOrderNumber="@Params.SalesOrder"
                          SalesOrderNumberChanged="@(value => { Params.SalesOrder = value; })"
                          Dealer="@Params.Dealer"
                          DealerChanged="@(value => { Params.Dealer = value; })"
                          PoNumber="@Params.PoNumber"
                          PoNumberChanged="@(value => { Params.PoNumber = value; })"
                          OnSearch="@HandleSearch" />
```

**Lines 440-485** - `HandleSearch()` method (works correctly, just needs proper params)

### 3. `Components/Pages/POXfer/PO/POSeedData.cs`
**Lines 33-72** - `SearchPurchaseOrdersBySalesOrderAsync()` (backend logic - working)
**Lines 75-126** - `SearchPurchaseOrdersByPoAsync()` (backend logic - working)

## Required Fix

### Step 1: Fix Sales Order Number Input
Replace `@bind` with explicit event handlers in `PurchaseOrderSearchPanel.razor`:

**Change lines 20-26 from:**
```razor
<input id="so-input"
       type="text"
       class="form-input"
       placeholder="Enter sales order number (partial ok)"
       @bind="SalesOrderNumber"
       @bind:event="oninput"
       @onkeydown="HandleKeyDown" />
```

**To:**
```razor
<input id="so-input"
       type="text"
       class="form-input"
       placeholder="Enter sales order number (partial ok)"
       value="@SalesOrderNumber"
       @oninput="@(e => HandleSalesOrderChange(e.Value?.ToString()))"
       @onkeydown="HandleKeyDown" />
```

### Step 2: Fix PO Number Input
**Change lines 46-52 from:**
```razor
<input id="po-input"
       type="text"
       class="form-input"
       placeholder="Enter PO number (partial ok)"
       @bind="PoNumber"
       @bind:event="oninput"
       @onkeydown="HandleKeyDown" />
```

**To:**
```razor
<input id="po-input"
       type="text"
       class="form-input"
       placeholder="Enter PO number (partial ok)"
       value="@PoNumber"
       @oninput="@(e => HandlePoNumberChange(e.Value?.ToString()))"
       @onkeydown="HandleKeyDown" />
```

### Step 3: Add Event Handler Methods
Add these methods to the `@code` section in `PurchaseOrderSearchPanel.razor` (after line 128):

```csharp
private async Task HandleSalesOrderChange(string? value)
{
    await SalesOrderNumberChanged.InvokeAsync(value);
}

private async Task HandlePoNumberChange(string? value)
{
    await PoNumberChanged.InvokeAsync(value);
}
```

Note: `HandleDealerChange()` already exists at line 125-128 and follows this same pattern.

## Testing Instructions

After implementing the fix, test these scenarios:

### Test 1: Sales Order Search (Partial Match)
1. Navigate to `/po-transfer/purchase-orders-internal`
2. Ensure "Sales Order Search" tab is active
3. Enter "10847" in the Sales Order # field
4. Click Search
5. **Expected**: Should find PO-0000100807 with SO-2024-10847

### Test 2: Purchase Order Search - PO Number Only
1. Switch to "Purchase Order Search" tab
2. Leave Dealer dropdown as "All Dealers"
3. Enter "807" in PO # field
4. Click Search
5. **Expected**: Should find PO-0000100807 (Adams Auto Parts)

### Test 3: Purchase Order Search - Dealer Only
1. Select "Adams Auto Parts (88d9)" from Dealer dropdown
2. Leave PO # field empty
3. Click Search
4. **Expected**: Should find all POs for Adams Auto Parts (PO-0000100802, PO-0000100807, PO-0000100830, PO-0000100895)

### Test 4: Purchase Order Search - Both Fields
1. Select "Adams Auto Parts (88d9)" from Dealer dropdown
2. Enter "807" in PO # field
3. Click Search
4. **Expected**: Should find PO-0000100807 (matches both criteria)

### Test 5: Verify Deep-linking Still Works
1. Navigate directly to: `/po-transfer/purchase-orders-internal?mode=po&dealer=88d9&po=807`
2. **Expected**: Should auto-execute search and show results (this already works, verify it still does)

### Test 6: Enter Key Functionality
1. Type search criteria
2. Press Enter instead of clicking Search button
3. **Expected**: Search should execute (verify `HandleKeyDown` still works with new binding)

## Technical Notes

### Why @bind Doesn't Work Here
In Blazor, `@bind` with `@bind:event` is designed for simple two-way binding on plain HTML elements. When used with component `[Parameter]` properties that have corresponding `EventCallback<T>` parameters (following the `{PropertyName}Changed` naming convention), the `@bind` directive does NOT automatically invoke the callback. This is by design - `@bind` updates the property, but you must manually invoke callbacks in component communication scenarios.

### The Solution Pattern
For parent-child component communication with two-way binding:
1. Use `value="@PropertyName"` instead of `@bind="PropertyName"`
2. Use `@oninput` or `@onchange` with an explicit handler
3. Handler must call `await PropertyNameChanged.InvokeAsync(newValue)`

This pattern is already correctly implemented for the dealer dropdown (lines 35-36) and just needs to be applied to the text inputs.

## Additional Context

### Sample Data Available for Testing
The seed data includes 18 POs across 7 dealers with various SO numbers. Key examples:
- PO-0000100802: Dealer 88d9 (Adams Auto Parts), SO-2024-10801
- PO-0000100807: Dealer 88d9 (Adams Auto Parts), SO-2024-10847
- PO-0000100805: Dealer b741 (Baker Supply Co), SO-2024-10824

### Backend Search Logic
Both search methods in `POSeedData.cs` already implement:
- Alphanumeric cleanup (removes special characters)
- Case-insensitive partial matching
- Flexible matching (searches within full number strings)

This is why deep-linking works - the backend is sound.

## Acceptance Criteria
- [ ] Sales Order search works with partial matching (e.g., "864" finds SO-2024-10847)
- [ ] PO search works with dealer only (returns all POs for selected dealer)
- [ ] PO search works with PO number only (returns matching POs across all dealers)
- [ ] PO search works with both fields (returns POs matching both criteria)
- [ ] Deep-linking continues to work as before
- [ ] Enter key triggers search (existing `HandleKeyDown` functionality preserved)
- [ ] No console errors or warnings
- [ ] Search button enables/disables correctly based on field validation

## Priority
**HIGH** - This is a critical feature that is completely non-functional in the UI, forcing users to manually construct deep-link URLs as a workaround.
