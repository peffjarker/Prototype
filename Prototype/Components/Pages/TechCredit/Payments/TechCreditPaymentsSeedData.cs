// Components/Pages/Collections/TechCredit/TechCreditPaymentsSeedData.cs
using System;
using System.Collections.Generic;

namespace Prototype.Components.Pages.Collections.TechCredit
{
    public static class TechCreditPaymentsSeedData
    {
        public static List<PaymentRecord> GetPayments()
        {
            var payments = new List<PaymentRecord>();
            var random = new Random(42); // Fixed seed for consistent data

            // Past Due Payments
            payments.AddRange(GeneratePastDuePayments());
            
            // Scheduled Payments
            payments.AddRange(GenerateScheduledPayments());
            
            // Payment History
            payments.AddRange(GeneratePaymentHistory());

            return payments;
        }

        private static List<PaymentRecord> GeneratePastDuePayments()
        {
            return new List<PaymentRecord>
            {
                new PaymentRecord
                {
                    PaymentId = "PD001",
                    CustomerName = "BALL, TAYLOR JAMMAL",
                    BorrowerId = "0000759012",
                    IsPastDue = true,
                    DaysOverdue = 45,
                    AmountDue = 95.50m,
                    PastDueAmount = 355.25m,
                    CurrentBalance = 1250.00m,
                    LastPaymentDate = new DateTime(2024, 10, 1),
                    LastPaymentAmount = 95.50m,
                    PaymentFrequency = "Weekly",
                    Phone = "(216) 555-0456",
                    Email = "tball@email.com",
                    ContactAttempts = 3,
                    LastContactDate = new DateTime(2024, 11, 10),
                    LastContactMethod = "Phone",
                    LastContactResult = "Left Voicemail"
                },
                new PaymentRecord
                {
                    PaymentId = "PD002",
                    CustomerName = "BLUHM, LINDSAY",
                    BorrowerId = "0000772456",
                    IsPastDue = true,
                    DaysOverdue = 32,
                    AmountDue = 125.30m,
                    PastDueAmount = 455.20m,
                    CurrentBalance = 2150.75m,
                    LastPaymentDate = new DateTime(2024, 10, 10),
                    LastPaymentAmount = 125.30m,
                    PaymentFrequency = "Weekly",
                    Phone = "(330) 555-0891",
                    Email = "lbluhm@email.com",
                    ContactAttempts = 2,
                    LastContactDate = new DateTime(2024, 11, 12),
                    LastContactMethod = "Email",
                    LastContactResult = "No Response"
                },
                new PaymentRecord
                {
                    PaymentId = "PD003",
                    CustomerName = "CHRISTOPHER MICHAEL",
                    BorrowerId = "0000781942",
                    IsPastDue = true,
                    DaysOverdue = 21,
                    AmountDue = 89.67m,
                    PastDueAmount = 198.71m,
                    CurrentBalance = 0.00m,
                    LastPaymentDate = new DateTime(2024, 10, 20),
                    LastPaymentAmount = 89.67m,
                    PaymentFrequency = "Weekly",
                    Phone = "(330) 833-6585",
                    Email = "cmichael@email.com",
                    ContactAttempts = 5,
                    LastContactDate = new DateTime(2024, 11, 14),
                    LastContactMethod = "Phone",
                    LastContactResult = "Promised Payment"
                },
                new PaymentRecord
                {
                    PaymentId = "PD004",
                    CustomerName = "CORY MCDERMOTT",
                    BorrowerId = "0000753723",
                    IsPastDue = true,
                    DaysOverdue = 18,
                    AmountDue = 81.56m,
                    PastDueAmount = 141.23m,
                    CurrentBalance = 3696.21m,
                    LastPaymentDate = new DateTime(2024, 10, 25),
                    LastPaymentAmount = 81.56m,
                    PaymentFrequency = "Weekly",
                    Phone = "(330) 454-8800",
                    Email = "cmcdermott@email.com",
                    ContactAttempts = 1,
                    LastContactDate = new DateTime(2024, 11, 15),
                    LastContactMethod = "Phone",
                    LastContactResult = "Spoke with Customer"
                },
                new PaymentRecord
                {
                    PaymentId = "PD005",
                    CustomerName = "DRAGONHEAD",
                    BorrowerId = "0000805310",
                    IsPastDue = true,
                    DaysOverdue = 14,
                    AmountDue = 18.35m,
                    PastDueAmount = 166.82m,
                    CurrentBalance = 2355.08m,
                    LastPaymentDate = new DateTime(2024, 10, 28),
                    LastPaymentAmount = 18.35m,
                    PaymentFrequency = "Weekly",
                    Phone = "(330) 645-7275",
                    Email = "dragonhead@email.com",
                    ContactAttempts = 4,
                    LastContactDate = new DateTime(2024, 11, 11),
                    LastContactMethod = "Text",
                    LastContactResult = "Read, No Response"
                },
                new PaymentRecord
                {
                    PaymentId = "PD006",
                    CustomerName = "HARLEY GALLOWAY",
                    BorrowerId = "0000757922",
                    IsPastDue = true,
                    DaysOverdue = 8,
                    AmountDue = 701.65m,
                    PastDueAmount = 529.32m,
                    CurrentBalance = 7187.08m,
                    LastPaymentDate = new DateTime(2024, 11, 1),
                    LastPaymentAmount = 43.08m,
                    PaymentFrequency = "Monthly",
                    Phone = "(330) 360-8732",
                    Email = "hgalloway@email.com",
                    ContactAttempts = 6,
                    LastContactDate = new DateTime(2024, 11, 16),
                    LastContactMethod = "Phone",
                    LastContactResult = "Payment Plan Discussed"
                },
                new PaymentRecord
                {
                    PaymentId = "PD007",
                    CustomerName = "JOHNATHAN SHOEMAKER",
                    BorrowerId = "0000685813",
                    IsPastDue = true,
                    DaysOverdue = 7,
                    AmountDue = 59.16m,
                    PastDueAmount = 113.32m,
                    CurrentBalance = 7748.03m,
                    LastPaymentDate = new DateTime(2024, 11, 5),
                    LastPaymentAmount = 59.16m,
                    PaymentFrequency = "Weekly",
                    Phone = "(708) 868-0099",
                    Email = "jshoemaker@email.com",
                    ContactAttempts = 0,
                    LastContactDate = null,
                    LastContactMethod = "",
                    LastContactResult = ""
                }
            };
        }

