using System;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.Components.Pages.POXfer.ReceiptOfProduct;

public static class ReceiptOfProductSeedData
{
    public static List<AsnSummary> GetAsnSummaries()
    {
        return new List<AsnSummary>
        {
            // CQT Stock ASNs
            new AsnSummary("0044501566", new DateTime(2025, 8, 22), "1DC", 365.44m, ReceiptStatus.Open, "CQT Stock",
                PrintNeedsOnly: false, ReceivedAmount: 0m, TrackingNumber: "1Z4824G30934141682", Carrier: "UPS"),

            new AsnSummary("0044453050", new DateTime(2025, 8, 15), "1DC", 92.10m, ReceiptStatus.Open, "CQT Stock",
                PrintNeedsOnly: false, ReceivedAmount: 0m, TrackingNumber: "1Z9999W99999999999", Carrier: "UPS"),

            new AsnSummary("0044249540", new DateTime(2025, 8, 13), "1DC", 410.00m, ReceiptStatus.Closed, "CQT Stock",
                PrintNeedsOnly: true, ReceivedAmount: 410.00m, TrackingNumber: "1Z5555E55555555555", Carrier: "UPS"),

            new AsnSummary("0044341783", new DateTime(2025, 8, 4), "1DC", 76.25m, ReceiptStatus.Closed, "CQT Stock",
                PrintNeedsOnly: false, ReceivedAmount: 76.25m, TrackingNumber: null, Carrier: "UPS"),

            new AsnSummary("0044350891", new DateTime(2025, 7, 30), "1DC", 129.99m, ReceiptStatus.Open, "CQT Stock",
                PrintNeedsOnly: true, ReceivedAmount: 0m, TrackingNumber: "1Z7777A77777777777", Carrier: "UPS"),

            new AsnSummary("0044512345", new DateTime(2025, 8, 28), "1DC", 1245.50m, ReceiptStatus.Open, "CQT Stock",
                PrintNeedsOnly: false, ReceivedAmount: 0m, TrackingNumber: "1Z8888B88888888888", Carrier: "UPS"),

            new AsnSummary("0044523456", new DateTime(2025, 8, 25), "1DC", 567.89m, ReceiptStatus.Open, "CQT Stock",
                PrintNeedsOnly: true, ReceivedAmount: 234.50m, TrackingNumber: "794615901234", Carrier: "FedEx"),

            new AsnSummary("0044534567", new DateTime(2025, 8, 20), "1DC", 234.75m, ReceiptStatus.Closed, "CQT Stock",
                PrintNeedsOnly: false, ReceivedAmount: 234.75m, TrackingNumber: "9405511899560123456789", Carrier: "USPS"),

            new AsnSummary("0044545678", new DateTime(2025, 8, 10), "1DC", 892.30m, ReceiptStatus.Open, "CQT Stock",
                PrintNeedsOnly: true, ReceivedAmount: 0m, TrackingNumber: "1Z1111C11111111111", Carrier: "UPS"),

            new AsnSummary("0044556789", new DateTime(2025, 8, 5), "1DC", 345.60m, ReceiptStatus.Closed, "CQT Stock",
                PrintNeedsOnly: false, ReceivedAmount: 345.60m, TrackingNumber: "794615902345", Carrier: "FedEx"),
            
            // Local Only ASNs (LPN-ASN- prefix)
            new AsnSummary("LPN-ASN-7001", new DateTime(2025, 8, 27), "Local Supplier", 456.75m, ReceiptStatus.Open, "Local Only",
                PrintNeedsOnly: false, ReceivedAmount: 0m, TrackingNumber: "LOCAL-TRK-001", Carrier: "Other"),

            new AsnSummary("LPN-ASN-7002", new DateTime(2025, 8, 23), "Local Parts", 189.99m, ReceiptStatus.Open, "Local Only",
                PrintNeedsOnly: true, ReceivedAmount: 0m, TrackingNumber: null, Carrier: "Other"),

            new AsnSummary("LPN-ASN-7003", new DateTime(2025, 8, 18), "Local Distributor", 678.50m, ReceiptStatus.Closed, "Local Only",
                PrintNeedsOnly: false, ReceivedAmount: 678.50m, TrackingNumber: "LOCAL-TRK-002", Carrier: "Other"),

            new AsnSummary("LPN-ASN-7004", new DateTime(2025, 8, 12), "Local Supplier", 234.00m, ReceiptStatus.Open, "Local Only",
                PrintNeedsOnly: true, ReceivedAmount: 156.00m, TrackingNumber: "LOCAL-TRK-003", Carrier: "Other"),

            new AsnSummary("LPN-ASN-7005", new DateTime(2025, 8, 8), "Local Parts", 567.25m, ReceiptStatus.Closed, "Local Only",
                PrintNeedsOnly: false, ReceivedAmount: 567.25m, TrackingNumber: null, Carrier: "Other"),

            new AsnSummary("LPN-ASN-7006", new DateTime(2025, 8, 2), "Local Distributor", 890.40m, ReceiptStatus.Open, "Local Only",
                PrintNeedsOnly: true, ReceivedAmount: 0m, TrackingNumber: "LOCAL-TRK-004", Carrier: "Other"),

            new AsnSummary("LPN-ASN-7007", new DateTime(2025, 7, 28), "Local Supplier", 123.50m, ReceiptStatus.Closed, "Local Only",
                PrintNeedsOnly: false, ReceivedAmount: 123.50m, TrackingNumber: "LOCAL-TRK-005", Carrier: "Other")
        };
    }

