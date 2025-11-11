// Components/Pages/Collections/CreditApplication/CreditApplicationSeedData.cs
using System;
using System.Collections.Generic;

namespace Prototype.Components.Pages.Collections.CreditApplication
{
    public static class CreditApplicationSeedData
    {
        public static List<CreditApp> GetApplications()
        {
            return new List<CreditApp>
            {
                new CreditApp
                {
                    BorrowerId = "0000736262",
                    CustomerName = "ALLTOP, TIMOTHY C",
                    FirstName = "TIMOTHY",
                    MiddleInitial = "C",
                    LastName = "ALLTOP",
                    Suffix = "",
                    SSN = "999-88-7777",
                    DOB = new DateTime(1991, 11, 4),
                    Profile = "",
                    Address1 = "7527 cleveland mass rd",
                    Address2 = "",
                    City = "clinton",
                    State = "OH",
                    Zip = "44216",
                    Phone = "419-685-4759",
                    Extension = "",
                    CellPhone = "419-685-4759",
                    Email = "jeffp@cornwelltools.com",
                    MailingAddressSame = true,
                    TimeAtResidenceYears = 2,
                    TimeAtResidenceMonths = 2,
                    EmployerName = "ZIEGLER TIRE",
                    Occupation = "tech",
                    EmployerAddress1 = "4010 PORTAGE AVE. N.W.",
                    EmployerAddress2 = "",
                    EmployerCity = "N CANTON",
                    EmployerState = "OH",
                    EmployerZip = "44720",
                    EmployerPhone = "330-966-0980",
                    EmployerExtension = "",
                    TimeAtEmployerYears = 7,
                    TimeAtEmployerMonths = 0,
                    NearestRelative = "emily alltop",
                    RelativePhone = "330-201-1470",
                    CTCAppId = "",
                    ApplicationDate = new DateTime(2022, 3, 2),
                    Decision = "Expired",
                    CreditLimit = 0.00m,
                    IsTCBorrower = true,
                    IsPastDue = false
                },
                new CreditApp
                {
                    BorrowerId = "0000745891",
                    CustomerName = "AULT, JOSEPH",
                    FirstName = "JOSEPH",
                    MiddleInitial = "",
                    LastName = "AULT",
                    Suffix = "",
                    SSN = "888-77-6666",
                    DOB = new DateTime(1985, 6, 15),
                    Profile = "",
                    Address1 = "1234 Main Street",
                    Address2 = "Apt 5B",
                    City = "Akron",
                    State = "OH",
                    Zip = "44301",
                    Phone = "330-555-0123",
                    Extension = "",
                    CellPhone = "330-555-0124",
                    Email = "jault@email.com",
                    MailingAddressSame = true,
                    TimeAtResidenceYears = 5,
                    TimeAtResidenceMonths = 3,
                    EmployerName = "AUTO TECH INC",
                    Occupation = "mechanic",
                    EmployerAddress1 = "500 Industrial Pkwy",
                    EmployerAddress2 = "",
                    EmployerCity = "Akron",
                    EmployerState = "OH",
                    EmployerZip = "44301",
                    EmployerPhone = "330-555-5000",
                    EmployerExtension = "101",
                    TimeAtEmployerYears = 4,
                    TimeAtEmployerMonths = 6,
                    NearestRelative = "Mary Ault",
                    RelativePhone = "330-555-0200",
                    CTCAppId = "APP-2019-00123",
                    ApplicationDate = new DateTime(2019, 3, 15),
                    Decision = "Approved",
                    CreditLimit = 12000.00m,
                    IsTCBorrower = true,
                    IsPastDue = false
                },
                new CreditApp
                {
                    BorrowerId = "0000752341",
                    CustomerName = "BAGINSKI, KEVIN",
                    FirstName = "KEVIN",
                    MiddleInitial = "J",
                    LastName = "BAGINSKI",
                    Suffix = "",
                    SSN = "777-66-5555",
                    DOB = new DateTime(1988, 9, 22),
                    Profile = "",
                    Address1 = "8900 Lake Ave",
                    Address2 = "",
                    City = "Cleveland",
                    State = "OH",
                    Zip = "44102",
                    Phone = "440-555-0198",
                    Extension = "",
                    CellPhone = "440-555-0199",
                    Email = "kbaginski@email.com",
                    MailingAddressSame = false,
                    TimeAtResidenceYears = 3,
                    TimeAtResidenceMonths = 8,
                    EmployerName = "PRECISION AUTO REPAIR",
                    Occupation = "service manager",
                    EmployerAddress1 = "2300 West 25th St",
                    EmployerAddress2 = "",
                    EmployerCity = "Cleveland",
                    EmployerState = "OH",
                    EmployerZip = "44113",
                    EmployerPhone = "440-555-3000",
                    EmployerExtension = "205",
                    TimeAtEmployerYears = 6,
                    TimeAtEmployerMonths = 2,
                    NearestRelative = "Susan Baginski",
                    RelativePhone = "440-555-1000",
                    CTCAppId = "APP-2020-00456",
                    ApplicationDate = new DateTime(2020, 1, 8),
                    Decision = "Approved",
                    CreditLimit = 18000.00m,
                    IsTCBorrower = true,
                    IsPastDue = false
                },
                new CreditApp
                {
                    BorrowerId = "0000759012",
                    CustomerName = "BALL, TAYLOR JAMMAL",
                    FirstName = "TAYLOR",
                    MiddleInitial = "J",
                    LastName = "BALL",
                    Suffix = "",
                    SSN = "666-55-4444",
                    DOB = new DateTime(1992, 3, 10),
                    Profile = "",
                    Address1 = "4567 Euclid Ave",
                    Address2 = "",
                    City = "Cleveland",
                    State = "OH",
                    Zip = "44103",
                    Phone = "216-555-0456",
                    Extension = "",
                    CellPhone = "216-555-0457",
                    Email = "tball@email.com",
                    MailingAddressSame = true,
                    TimeAtResidenceYears = 1,
                    TimeAtResidenceMonths = 6,
                    EmployerName = "QUICK LUBE CENTER",
                    Occupation = "technician",
                    EmployerAddress1 = "789 Broadway Ave",
                    EmployerAddress2 = "",
                    EmployerCity = "Cleveland",
                    EmployerState = "OH",
                    EmployerZip = "44104",
                    EmployerPhone = "216-555-7000",
                    EmployerExtension = "",
                    TimeAtEmployerYears = 2,
                    TimeAtEmployerMonths = 3,
                    NearestRelative = "James Ball",
                    RelativePhone = "216-555-2000",
                    CTCAppId = "APP-2021-00789",
                    ApplicationDate = new DateTime(2021, 6, 1),
                    Decision = "Approved",
                    CreditLimit = 10000.00m,
                    IsTCBorrower = true,
                    IsPastDue = true
                },
                new CreditApp
                {
                    BorrowerId = "0000761234",
                    CustomerName = "BANE, CARL",
                    FirstName = "CARL",
                    MiddleInitial = "R",
                    LastName = "BANE",
                    Suffix = "",
                    SSN = "555-44-3333",
                    DOB = new DateTime(1980, 12, 5),
                    Profile = "",
                    Address1 = "3210 Summit St",
                    Address2 = "",
                    City = "Columbus",
                    State = "OH",
                    Zip = "43201",
                    Phone = "614-555-0789",
                    Extension = "",
                    CellPhone = "614-555-0790",
                    Email = "cbane@email.com",
                    MailingAddressSame = true,
                    TimeAtResidenceYears = 8,
                    TimeAtResidenceMonths = 4,
                    EmployerName = "METRO MOTORS",
                    Occupation = "master technician",
                    EmployerAddress1 = "1500 North High St",
                    EmployerAddress2 = "",
                    EmployerCity = "Columbus",
                    EmployerState = "OH",
                    EmployerZip = "43201",
                    EmployerPhone = "614-555-8000",
                    EmployerExtension = "150",
                    TimeAtEmployerYears = 12,
                    TimeAtEmployerMonths = 0,
                    NearestRelative = "Linda Bane",
                    RelativePhone = "614-555-3000",
                    CTCAppId = "APP-2020-00912",
                    ApplicationDate = new DateTime(2020, 9, 12),
                    Decision = "Approved",
                    CreditLimit = 15000.00m,
                    IsTCBorrower = true,
                    IsPastDue = false
                },
                new CreditApp
                {
                    BorrowerId = "0000765678",
                    CustomerName = "BARKER, KEVIN",
                    FirstName = "KEVIN",
                    MiddleInitial = "M",
                    LastName = "BARKER",
                    Suffix = "Jr",
                    SSN = "444-33-2222",
                    DOB = new DateTime(1975, 7, 18),
                    Profile = "",
                    Address1 = "6789 Wright Ave",
                    Address2 = "",
                    City = "Dayton",
                    State = "OH",
                    Zip = "45402",
                    Phone = "937-555-0234",
                    Extension = "",
                    CellPhone = "937-555-0235",
                    Email = "kbarker@email.com",
                    MailingAddressSame = true,
                    TimeAtResidenceYears = 15,
                    TimeAtResidenceMonths = 0,
                    EmployerName = "WRIGHT AUTOMOTIVE",
                    Occupation = "shop foreman",
                    EmployerAddress1 = "2500 Keowee St",
                    EmployerAddress2 = "",
                    EmployerCity = "Dayton",
                    EmployerState = "OH",
                    EmployerZip = "45410",
                    EmployerPhone = "937-555-9000",
                    EmployerExtension = "301",
                    TimeAtEmployerYears = 18,
                    TimeAtEmployerMonths = 6,
                    NearestRelative = "Patricia Barker",
                    RelativePhone = "937-555-4000",
                    CTCAppId = "APP-2019-01123",
                    ApplicationDate = new DateTime(2019, 11, 20),
                    Decision = "Approved",
                    CreditLimit = 20000.00m,
                    IsTCBorrower = true,
                    IsPastDue = false
                }
            };
        }