        private static List<PaymentRecord> GenerateScheduledPayments()
        {
            var today = DateTime.Today;
            return new List<PaymentRecord>
            {
                new PaymentRecord
                {
                    PaymentId = "SCH001",
                    CustomerName = "AIDEN ROBINSON",
                    BorrowerId = "0000806748",
                    ScheduledDate = today.AddDays(2),
                    AmountDue = 10.93m,
                    CurrentBalance = 973.33m,
                    PaymentFrequency = "Weekly",
                    PaymentMethod = "Auto-Pay ACH",
                    AutoPay = true,
                    Phone = "(330) 795-6644",
                    Email = "arobinson@email.com"
                },
                new PaymentRecord
                {
                    PaymentId = "SCH002",
                    CustomerName = "ALEX ROWE",
                    BorrowerId = "0000761328",
                    ScheduledDate = today.AddDays(5),
                    AmountDue = 40.99m,
                    CurrentBalance = 4248.73m,
                    PaymentFrequency = "Weekly",
                    PaymentMethod = "Check",
                    AutoPay = false,
                    Phone = "(603) 438-8003",
                    Email = "arowe@email.com"
                },
                new PaymentRecord
                {
                    PaymentId = "SCH003",
                    CustomerName = "BAGINSKI, KEVIN",
                    BorrowerId = "0000752341",
                    ScheduledDate = today.AddDays(7),
                    AmountDue = 325.80m,
                    CurrentBalance = 8945.75m,
                    PaymentFrequency = "Monthly",
                    PaymentMethod = "Auto-Pay Card",
                    AutoPay = true,
                    Phone = "(440) 555-0198",
                    Email = "kbaginski@email.com"
                },
                new PaymentRecord
                {
                    PaymentId = "SCH004",
                    CustomerName = "BANE, CARL",
                    BorrowerId = "0000761234",
                    ScheduledDate = today.AddDays(10),
                    AmountDue = 210.50m,
                    CurrentBalance = 5678.90m,
                    PaymentFrequency = "Bi-Weekly",
                    PaymentMethod = "ACH",
                    AutoPay = false,
                    Phone = "(614) 555-0789",
                    Email = "cbane@email.com"
                },
                new PaymentRecord
                {
                    PaymentId = "SCH005",
                    CustomerName = "BARKER, KEVIN",
                    BorrowerId = "0000765678",
                    ScheduledDate = today.AddDays(14),
                    AmountDue = 425.75m,
                    CurrentBalance = 12450.25m,
                    PaymentFrequency = "Bi-Weekly",
                    PaymentMethod = "Auto-Pay ACH",
                    AutoPay = true,
                    Phone = "(937) 555-0234",
                    Email = "kbarker@email.com"
                },
                new PaymentRecord
                {
                    PaymentId = "SCH006",
                    CustomerName = "BAUM, JEREMY",
                    BorrowerId = "0000769123",
                    ScheduledDate = today.AddDays(18),
                    AmountDue = 165.25m,
                    CurrentBalance = 3890.50m,
                    PaymentFrequency = "Weekly",
                    PaymentMethod = "Check",
                    AutoPay = false,
                    Phone = "(419) 555-0567",
                    Email = "jbaum@email.com"
                },
                new PaymentRecord
                {
                    PaymentId = "SCH007",
                    CustomerName = "BOEHNLEIN, CHARLES",
                    BorrowerId = "0000775789",
                    ScheduledDate = today.AddDays(21),
                    AmountDue = 285.40m,
                    CurrentBalance = 7234.60m,
                    PaymentFrequency = "Monthly",
                    PaymentMethod = "Auto-Pay Card",
                    AutoPay = true,
                    Phone = "(440) 555-0345",
                    Email = "cboehnlein@email.com"
                },
                new PaymentRecord
                {
                    PaymentId = "SCH008",
                    CustomerName = "BRANDON COULTER",
                    BorrowerId = "0000732130",
                    ScheduledDate = today.AddDays(25),
                    AmountDue = 26.07m,
                    CurrentBalance = 10136.87m,
                    PaymentFrequency = "Weekly",
                    PaymentMethod = "Cash",
                    AutoPay = false,
                    Phone = "(330) 696-8551",
                    Email = "bcoulter@email.com"
                },
                new PaymentRecord
                {
                    PaymentId = "SCH009",
                    CustomerName = "DEAN M MARSHALL",
                    BorrowerId = "0000781942",
                    ScheduledDate = today.AddDays(28),
                    AmountDue = 597.42m,
                    CurrentBalance = 14433.45m,
                    PaymentFrequency = "Monthly",
                    PaymentMethod = "ACH",
                    AutoPay = false,
                    Phone = "(330) 238-3206",
                    Email = "dmarshall@email.com"
                }
            };
        }

