namespace Prototype.Components.Pages.Collections.SalesAuthorizations;

public class SalesAuthorizationsSeedData
{
    public static List<SalesAuth> GetSalesAuthorizations()
    {
        return new List<SalesAuth>
        {
            new SalesAuth
            {
                CustomerId = "0000736262",
                CustomerName = "ALLTOP, TIMOTHY C",
                Phone = "419-685-4759",
                CustomerProfile = "0-0-0",
                ThisSale = 2450.00m,
                RolloverAmount = 0.00m,
                LessTradeIn = 150.00m,
                SalesTax = 147.00m,
                AdminFee = 25.00m,
                PreviousBalance = 1200.00m,
                NewCTCBalance = 3672.00m,
                IBNSalesAuthNumber = "SA-2024-001",
                IBNDate = new DateTime(2024, 10, 15),
                CQTSalesAuthNumber = "CQT-2024-045",
                NetSale = 2300.00m,
                CQTDate = new DateTime(2024, 10, 15),
                WeeklyPayAmt = 85.00m,
                MonthlyPayAmt = 368.00m,
                FirstPayment = new DateTime(2024, 10, 22),
                APR = 18.5m,
                Status = "Processed",
                WhyPmt = 43,
                IsTCBorrower = true,
                IsPastDue = false,
                Items = new List<SalesAuthItem>
                {
                    new SalesAuthItem { ItemId = "CXTW12", Description = "12\" Adjustable Wrench", Qty = 2, Price = 45.99m },
                    new SalesAuthItem { ItemId = "CXTW24", Description = "24\" Pipe Wrench", Qty = 1, Price = 89.99m },
                    new SalesAuthItem { ItemId = "SOCKET120", Description = "120pc Socket Set", Qty = 1, Price = 299.99m },
                    new SalesAuthItem { ItemId = "DRILL18V", Description = "18V Cordless Drill Kit", Qty = 1, Price = 179.99m }
                }
            },
            new SalesAuth
            {
                CustomerId = "0000745891",
                CustomerName = "AULT, JOSEPH",
                Phone = "330-555-0123",
                CustomerProfile = "1-2-0",
                ThisSale = 1850.00m,
                RolloverAmount = 350.00m,
                LessTradeIn = 0.00m,
                SalesTax = 132.00m,
                AdminFee = 25.00m,
                PreviousBalance = 0.00m,
                NewCTCBalance = 2357.00m,
                IBNSalesAuthNumber = "SA-2024-002",
                IBNDate = new DateTime(2024, 10, 12),
                CQTSalesAuthNumber = "CQT-2024-044",
                NetSale = 2200.00m,
                CQTDate = new DateTime(2024, 10, 12),
                WeeklyPayAmt = 65.00m,
                MonthlyPayAmt = 282.00m,
                FirstPayment = new DateTime(2024, 10, 19),
                APR = 17.9m,
                Status = "Processed",
                WhyPmt = 36,
                IsTCBorrower = true,
                IsPastDue = false,
                Items = new List<SalesAuthItem>
                {
                    new SalesAuthItem { ItemId = "TOOLBOX52", Description = "52\" Tool Chest", Qty = 1, Price = 599.99m },
                    new SalesAuthItem { ItemId = "HAMM16", Description = "16oz Hammer Set", Qty = 1, Price = 49.99m },
                    new SalesAuthItem { ItemId = "SCREWSET", Description = "Screwdriver Set 15pc", Qty = 1, Price = 39.99m }
                }
            },
            new SalesAuth
            {
                CustomerId = "0000752341",
                CustomerName = "BAGINSKI, KEVIN",
                Phone = "440-555-0198",
                CustomerProfile = "0-1-0",
                ThisSale = 3200.00m,
                RolloverAmount = 0.00m,
                LessTradeIn = 200.00m,
                SalesTax = 180.00m,
                AdminFee = 25.00m,
                PreviousBalance = 850.00m,
                NewCTCBalance = 4055.00m,
                IBNSalesAuthNumber = "SA-2024-003",
                IBNDate = new DateTime(2024, 10, 10),
                CQTSalesAuthNumber = "CQT-2024-043",
                NetSale = 3000.00m,
                CQTDate = new DateTime(2024, 10, 10),
                WeeklyPayAmt = 95.00m,
                MonthlyPayAmt = 412.00m,
                FirstPayment = new DateTime(2024, 10, 17),
                APR = 19.2m,
                Status = "Processed",
                WhyPmt = 43,
                IsTCBorrower = true,
                IsPastDue = true,
                Items = new List<SalesAuthItem>
                {
                    new SalesAuthItem { ItemId = "IMPACT20V", Description = "20V Impact Driver", Qty = 1, Price = 249.99m },
                    new SalesAuthItem { ItemId = "GRINDER7", Description = "7\" Angle Grinder", Qty = 1, Price = 129.99m },
                    new SalesAuthItem { ItemId = "SAWSKIL", Description = "Circular Saw 7-1/4\"", Qty = 1, Price = 179.99m }
                }
            },
            new SalesAuth
            {
                CustomerId = "0000759012",
                CustomerName = "BALL, TAYLOR JAMMAL",
                Phone = "216-555-0456",
                CustomerProfile = "2-0-1",
                ThisSale = 1500.00m,
                RolloverAmount = 0.00m,
                LessTradeIn = 0.00m,
                SalesTax = 90.00m,
                AdminFee = 25.00m,
                PreviousBalance = 0.00m,
                NewCTCBalance = 1615.00m,
                IBNSalesAuthNumber = "SA-2024-004",
                IBNDate = new DateTime(2024, 10, 8),
                CQTSalesAuthNumber = "CQT-2024-042",
                NetSale = 1500.00m,
                CQTDate = new DateTime(2024, 10, 8),
                WeeklyPayAmt = 45.00m,
                MonthlyPayAmt = 195.00m,
                FirstPayment = new DateTime(2024, 10, 15),
                APR = 16.5m,
                Status = "Processed",
                WhyPmt = 36,
                IsTCBorrower = true,
                IsPastDue = false,
                Items = new List<SalesAuthItem>
                {
                    new SalesAuthItem { ItemId = "RATCHSET", Description = "Ratchet Set 42pc", Qty = 1, Price = 149.99m },
                    new SalesAuthItem { ItemId = "PLIERS8", Description = "Pliers Set 8pc", Qty = 1, Price = 79.99m },
                    new SalesAuthItem { ItemId = "TAPESET", Description = "Measuring Tape Set", Qty = 1, Price = 29.99m }
                }
            },
            new SalesAuth
            {
                CustomerId = "0000761234",
                CustomerName = "BANE, CARL",
                Phone = "614-555-0789",
                CustomerProfile = "0-0-0",
                ThisSale = 2800.00m,
                RolloverAmount = 450.00m,
                LessTradeIn = 100.00m,
                SalesTax = 168.00m,
                AdminFee = 25.00m,
                PreviousBalance = 1500.00m,
                NewCTCBalance = 4843.00m,
                IBNSalesAuthNumber = "SA-2024-005",
                IBNDate = new DateTime(2024, 10, 5),
                CQTSalesAuthNumber = "CQT-2024-041",
                NetSale = 3150.00m,
                CQTDate = new DateTime(2024, 10, 5),
                WeeklyPayAmt = 110.00m,
                MonthlyPayAmt = 477.00m,
                FirstPayment = new DateTime(2024, 10, 12),
                APR = 20.1m,
                Status = "Processed",
                WhyPmt = 44,
                IsTCBorrower = true,
                IsPastDue = true,
                Items = new List<SalesAuthItem>
                {
                    new SalesAuthItem { ItemId = "COMPRESSOR", Description = "Air Compressor 6 Gal", Qty = 1, Price = 349.99m },
                    new SalesAuthItem { ItemId = "NAILGUN", Description = "Pneumatic Nail Gun", Qty = 1, Price = 199.99m },
                    new SalesAuthItem { ItemId = "HOSE50", Description = "50ft Air Hose", Qty = 1, Price = 59.99m }
                }
            },
            new SalesAuth
            {
                CustomerId = "0000765678",
                CustomerName = "BARKER, KEVIN",
                Phone = "937-555-0234",
                CustomerProfile = "1-0-0",
                ThisSale = 1200.00m,
                RolloverAmount = 0.00m,
                LessTradeIn = 0.00m,
                SalesTax = 72.00m,
                AdminFee = 25.00m,
                PreviousBalance = 0.00m,
                NewCTCBalance = 1297.00m,
                IBNSalesAuthNumber = "SA-2024-006",
                IBNDate = new DateTime(2024, 10, 3),
                CQTSalesAuthNumber = "CQT-2024-040",
                NetSale = 1200.00m,
                CQTDate = new DateTime(2024, 10, 3),
                WeeklyPayAmt = 38.00m,
                MonthlyPayAmt = 165.00m,
                FirstPayment = new DateTime(2024, 10, 10),
                APR = 15.9m,
                Status = "Pending",
                WhyPmt = 34,
                IsTCBorrower = true,
                IsPastDue = false,
                Items = new List<SalesAuthItem>
                {
                    new SalesAuthItem { ItemId = "MULTIMETER", Description = "Digital Multimeter", Qty = 1, Price = 89.99m },
                    new SalesAuthItem { ItemId = "WIRESTRIP", Description = "Wire Stripper Set", Qty = 1, Price = 34.99m },
                    new SalesAuthItem { ItemId = "FLASHSET", Description = "LED Flashlight Set", Qty = 1, Price = 59.99m }
                }
            },
            new SalesAuth
            {
                CustomerId = "0000769123",
                CustomerName = "BAUM, JEREMY",
                Phone = "419-555-0567",
                CustomerProfile = "0-1-0",
                ThisSale = 980.00m,
                RolloverAmount = 0.00m,
                LessTradeIn = 50.00m,
                SalesTax = 55.80m,
                AdminFee = 25.00m,
                PreviousBalance = 0.00m,
                NewCTCBalance = 1010.80m,
                IBNSalesAuthNumber = "SA-2024-007",
                IBNDate = new DateTime(2024, 10, 1),
                CQTSalesAuthNumber = "CQT-2024-039",
                NetSale = 930.00m,
                CQTDate = new DateTime(2024, 10, 1),
                WeeklyPayAmt = 30.00m,
                MonthlyPayAmt = 130.00m,
                FirstPayment = new DateTime(2024, 10, 8),
                APR = 14.9m,
                Status = "Processed",
                WhyPmt = 34,
                IsTCBorrower = false,
                IsPastDue = false,
                Items = new List<SalesAuthItem>
                {
                    new SalesAuthItem { ItemId = "TOOLBAG", Description = "Large Tool Bag", Qty = 1, Price = 79.99m },
                    new SalesAuthItem { ItemId = "LEVEL24", Description = "24\" Level", Qty = 1, Price = 39.99m },
                    new SalesAuthItem { ItemId = "SQUARE12", Description = "12\" Speed Square", Qty = 1, Price = 19.99m }
                }
            },
            new SalesAuth
            {
                CustomerId = "0000772456",
                CustomerName = "BLUHM, LINDSAY",
                Phone = "330-555-0891",
                CustomerProfile = "0-0-1",
                ThisSale = 3500.00m,
                RolloverAmount = 0.00m,
                LessTradeIn = 0.00m,
                SalesTax = 210.00m,
                AdminFee = 25.00m,
                PreviousBalance = 2100.00m,
                NewCTCBalance = 5835.00m,
                IBNSalesAuthNumber = "SA-2024-008",
                IBNDate = new DateTime(2024, 9, 28),
                CQTSalesAuthNumber = "CQT-2024-038",
                NetSale = 3500.00m,
                CQTDate = new DateTime(2024, 9, 28),
                WeeklyPayAmt = 125.00m,
                MonthlyPayAmt = 542.00m,
                FirstPayment = new DateTime(2024, 10, 5),
                APR = 21.5m,
                Status = "Reversed",
                WhyPmt = 47,
                IsTCBorrower = true,
                IsPastDue = true,
                Items = new List<SalesAuthItem>
                {
                    new SalesAuthItem { ItemId = "WELDER220", Description = "220V MIG Welder", Qty = 1, Price = 899.99m },
                    new SalesAuthItem { ItemId = "HELMWELD", Description = "Auto-Darkening Helmet", Qty = 1, Price = 149.99m },
                    new SalesAuthItem { ItemId = "GLOVEWELD", Description = "Welding Gloves", Qty = 1, Price = 39.99m }
                }
            }
        };
    }