        public sealed class CreditApp
        {
            public string BorrowerId { get; set; } = "";
            public string CustomerName { get; set; } = "";
            public string FirstName { get; set; } = "";
            public string MiddleInitial { get; set; } = "";
            public string LastName { get; set; } = "";
            public string Suffix { get; set; } = "";
            public string SSN { get; set; } = "";
            public DateTime DOB { get; set; }
            public string Profile { get; set; } = "";

            // Home Address
            public string Address1 { get; set; } = "";
            public string Address2 { get; set; } = "";
            public string City { get; set; } = "";
            public string State { get; set; } = "";
            public string Zip { get; set; } = "";
            public string Phone { get; set; } = "";
            public string Extension { get; set; } = "";
            public string CellPhone { get; set; } = "";
            public string Email { get; set; } = "";
            public bool MailingAddressSame { get; set; }
            public int TimeAtResidenceYears { get; set; }
            public int TimeAtResidenceMonths { get; set; }

            // Employer Information
            public string EmployerName { get; set; } = "";
            public string Occupation { get; set; } = "";
            public string EmployerAddress1 { get; set; } = "";
            public string EmployerAddress2 { get; set; } = "";
            public string EmployerCity { get; set; } = "";
            public string EmployerState { get; set; } = "";
            public string EmployerZip { get; set; } = "";
            public string EmployerPhone { get; set; } = "";
            public string EmployerExtension { get; set; } = "";
            public int TimeAtEmployerYears { get; set; }
            public int TimeAtEmployerMonths { get; set; }

            // Nearest Relative
            public string NearestRelative { get; set; } = "";
            public string RelativePhone { get; set; } = "";

            // Application Info
            public string CTCAppId { get; set; } = "";
            public DateTime ApplicationDate { get; set; }
            public string Decision { get; set; } = "";
            public decimal CreditLimit { get; set; }

            // Status flags
            public bool IsTCBorrower { get; set; }
            public bool IsPastDue { get; set; }
        }
    }
}