    public static List<AsnLine> GetAsnLines(string asnId)
    {
        var lines = new List<AsnLine>();

        // CQT Stock ASN Lines
        if (asnId == "0044501566")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "MWE289120",
                    LabelCount = 1,
                    Description = "M18 & M12 Wireless Spkr",
                    UnitCost = 185.46m,
                    ExtCost = 185.46m,
                    PONumber = "PO-200996",
                    POLine = 7,
                    NeedFor = "Truck",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 0,
                    QtyShip = 1,
                    PreviouslyReceived = 0,
                    ReceiveQty = 1,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "SB4820",
                    LabelCount = 2,
                    Description = "20pc 1/2\" Drive SAE Socket Set",
                    UnitCost = 89.99m,
                    ExtCost = 179.98m,
                    PONumber = "PO-200996",
                    POLine = 8,
                    NeedFor = "Stock",
                    QtyOnHand = 2,
                    TotalQtyNeeds = 3,
                    QtyShip = 2,
                    PreviouslyReceived = 0,
                    ReceiveQty = 2,
                    Status = "Open"
                }
            });
        }
        else if (asnId == "0044453050")
        {
            lines.Add(new AsnLine
            {
                Item = "WT3350",
                LabelCount = 1,
                Description = "Adjustable Wrench 12\"",
                UnitCost = 92.10m,
                ExtCost = 92.10m,
                PONumber = "PO-200994",
                POLine = 3,
                NeedFor = "Customer",
                QtyOnHand = 1,
                TotalQtyNeeds = 1,
                QtyShip = 1,
                PreviouslyReceived = 0,
                ReceiveQty = 0,
                Status = "Open"
            });
        }
        else if (asnId == "0044249540")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "C120",
                    LabelCount = 1,
                    Description = "Torque Wrench 1/2\" Drive 50-250 ft-lb",
                    UnitCost = 425.00m,
                    ExtCost = 425.00m,
                    PONumber = "PO-200995",
                    POLine = 5,
                    NeedFor = "Stock",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 2,
                    QtyShip = 1,
                    PreviouslyReceived = 1,
                    ReceiveQty = 0,
                    Status = "Closed"
                }
            });
        }
        else if (asnId == "0044341783")
        {
            lines.Add(new AsnLine
            {
                Item = "E440",
                LabelCount = 1,
                Description = "Pliers Set 4pc",
                UnitCost = 76.25m,
                ExtCost = 76.25m,
                PONumber = "PO-201001",
                POLine = 2,
                NeedFor = "Customer",
                QtyOnHand = 3,
                TotalQtyNeeds = 1,
                QtyShip = 1,
                PreviouslyReceived = 1,
                ReceiveQty = 0,
                Status = "Closed"
            });
        }
        else if (asnId == "0044350891")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "G660",
                    LabelCount = 1,
                    Description = "Ball Peen Hammer 24oz",
                    UnitCost = 42.50m,
                    ExtCost = 42.50m,
                    PONumber = "PO-201002",
                    POLine = 1,
                    NeedFor = "Stock",
                    QtyOnHand = 1,
                    TotalQtyNeeds = 3,
                    QtyShip = 1,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "F550",
                    LabelCount = 1,
                    Description = "Screwdriver Set 10pc",
                    UnitCost = 67.99m,
                    ExtCost = 67.99m,
                    PONumber = "PO-201002",
                    POLine = 2,
                    NeedFor = "Truck",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 1,
                    QtyShip = 1,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "N200",
                    LabelCount = 1,
                    Description = "Battery Terminal Cleaner",
                    UnitCost = 18.50m,
                    ExtCost = 18.50m,
                    PONumber = "PO-201002",
                    POLine = 3,
                    NeedFor = "Stock",
                    QtyOnHand = 5,
                    TotalQtyNeeds = 2,
                    QtyShip = 1,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                }
            });
        }
        else if (asnId == "0044512345")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "B450",
                    LabelCount = 3,
                    Description = "1/2\" Impact Socket Set, 15pc",
                    UnitCost = 189.99m,
                    ExtCost = 569.97m,
                    PONumber = "PO-201003",
                    POLine = 1,
                    NeedFor = "Stock",
                    QtyOnHand = 1,
                    TotalQtyNeeds = 5,
                    QtyShip = 3,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "D330",
                    LabelCount = 2,
                    Description = "Ratcheting Wrench Set SAE 10pc",
                    UnitCost = 195.00m,
                    ExtCost = 390.00m,
                    PONumber = "PO-201003",
                    POLine = 2,
                    NeedFor = "Stock",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 3,
                    QtyShip = 2,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "M100",
                    LabelCount = 3,
                    Description = "Oil Filter Wrench Set 3pc",
                    UnitCost = 45.99m,
                    ExtCost = 137.97m,
                    PONumber = "PO-201003",
                    POLine = 3,
                    NeedFor = "Customer",
                    QtyOnHand = 2,
                    TotalQtyNeeds = 2,
                    QtyShip = 3,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "H770",
                    LabelCount = 4,
                    Description = "Mechanic's Gloves XL",
                    UnitCost = 24.99m,
                    ExtCost = 99.96m,
                    PONumber = "PO-201003",
                    POLine = 4,
                    NeedFor = "Stock",
                    QtyOnHand = 8,
                    TotalQtyNeeds = 6,
                    QtyShip = 4,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                }
            });
        }
        else if (asnId == "0044523456")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "C125",
                    LabelCount = 1,
                    Description = "Digital Torque Adapter 1/4\" Drive",
                    UnitCost = 275.00m,
                    ExtCost = 275.00m,
                    PONumber = "PO-201004",
                    POLine = 1,
                    NeedFor = "Truck",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 1,
                    QtyShip = 1,
                    PreviouslyReceived = 0,
                    ReceiveQty = 1,
                    Status = "Partial"
                },
                new AsnLine
                {
                    Item = "AS6SB",
                    LabelCount = 1,
                    Description = "Stubby Air Slide Hammer",
                    UnitCost = 159.99m,
                    ExtCost = 159.99m,
                    PONumber = "PO-201004",
                    POLine = 2,
                    NeedFor = "Customer",
                    QtyOnHand = 1,
                    TotalQtyNeeds = 1,
                    QtyShip = 1,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "M105",
                    LabelCount = 1,
                    Description = "Brake Caliper Tool Kit",
                    UnitCost = 132.90m,
                    ExtCost = 132.90m,
                    PONumber = "PO-201004",
                    POLine = 3,
                    NeedFor = "Stock",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 2,
                    QtyShip = 1,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                }
            });
        }
        else if (asnId == "0044534567")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "P300",
                    LabelCount = 5,
                    Description = "Magnetic Parts Tray 6\"",
                    UnitCost = 14.99m,
                    ExtCost = 74.95m,
                    PONumber = "PO-201005",
                    POLine = 1,
                    NeedFor = "Stock",
                    QtyOnHand = 10,
                    TotalQtyNeeds = 8,
                    QtyShip = 5,
                    PreviouslyReceived = 5,
                    ReceiveQty = 0,
                    Status = "Closed"
                },
                new AsnLine
                {
                    Item = "J885",
                    LabelCount = 8,
                    Description = "Parts Cleaner Spray 16oz",
                    UnitCost = 12.50m,
                    ExtCost = 100.00m,
                    PONumber = "PO-201005",
                    POLine = 2,
                    NeedFor = "Stock",
                    QtyOnHand = 15,
                    TotalQtyNeeds = 12,
                    QtyShip = 8,
                    PreviouslyReceived = 8,
                    ReceiveQty = 0,
                    Status = "Closed"
                },
                new AsnLine
                {
                    Item = "K995",
                    LabelCount = 1,
                    Description = "Mechanic's Stool Adjustable",
                    UnitCost = 59.80m,
                    ExtCost = 59.80m,
                    PONumber = "PO-201005",
                    POLine = 3,
                    NeedFor = "Truck",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 1,
                    QtyShip = 1,
                    PreviouslyReceived = 1,
                    ReceiveQty = 0,
                    Status = "Closed"
                }
            });
        }
        else if (asnId == "0044545678")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "B451",
                    LabelCount = 2,
                    Description = "3/8\" Deep Socket Set, 12pc",
                    UnitCost = 145.50m,
                    ExtCost = 291.00m,
                    PONumber = "PO-201006",
                    POLine = 1,
                    NeedFor = "Stock",
                    QtyOnHand = 1,
                    TotalQtyNeeds = 4,
                    QtyShip = 2,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "D335",
                    LabelCount = 3,
                    Description = "Ratcheting Wrench Set Metric 10pc",
                    UnitCost = 195.00m,
                    ExtCost = 585.00m,
                    PONumber = "PO-201006",
                    POLine = 2,
                    NeedFor = "Customer",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 2,
                    QtyShip = 3,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                }
            });
        }
        else if (asnId == "0044556789")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "H775",
                    LabelCount = 3,
                    Description = "Mechanic's Gloves L",
                    UnitCost = 24.99m,
                    ExtCost = 74.97m,
                    PONumber = "PO-201007",
                    POLine = 1,
                    NeedFor = "Stock",
                    QtyOnHand = 6,
                    TotalQtyNeeds = 5,
                    QtyShip = 3,
                    PreviouslyReceived = 3,
                    ReceiveQty = 0,
                    Status = "Closed"
                },
                new AsnLine
                {
                    Item = "J880",
                    LabelCount = 4,
                    Description = "Shop Towels (Box of 200)",
                    UnitCost = 15.99m,
                    ExtCost = 63.96m,
                    PONumber = "PO-201007",
                    POLine = 2,
                    NeedFor = "Stock",
                    QtyOnHand = 8,
                    TotalQtyNeeds = 6,
                    QtyShip = 4,
                    PreviouslyReceived = 4,
                    ReceiveQty = 0,
                    Status = "Closed"
                },
                new AsnLine
                {
                    Item = "AF12201",
                    LabelCount = 10,
                    Description = "1220-1 AIR TOOL OIL PINT",
                    UnitCost = 10.00m,
                    ExtCost = 100.00m,
                    PONumber = "PO-201007",
                    POLine = 3,
                    NeedFor = "Stock",
                    QtyOnHand = 20,
                    TotalQtyNeeds = 15,
                    QtyShip = 10,
                    PreviouslyReceived = 10,
                    ReceiveQty = 0,
                    Status = "Closed"
                }
            });
        }
        // Local Only ASN Lines
        else if (asnId == "LPN-ASN-7001")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "LPN-BC45",
                    LabelCount = 2,
                    Description = "Brake Caliper Piston Tool",
                    UnitCost = 67.50m,
                    ExtCost = 135.00m,
                    PONumber = "LPN-PO-5001",
                    POLine = 1,
                    NeedFor = "Stock",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 3,
                    QtyShip = 2,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "LPN-TW250",
                    LabelCount = 1,
                    Description = "Torque Wrench 3/8\" Drive 25-250 in-lb",
                    UnitCost = 189.99m,
                    ExtCost = 189.99m,
                    PONumber = "LPN-PO-5001",
                    POLine = 2,
                    NeedFor = "Customer",
                    QtyOnHand = 1,
                    TotalQtyNeeds = 1,
                    QtyShip = 1,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "LPN-HS12",
                    LabelCount = 2,
                    Description = "Hose Clamp Pliers Set",
                    UnitCost = 54.99m,
                    ExtCost = 109.98m,
                    PONumber = "LPN-PO-5002",
                    POLine = 1,
                    NeedFor = "Truck",
                    QtyOnHand = 1,
                    TotalQtyNeeds = 2,
                    QtyShip = 2,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                }
            });
        }
        else if (asnId == "LPN-ASN-7002")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "LPN-PB20",
                    LabelCount = 1,
                    Description = "Pry Bar Set 3pc",
                    UnitCost = 39.99m,
                    ExtCost = 39.99m,
                    PONumber = "LPN-PO-5002",
                    POLine = 2,
                    NeedFor = "Stock",
                    QtyOnHand = 2,
                    TotalQtyNeeds = 1,
                    QtyShip = 1,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "LPN-CH80",
                    LabelCount = 3,
                    Description = "Chisel Set 8pc",
                    UnitCost = 48.50m,
                    ExtCost = 145.50m,
                    PONumber = "LPN-PO-5003",
                    POLine = 1,
                    NeedFor = "Stock",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 4,
                    QtyShip = 3,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                }
            });
        }
        else if (asnId == "LPN-ASN-7003")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "LPN-FB15",
                    LabelCount = 2,
                    Description = "File Set 5pc with Handle",
                    UnitCost = 32.99m,
                    ExtCost = 65.98m,
                    PONumber = "LPN-PO-5003",
                    POLine = 2,
                    NeedFor = "Truck",
                    QtyOnHand = 1,
                    TotalQtyNeeds = 1,
                    QtyShip = 2,
                    PreviouslyReceived = 2,
                    ReceiveQty = 0,
                    Status = "Closed"
                },
                new AsnLine
                {
                    Item = "LPN-TH25",
                    LabelCount = 3,
                    Description = "Thread Repair Kit M10 x 1.5",
                    UnitCost = 78.00m,
                    ExtCost = 234.00m,
                    PONumber = "LPN-PO-5004",
                    POLine = 1,
                    NeedFor = "Stock",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 5,
                    QtyShip = 3,
                    PreviouslyReceived = 3,
                    ReceiveQty = 0,
                    Status = "Closed"
                },
                new AsnLine
                {
                    Item = "LPN-OT40",
                    LabelCount = 5,
                    Description = "Oil Filter Cap Wrench 74mm",
                    UnitCost = 24.99m,
                    ExtCost = 124.95m,
                    PONumber = "LPN-PO-5004",
                    POLine = 2,
                    NeedFor = "Stock",
                    QtyOnHand = 3,
                    TotalQtyNeeds = 4,
                    QtyShip = 5,
                    PreviouslyReceived = 5,
                    ReceiveQty = 0,
                    Status = "Closed"
                }
            });
        }
        else if (asnId == "LPN-ASN-7004")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "LPN-ST60",
                    LabelCount = 2,
                    Description = "Spark Plug Socket Set 3pc",
                    UnitCost = 42.50m,
                    ExtCost = 85.00m,
                    PONumber = "LPN-PO-5005",
                    POLine = 1,
                    NeedFor = "Customer",
                    QtyOnHand = 1,
                    TotalQtyNeeds = 1,
                    QtyShip = 2,
                    PreviouslyReceived = 1,
                    ReceiveQty = 1,
                    Status = "Partial"
                },
                new AsnLine
                {
                    Item = "LPN-FL90",
                    LabelCount = 3,
                    Description = "Flashlight LED Rechargeable",
                    UnitCost = 45.99m,
                    ExtCost = 137.97m,
                    PONumber = "LPN-PO-5005",
                    POLine = 2,
                    NeedFor = "Stock",
                    QtyOnHand = 2,
                    TotalQtyNeeds = 4,
                    QtyShip = 3,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                }
            });
        }
        else if (asnId == "LPN-ASN-7005")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "LPN-MT55",
                    LabelCount = 2,
                    Description = "Multimeter Digital Auto-Ranging",
                    UnitCost = 89.99m,
                    ExtCost = 179.98m,
                    PONumber = "LPN-PO-5006",
                    POLine = 1,
                    NeedFor = "Truck",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 2,
                    QtyShip = 2,
                    PreviouslyReceived = 2,
                    ReceiveQty = 0,
                    Status = "Closed"
                },
                new AsnLine
                {
                    Item = "LPN-WR35",
                    LabelCount = 4,
                    Description = "Wire Stripper/Crimper Tool",
                    UnitCost = 36.50m,
                    ExtCost = 146.00m,
                    PONumber = "LPN-PO-5006",
                    POLine = 2,
                    NeedFor = "Stock",
                    QtyOnHand = 5,
                    TotalQtyNeeds = 3,
                    QtyShip = 4,
                    PreviouslyReceived = 4,
                    ReceiveQty = 0,
                    Status = "Closed"
                },
                new AsnLine
                {
                    Item = "LPN-RW7M",
                    LabelCount = 3,
                    Description = "Ratcheting Wrench 7mm",
                    UnitCost = 28.50m,
                    ExtCost = 85.50m,
                    PONumber = "LPN-PO-5007",
                    POLine = 1,
                    NeedFor = "Stock",
                    QtyOnHand = 2,
                    TotalQtyNeeds = 5,
                    QtyShip = 3,
                    PreviouslyReceived = 3,
                    ReceiveQty = 0,
                    Status = "Closed"
                }
            });
        }
        else if (asnId == "LPN-ASN-7006")
        {
            lines.AddRange(new[]
            {
                new AsnLine
                {
                    Item = "LPN-VG22",
                    LabelCount = 1,
                    Description = "Vacuum Pump & Gauge Kit",
                    UnitCost = 156.00m,
                    ExtCost = 156.00m,
                    PONumber = "LPN-PO-5007",
                    POLine = 2,
                    NeedFor = "Customer",
                    QtyOnHand = 0,
                    TotalQtyNeeds = 1,
                    QtyShip = 1,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                },
                new AsnLine
                {
                    Item = "LPN-AR18",
                    LabelCount = 2,
                    Description = "A/C Service Hose Set",
                    UnitCost = 124.99m,
                    ExtCost = 249.98m,
                    PONumber = "LPN-PO-5007",
                    POLine = 3,
                    NeedFor = "Stock",
                    QtyOnHand = 1,
                    TotalQtyNeeds = 3,
                    QtyShip = 2,
                    PreviouslyReceived = 0,
                    ReceiveQty = 0,
                    Status = "Open"
                }
            });
        }
        else if (asnId == "LPN-ASN-7007")
        {
            lines.Add(new AsnLine
            {
                Item = "LPN-1002",
                LabelCount = 1,
                Description = "Specialty Socket 32mm Deep",
                UnitCost = 123.50m,
                ExtCost = 123.50m,
                PONumber = "LPN-PO-5001",
                POLine = 3,
                NeedFor = "Truck",
                QtyOnHand = 0,
                TotalQtyNeeds = 1,
                QtyShip = 1,
                PreviouslyReceived = 1,
                ReceiveQty = 0,
                Status = "Closed"
            });
        }

        return lines;
    }

    public enum ReceiptStatus { All, Open, Closed }

    public sealed record AsnSummary(
        string AsnId,
        DateTime ShipDate,
        string ShipFrom,
        decimal Total,
        ReceiptStatus Status,
        string Class,
        bool PrintNeedsOnly = false,
        decimal ReceivedAmount = 0m,
        string? TrackingNumber = null,
        string Carrier = "UPS")
    {
        public string Display => $"{AsnId} - {ShipDate:MM/dd/yyyy}";
    }

    public sealed class AsnLine
    {
        public string Item { get; set; } = "";
        public int LabelCount { get; set; }
        public string Description { get; set; } = "";
        public decimal UnitCost { get; set; }
        public decimal ExtCost { get; set; }
        public string PONumber { get; set; } = "";
        public int POLine { get; set; }
        public string NeedFor { get; set; } = "";
        public int QtyOnHand { get; set; }
        public int TotalQtyNeeds { get; set; }
        public int QtyShip { get; set; }
        public int PreviouslyReceived { get; set; }
        public int ReceiveQty { get; set; }
        public string Status { get; set; } = "Open";
    }
}