        private static List<PaymentRecord> GeneratePaymentHistory()
        {
            var today = DateTime.Today;
            return new List<PaymentRecord>
            {
                new PaymentRecord
                {
                    PaymentId = "HIST001",
                    CustomerName = "AULT, JOSEPH",
                    BorrowerId = "0000745891",
                    PaymentDate = today,
                    AmountPaid = 180.25m,
                    CurrentBalance = 4250.50m,
                    PaymentMethod = "Auto-Pay ACH",
                    ReferenceNumber = "ACH202411170001",
                    ProcessedBy = "System",
                    PaymentStatus = "Completed",
                    Notes = "Automatic payment processed successfully"
                },
                new PaymentRecord
                {
                    PaymentId = "HIST002",
                    CustomerName = "BOUGHMAN, BRENDEN",
                    BorrowerId = "0000778234",
                    PaymentDate = today,
                    AmountPaid = 195.75m,
                    CurrentBalance = 5560.80m,
                    PaymentMethod = "Check",
                    ReferenceNumber = "CHK4589",
                    ProcessedBy = "Sarah M.",
                    PaymentStatus = "Completed",
                    Notes = "Check received and deposited"
                },
                new PaymentRecord
                {
                    PaymentId = "HIST003",
                    CustomerName = "ALLTOP, TIMOTHY C",
                    BorrowerId = "0000736262",
                    PaymentDate = today.AddDays(-1),
                    AmountPaid = 246.46m,
                    CurrentBalance = 6107.70m,
                    PaymentMethod = "Cash",
                    ReferenceNumber = "CASH20241116001",
                    ProcessedBy = "John D.",
                    PaymentStatus = "Completed",
                    Notes = "Cash payment received at office"
                },
                new PaymentRecord
                {
                    PaymentId = "HIST004",
                    CustomerName = "BRETT MOORE",
                    BorrowerId = "0000788254",
                    PaymentDate = today.AddDays(-2),
                    AmountPaid = 37.90m,
                    CurrentBalance = 3063.13m,
                    PaymentMethod = "Auto-Pay Card",
                    ReferenceNumber = "CC202411150089",
                    ProcessedBy = "System",
                    PaymentStatus = "Completed",
                    Notes = "Credit card payment processed"
                },
                new PaymentRecord
                {
                    PaymentId = "HIST005",
                    CustomerName = "BRIAN PYLE",
                    BorrowerId = "0000789328",
                    PaymentDate = today.AddDays(-3),
                    AmountPaid = 40.77m,
                    CurrentBalance = 2256.88m,
                    PaymentMethod = "ACH",
                    ReferenceNumber = "ACH202411140045",
                    ProcessedBy = "System",
                    PaymentStatus = "Completed",
                    Notes = "ACH transfer completed"
                },
                new PaymentRecord
                {
                    PaymentId = "HIST006",
                    CustomerName = "DAVE JOHNSONS SPEED",
                    BorrowerId = "0000630304",
                    PaymentDate = today.AddDays(-5),
                    AmountPaid = 282.46m,
                    CurrentBalance = 5703.79m,
                    PaymentMethod = "Check",
                    ReferenceNumber = "CHK7821",
                    ProcessedBy = "Sarah M.",
                    PaymentStatus = "Completed",
                    Notes = "Company check received"
                },
                new PaymentRecord
                {
                    PaymentId = "HIST007",
                    CustomerName = "GABRIEL CAMPBELL",
                    BorrowerId = "0000791600",
                    PaymentDate = today.AddDays(-7),
                    AmountPaid = 329.04m,
                    CurrentBalance = 9587.88m,
                    PaymentMethod = "Auto-Pay ACH",
                    ReferenceNumber = "ACH202411100023",
                    ProcessedBy = "System",
                    PaymentStatus = "Completed",
                    Notes = "Automatic payment processed"
                },
                new PaymentRecord
                {
                    PaymentId = "HIST008",
                    CustomerName = "CHRISTOPHER MICHAEL",
                    BorrowerId = "0000781942",
                    PaymentDate = today.AddDays(-10),
                    AmountPaid = 50.00m,
                    CurrentBalance = 0.00m,
                    PaymentMethod = "Cash",
                    ReferenceNumber = "CASH20241107002",
                    ProcessedBy = "John D.",
                    PaymentStatus = "Completed",
                    Notes = "Partial payment - customer financial hardship"
                },
                new PaymentRecord
                {
                    PaymentId = "HIST009",
                    CustomerName = "BALL, TAYLOR JAMMAL",
                    BorrowerId = "0000759012",
                    PaymentDate = today.AddDays(-15),
                    AmountPaid = 95.50m,
                    CurrentBalance = 1250.00m,
                    PaymentMethod = "Check",
                    ReferenceNumber = "CHK9234",
                    ProcessedBy = "Sarah M.",
                    PaymentStatus = "Failed",
                    Notes = "Check returned - NSF"
                },
                new PaymentRecord
                {
                    PaymentId = "HIST010",
                    CustomerName = "DRAGONHEAD",
                    BorrowerId = "0000805310",
                    PaymentDate = today.AddDays(-20),
                    AmountPaid = 18.35m,
                    CurrentBalance = 2355.08m,
                    PaymentMethod = "Cash",
                    ReferenceNumber = "CASH20241028001",
                    ProcessedBy = "John D.",
                    PaymentStatus = "Completed",
                    Notes = "Cash payment received"
                },
                new PaymentRecord
                {
                    PaymentId = "HIST011",
                    CustomerName = "CORY MCDERMOTT",
                    BorrowerId = "0000753723",
                    PaymentDate = today.AddDays(-25),
                    AmountPaid = 163.12m,
                    CurrentBalance = 3696.21m,
                    PaymentMethod = "ACH",
                    ReferenceNumber = "ACH202410230078",
                    ProcessedBy = "System",
                    PaymentStatus = "Reversed",
                    Notes = "Customer disputed charge - reversed"
                }
            };
        }

