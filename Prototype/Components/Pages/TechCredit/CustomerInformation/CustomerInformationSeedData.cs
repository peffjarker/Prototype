// Components/Pages/techcredit/customerinformation/TechCreditSeedData.cs
using System;
using System.Collections.Generic;

namespace Prototype.Components.Pages.Collections.TechCredit
{
    public static class CustomerInformationSeedData
    {
        public static List<CustomerInfo> GetCustomers()
        {
            return new List<CustomerInfo>
            {
                new CustomerInfo
                {
                    CustomerId = "0000736262",
                    Name = "ALLTOP, TIMOTHY C",
                    Phone = "419-685-4759",
                    Profile = "0-0-0",
                    CreditLimit = 15000.00m,
                    CurrentRate = 19.99m,
                    Balance = 6107.70m,
                    TotalAmtDue = 246.46m,
                    AvailableCredit = 8892.30m,
                    WeeklyPmt = 246.46m,
                    PastDue = 0m,
                    MonthlyPmt = 185.83m,
                    FirstPayment = new DateTime(2018, 5, 10),
                    APR = 19.99m,
                    IsTCBorrower = true,
                    IsPastDue = false,
                    LatestSalesAuth = new SalesAuthorization
                    {
                        ThisSale = 0.00m,
                        RolloverAmount = 5532.26m,
                        LessTradeIn = 0.00m,
                        SalesTax = 0.00m,
                        AdminFee = 20.00m,
                        PreviousBalance = 5555.44m,
                        NewCTCBalance = 6107.70m,
                        IBNSalesAuthNumber = "SA-0010310005",
                        Date = new DateTime(2018, 5, 3),
                        CQTSalesAuthNumber = "0000124821"
                    },
                    Transactions = new List<Transaction>
                    {
                        new Transaction { Memo = "0007080541", Type = "Withhold", Date = new DateTime(2018, 5, 4), Amount = 53.23m },
                        new Transaction { Memo = "0007080541", Type = "Sale", Date = new DateTime(2018, 5, 4), Amount = -5532.26m },
                        new Transaction { Memo = "0006669575", Type = "Sale", Date = new DateTime(2017, 11, 8), Amount = -5683.58m },
                        new Transaction { Memo = "0006669575", Type = "Withhold", Date = new DateTime(2017, 11, 8), Amount = 68.36m },
                        new Transaction { Memo = "0006530464", Type = "Sale", Date = new DateTime(2017, 8, 30), Amount = -5981.74m },
                        new Transaction { Memo = "0006530464", Type = "Withhold", Date = new DateTime(2017, 8, 30), Amount = 98.17m },
                        new Transaction { Memo = "0006273131", Type = "Withhold", Date = new DateTime(2017, 4, 26), Amount = 375.52m }
                    }
                },
                new CustomerInfo
                {
                    CustomerId = "0000745891",
                    Name = "AULT, JOSEPH",
                    Phone = "330-555-0123",
                    Profile = "1-0-0",
                    CreditLimit = 12000.00m,
                    CurrentRate = 19.99m,
                    Balance = 4250.50m,
                    TotalAmtDue = 180.25m,
                    AvailableCredit = 7749.50m,
                    WeeklyPmt = 180.25m,
                    PastDue = 0m,
                    MonthlyPmt = 156.42m,
                    FirstPayment = new DateTime(2019, 3, 15),
                    APR = 19.99m,
                    IsTCBorrower = true,
                    IsPastDue = false
                },
                new CustomerInfo
                {
                    CustomerId = "0000752341",
                    Name = "BAGINSKI, KEVIN",
                    Phone = "440-555-0198",
                    Profile = "0-1-0",
                    CreditLimit = 18000.00m,
                    CurrentRate = 18.99m,
                    Balance = 8945.75m,
                    TotalAmtDue = 325.80m,
                    AvailableCredit = 9054.25m,
                    WeeklyPmt = 325.80m,
                    PastDue = 0m,
                    MonthlyPmt = 245.50m,
                    FirstPayment = new DateTime(2020, 1, 8),
                    APR = 18.99m,
                    IsTCBorrower = true,
                    IsPastDue = false
                },
                new CustomerInfo
                {
                    CustomerId = "0000759012",
                    Name = "BALL, TAYLOR JAMMAL",
                    Phone = "216-555-0456",
                    Profile = "0-0-1",
                    CreditLimit = 10000.00m,
                    CurrentRate = 21.99m,
                    Balance = 1250.00m,
                    TotalAmtDue = 450.75m,
                    AvailableCredit = 8750.00m,
                    WeeklyPmt = 95.50m,
                    PastDue = 355.25m,
                    MonthlyPmt = 125.00m,
                    FirstPayment = new DateTime(2021, 6, 1),
                    APR = 21.99m,
                    IsTCBorrower = true,
                    IsPastDue = true
                },
                new CustomerInfo
                {
                    CustomerId = "0000761234",
                    Name = "BANE, CARL",
                    Phone = "614-555-0789",
                    Profile = "0-0-0",
                    CreditLimit = 15000.00m,
                    CurrentRate = 19.99m,
                    Balance = 5678.90m,
                    TotalAmtDue = 210.50m,
                    AvailableCredit = 9321.10m,
                    WeeklyPmt = 210.50m,
                    PastDue = 0m,
                    MonthlyPmt = 178.25m,
                    FirstPayment = new DateTime(2020, 9, 12),
                    APR = 19.99m,
                    IsTCBorrower = true,
                    IsPastDue = false
                },
                new CustomerInfo
                {
                    CustomerId = "0000765678",
                    Name = "BARKER, KEVIN",
                    Phone = "937-555-0234",
                    Profile = "1-1-0",
                    CreditLimit = 20000.00m,
                    CurrentRate = 17.99m,
                    Balance = 12450.25m,
                    TotalAmtDue = 425.75m,
                    AvailableCredit = 7549.75m,
                    WeeklyPmt = 425.75m,
                    PastDue = 0m,
                    MonthlyPmt = 312.50m,
                    FirstPayment = new DateTime(2019, 11, 20),
                    APR = 17.99m,
                    IsTCBorrower = true,
                    IsPastDue = false
                },
                new CustomerInfo
                {
                    CustomerId = "0000769123",
                    Name = "BAUM, JEREMY",
                    Phone = "419-555-0567",
                    Profile = "0-0-0",
                    CreditLimit = 13500.00m,
                    CurrentRate = 20.99m,
                    Balance = 3890.50m,
                    TotalAmtDue = 165.25m,
                    AvailableCredit = 9609.50m,
                    WeeklyPmt = 165.25m,
                    PastDue = 0m,
                    MonthlyPmt = 142.80m,
                    FirstPayment = new DateTime(2021, 2, 15),
                    APR = 20.99m,
                    IsTCBorrower = true,
                    IsPastDue = false
                },
                new CustomerInfo
                {
                    CustomerId = "0000772456",
                    Name = "BLUHM, LINDSAY",
                    Phone = "330-555-0891",
                    Profile = "0-1-1",
                    CreditLimit = 11000.00m,
                    CurrentRate = 19.99m,
                    Balance = 2150.75m,
                    TotalAmtDue = 580.50m,
                    AvailableCredit = 8849.25m,
                    WeeklyPmt = 125.30m,
                    PastDue = 455.20m,
                    MonthlyPmt = 95.00m,
                    FirstPayment = new DateTime(2020, 7, 8),
                    APR = 19.99m,
                    IsTCBorrower = true,
                    IsPastDue = true
                },
                new CustomerInfo
                {
                    CustomerId = "0000775789",
                    Name = "BOEHNLEIN, CHARLES",
                    Phone = "440-555-0345",
                    Profile = "0-0-0",
                    CreditLimit = 16000.00m,
                    CurrentRate = 18.99m,
                    Balance = 7234.60m,
                    TotalAmtDue = 285.40m,
                    AvailableCredit = 8765.40m,
                    WeeklyPmt = 285.40m,
                    PastDue = 0m,
                    MonthlyPmt = 225.75m,
                    FirstPayment = new DateTime(2019, 5, 22),
                    APR = 18.99m,
                    IsTCBorrower = true,
                    IsPastDue = false
                },
                new CustomerInfo
                {
                    CustomerId = "0000778234",
                    Name = "BOUGHMAN, BRENDEN",
                    Phone = "614-555-0678",
                    Profile = "1-0-0",
                    CreditLimit = 14000.00m,
                    CurrentRate = 19.99m,
                    Balance = 5560.80m,
                    TotalAmtDue = 195.75m,
                    AvailableCredit = 8439.20m,
                    WeeklyPmt = 195.75m,
                    PastDue = 0m,
                    MonthlyPmt = 168.50m,
                    FirstPayment = new DateTime(2020, 10, 5),
                    APR = 19.99m,
                    IsTCBorrower = true,
                    IsPastDue = false
                }
            };
        }