    public static List<AuthorizationHistory> GetAuthorizationHistory(string customerId)
    {
        // Generate some historical data for the customer
        var random = new Random(customerId.GetHashCode());
        var history = new List<AuthorizationHistory>();
        var startDate = DateTime.Now.AddMonths(-12);

        for (int i = 0; i < random.Next(3, 8); i++)
        {
            var saleAmt = random.Next(800, 4000);
            var rollover = random.Next(0, 500);
            var tax = saleAmt * 0.06m;
            
            history.Add(new AuthorizationHistory
            {
                Date = startDate.AddMonths(i).AddDays(random.Next(0, 28)),
                Status = i == 0 ? (random.Next(0, 3) == 0 ? "Reversed" : "Processed") : "Processed",
                IBNSalesAuthNumber = $"SA-{DateTime.Now.Year}-{(100 + i):000}",
                SaleAmt = saleAmt,
                Rollover = rollover,
                Tax = tax,
                Total = saleAmt + rollover + tax + 25,
                WhyPmt = random.Next(30, 50)
            });
        }

        return history.OrderByDescending(h => h.Date).ToList();
    }
}

// Model classes
public class SalesAuth
{
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string CustomerProfile { get; set; } = string.Empty;
    
    // Blue Ribbon Sale
    public decimal ThisSale { get; set; }
    public decimal RolloverAmount { get; set; }
    public decimal LessTradeIn { get; set; }
    public decimal SalesTax { get; set; }
    public decimal AdminFee { get; set; }
    public decimal PreviousBalance { get; set; }
    public decimal NewCTCBalance { get; set; }
    
    // Authorization Details
    public string IBNSalesAuthNumber { get; set; } = string.Empty;
    public DateTime IBNDate { get; set; }
    public string CQTSalesAuthNumber { get; set; } = string.Empty;
    public decimal NetSale { get; set; }
    public DateTime CQTDate { get; set; }
    
    // Payment Details
    public decimal WeeklyPayAmt { get; set; }
    public decimal MonthlyPayAmt { get; set; }
    public DateTime FirstPayment { get; set; }
    public decimal APR { get; set; }
    
    // Status
    public string Status { get; set; } = string.Empty;
    public int WhyPmt { get; set; }
    
    // Filters
    public bool IsTCBorrower { get; set; }
    public bool IsPastDue { get; set; }
    
    // Items
    public List<SalesAuthItem> Items { get; set; } = new();
}

public class SalesAuthItem
{
    public string ItemId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Qty { get; set; }
    public decimal Price { get; set; }
}

public class AuthorizationHistory
{
    public DateTime Date { get; set; }
    public string Status { get; set; } = string.Empty;
    public string IBNSalesAuthNumber { get; set; } = string.Empty;
    public decimal SaleAmt { get; set; }
    public decimal Rollover { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public int WhyPmt { get; set; }
}