        public sealed class PaymentRecord
        {
            // Common fields
            public string PaymentId { get; set; } = "";
            public string CustomerName { get; set; } = "";
            public string BorrowerId { get; set; } = "";
            public string Phone { get; set; } = "";
            public string Email { get; set; } = "";
            public decimal CurrentBalance { get; set; }
            public string PaymentFrequency { get; set; } = "";
            public string PaymentMethod { get; set; } = "";

            // Past Due fields
            public bool IsPastDue { get; set; }
            public int DaysOverdue { get; set; }
            public decimal PastDueAmount { get; set; }
            public DateTime? LastPaymentDate { get; set; }
            public decimal LastPaymentAmount { get; set; }
            public int ContactAttempts { get; set; }
            public DateTime? LastContactDate { get; set; }
            public string LastContactMethod { get; set; } = "";
            public string LastContactResult { get; set; } = "";

            // Scheduled fields
            public DateTime? ScheduledDate { get; set; }
            public decimal AmountDue { get; set; }
            public bool AutoPay { get; set; }

            // History fields
            public DateTime? PaymentDate { get; set; }
            public decimal AmountPaid { get; set; }
            public string ReferenceNumber { get; set; } = "";
            public string ProcessedBy { get; set; } = "";
            public string PaymentStatus { get; set; } = "";
            public string Notes { get; set; } = "";
        }
    }
}