        public sealed class CustomerInfo
        {
            public string CustomerId { get; set; } = "";
            public string Name { get; set; } = "";
            public string Phone { get; set; } = "";
            public string Profile { get; set; } = "";
            public decimal CreditLimit { get; set; }
            public decimal CurrentRate { get; set; }
            public decimal Balance { get; set; }
            public decimal TotalAmtDue { get; set; }
            public decimal AvailableCredit { get; set; }
            public decimal WeeklyPmt { get; set; }
            public decimal PastDue { get; set; }
            public decimal MonthlyPmt { get; set; }
            public DateTime FirstPayment { get; set; }
            public decimal APR { get; set; }
            public bool IsTCBorrower { get; set; }
            public bool IsPastDue { get; set; }
            public SalesAuthorization? LatestSalesAuth { get; set; }
            public List<Transaction> Transactions { get; set; } = new();
        }

        public sealed class SalesAuthorization
        {
            public decimal ThisSale { get; set; }
            public decimal RolloverAmount { get; set; }
            public decimal LessTradeIn { get; set; }
            public decimal SalesTax { get; set; }
            public decimal AdminFee { get; set; }
            public decimal PreviousBalance { get; set; }
            public decimal NewCTCBalance { get; set; }
            public string IBNSalesAuthNumber { get; set; } = "";
            public DateTime Date { get; set; }
            public string CQTSalesAuthNumber { get; set; } = "";
        }

        public sealed class Transaction
        {
            public string Memo { get; set; } = "";
            public string Type { get; set; } = "";
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
        }
    }
}