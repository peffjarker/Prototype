using System;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.Pages.POTransfer.SeedData;

public static class PurchaseOrdersSeedData
{
    public static List<PoSummary> GetPoSummaries()
    {
        return new List<PoSummary>
        {
            // CQT Stock POs
            new PoSummary("PO-200996", new DateTime(2025, 07, 29), "Closed", "CQT Stock"),
            new PoSummary("PO-200995", new DateTime(2025, 07, 24), "Open – Acknowledged", "CQT Stock"),
            new PoSummary("PO-200994", new DateTime(2025, 07, 22), "Pending", "CQT Stock"),
            new PoSummary("PO-000010929", new DateTime(2025, 07, 09), "Closed", "CQT Stock"),
            new PoSummary("PO-201001", new DateTime(2025, 08, 15), "Open – Acknowledged", "CQT Stock"),
            new PoSummary("PO-201002", new DateTime(2025, 08, 20), "In Process", "CQT Stock"),
            new PoSummary("PO-201003", new DateTime(2025, 09, 05), "Issued – UnAcknowledged", "CQT Stock"),
            new PoSummary("PO-201004", new DateTime(2025, 09, 12), "Open – Acknowledged", "CQT Stock"),
            new PoSummary("PO-201005", new DateTime(2025, 09, 18), "Pending", "CQT Stock"),
            new PoSummary("PO-201006", new DateTime(2025, 10, 01), "In Process", "CQT Stock"),
            new PoSummary("PO-201007", new DateTime(2025, 10, 08), "Open – Acknowledged", "CQT Stock"),
            new PoSummary("PO-201008", new DateTime(2025, 10, 15), "Cancelled", "CQT Stock"),
            
            // Local Only POs (LPN- prefix)
            new PoSummary("PO-5001", new DateTime(2025, 08, 10), "Closed", "Local Only"),
            new PoSummary("PO-5002", new DateTime(2025, 08, 25), "Open – Acknowledged", "Local Only"),
            new PoSummary("PO-5003", new DateTime(2025, 09, 08), "Pending", "Local Only"),
            new PoSummary("PO-5004", new DateTime(2025, 09, 22), "In Process", "Local Only"),
            new PoSummary("PO-5005", new DateTime(2025, 10, 05), "Open – Acknowledged", "Local Only"),
            new PoSummary("PO-5006", new DateTime(2025, 10, 12), "Issued – UnAcknowledged", "Local Only"),
            new PoSummary("PO-5007", new DateTime(2025, 10, 20), "Pending", "Local Only")
        };
    }

    public static PurchaseOrder GetPoDetail(string poNumber, string status)
    {
        // Define base PO data
        var po = new PurchaseOrder
        {
            PoNumber = poNumber,
            PoStatus = status,
            RallyPricing = false,
            NoPaperwork = true,
            LiftGateRequired = false,
            ReleaseBackorders = false,
            BackorderUnfilled = false
        };

        // CQT Stock POs
        if (poNumber == "PO-200996")
        {
            po.PoDate = new DateTime(2025, 07, 29);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "TODARO, MEL J.";
            po.Attention = "1405 Hightower Dr";
            po.City = "Uniontown";
            po.State = "OH";
            po.Zip = "44685";
            po.SalesOrderNumber = "0003285168";
            po.DefaultShipMethod = "UPS";
            po.PremiumShipMethod = "—";
            po.Comments = "Purchase Order Added via CQT";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "THCE5401", Description = "Articulating Borescope, 5\" 6mm Cam", Comment = "Added - CQT",
                    Ordered = 1, Received = 1, Open = 0, Transit = 0, UnitCost = 299.99m, TotalCost = 299.99m, Status = "Closed" },
                new() { Item = "PFNPSB8", Description = "Sovereign True Wriss Bluetooth Earplugs", Comment = "Added - CQT",
                    Ordered = 1, Received = 1, Open = 0, Transit = 0, UnitCost = 89.99m, TotalCost = 89.99m, Status = "Closed" }
            };
        }
        else if (poNumber == "PO-200995")
        {
            po.PoDate = new DateTime(2025, 07, 24);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "JOHNSON AUTO REPAIR";
            po.Attention = "2340 Main Street";
            po.City = "Akron";
            po.State = "OH";
            po.Zip = "44310";
            po.SalesOrderNumber = "0003285170";
            po.DefaultShipMethod = "FedEx";
            po.PremiumShipMethod = "FedEx Overnight";
            po.Comments = "Rush order for customer";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "WT5500", Description = "5-Piece Wrench Set", Comment = "Pending Shipment",
                    Ordered = 2, Received = 0, Open = 2, Transit = 2, SalesOrderLine = 5, UnitCost = 45.50m, TotalCost = 91.00m, Status = "Open" },
                new() { Item = "B450", Description = "1/2\" Impact Socket Set, 15pc", Comment = "In Transit",
                    Ordered = 3, Received = 1, Open = 2, Transit = 2, SalesOrderLine = 6, UnitCost = 189.99m, TotalCost = 569.97m, Status = "Open" },
                new() { Item = "C120", Description = "Torque Wrench 1/2\" Drive 50-250 ft-lb", Comment = "Backordered",
                    Ordered = 1, Received = 0, Open = 1, Transit = 0, SalesOrderLine = 7, UnitCost = 425.00m, TotalCost = 425.00m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-200994")
        {
            po.PoDate = new DateTime(2025, 07, 22);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "SMITH'S GARAGE";
            po.Attention = "550 Industrial Pkwy";
            po.City = "Cleveland";
            po.State = "OH";
            po.Zip = "44114";
            po.SalesOrderNumber = "0003285165";
            po.DefaultShipMethod = "UPS Ground";
            po.PremiumShipMethod = "UPS 2-Day";
            po.Comments = "Standard order";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "AF12201", Description = "1220-1 AIR TOOL OIL PINT", Comment = "Pending",
                    Ordered = 12, Received = 0, Open = 12, Transit = 0, UnitCost = 10.00m, TotalCost = 120.00m, Status = "Open" },
                new() { Item = "H770", Description = "Mechanic's Gloves XL", Comment = "Pending",
                    Ordered = 6, Received = 0, Open = 6, Transit = 0, UnitCost = 24.99m, TotalCost = 149.94m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-000010929")
        {
            po.PoDate = new DateTime(2025, 07, 09);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "PRECISION AUTO SERVICE";
            po.Attention = "1200 Commerce Dr";
            po.City = "Canton";
            po.State = "OH";
            po.Zip = "44720";
            po.SalesOrderNumber = "0003285150";
            po.DefaultShipMethod = "UPS";
            po.PremiumShipMethod = "—";
            po.Comments = "Completed order";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "D330", Description = "Ratcheting Wrench Set SAE 10pc", Comment = "Delivered",
                    Ordered = 2, Received = 2, Open = 0, Transit = 0, UnitCost = 195.00m, TotalCost = 390.00m, Status = "Closed" },
                new() { Item = "E440", Description = "Pliers Set 4pc", Comment = "Delivered",
                    Ordered = 3, Received = 3, Open = 0, Transit = 0, UnitCost = 89.99m, TotalCost = 269.97m, Status = "Closed" }
            };
        }
        else if (poNumber == "PO-201001")
        {
            po.PoDate = new DateTime(2025, 08, 15);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "BROWN'S AUTOMOTIVE";
            po.Attention = "880 State Route 44";
            po.City = "Ravenna";
            po.State = "OH";
            po.Zip = "44266";
            po.SalesOrderNumber = "0003285180";
            po.DefaultShipMethod = "UPS Ground";
            po.PremiumShipMethod = "UPS Next Day";
            po.Comments = "Large order - multiple lines";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "F550", Description = "Screwdriver Set 10pc", Comment = "In Transit",
                    Ordered = 5, Received = 2, Open = 3, Transit = 3, UnitCost = 67.99m, TotalCost = 339.95m, Status = "Open" },
                new() { Item = "G660", Description = "Ball Peen Hammer 24oz", Comment = "Partially Received",
                    Ordered = 4, Received = 2, Open = 2, Transit = 1, UnitCost = 42.50m, TotalCost = 170.00m, Status = "Open" },
                new() { Item = "M100", Description = "Oil Filter Wrench Set 3pc", Comment = "Pending",
                    Ordered = 6, Received = 0, Open = 6, Transit = 0, UnitCost = 45.99m, TotalCost = 275.94m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-201002")
        {
            po.PoDate = new DateTime(2025, 08, 20);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "MILLER AUTO CLINIC";
            po.Attention = "425 W Market St";
            po.City = "Warren";
            po.State = "OH";
            po.Zip = "44481";
            po.SalesOrderNumber = "0003285185";
            po.DefaultShipMethod = "FedEx";
            po.PremiumShipMethod = "—";
            po.Comments = "Processing order";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "K990", Description = "Creeper Low Profile", Comment = "Processing",
                    Ordered = 2, Received = 0, Open = 2, Transit = 0, UnitCost = 125.00m, TotalCost = 250.00m, Status = "Open" },
                new() { Item = "N200", Description = "Battery Terminal Cleaner", Comment = "Processing",
                    Ordered = 8, Received = 0, Open = 8, Transit = 0, UnitCost = 18.50m, TotalCost = 148.00m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-201003")
        {
            po.PoDate = new DateTime(2025, 09, 05);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "DAVIS REPAIR SHOP";
            po.Attention = "3200 Pearl Rd";
            po.City = "Medina";
            po.State = "OH";
            po.Zip = "44256";
            po.SalesOrderNumber = "0003285190";
            po.DefaultShipMethod = "UPS";
            po.PremiumShipMethod = "UPS 2-Day";
            po.Comments = "Awaiting acknowledgment";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "P300", Description = "Magnetic Parts Tray 6\"", Comment = "Unacknowledged",
                    Ordered = 10, Received = 0, Open = 10, Transit = 0, UnitCost = 14.99m, TotalCost = 149.90m, Status = "Open" },
                new() { Item = "J885", Description = "Parts Cleaner Spray 16oz", Comment = "Unacknowledged",
                    Ordered = 15, Received = 0, Open = 15, Transit = 0, UnitCost = 12.50m, TotalCost = 187.50m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-201004")
        {
            po.PoDate = new DateTime(2025, 09, 12);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "ANDERSON'S AUTO";
            po.Attention = "1650 Market Ave N";
            po.City = "Canton";
            po.State = "OH";
            po.Zip = "44714";
            po.SalesOrderNumber = "0003285195";
            po.DefaultShipMethod = "UPS Ground";
            po.PremiumShipMethod = "—";
            po.Comments = "Regular replenishment";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "C125", Description = "Digital Torque Adapter 1/4\" Drive", Comment = "In Transit",
                    Ordered = 2, Received = 0, Open = 2, Transit = 2, UnitCost = 275.00m, TotalCost = 550.00m, Status = "Open" },
                new() { Item = "B451", Description = "3/8\" Deep Socket Set, 12pc", Comment = "Backordered",
                    Ordered = 3, Received = 0, Open = 3, Transit = 0, UnitCost = 145.50m, TotalCost = 436.50m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-201005")
        {
            po.PoDate = new DateTime(2025, 09, 18);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "WILSON'S SERVICE CENTER";
            po.Attention = "2800 Cleveland Rd";
            po.City = "Wooster";
            po.State = "OH";
            po.Zip = "44691";
            po.SalesOrderNumber = "0003285200";
            po.DefaultShipMethod = "FedEx Ground";
            po.PremiumShipMethod = "FedEx Priority";
            po.Comments = "Pending approval";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "D335", Description = "Ratcheting Wrench Set Metric 10pc", Comment = "Pending",
                    Ordered = 4, Received = 0, Open = 4, Transit = 0, UnitCost = 195.00m, TotalCost = 780.00m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-201006")
        {
            po.PoDate = new DateTime(2025, 10, 01);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "TAYLOR AUTO REPAIR";
            po.Attention = "500 E Waterloo Rd";
            po.City = "Akron";
            po.State = "OH";
            po.Zip = "44319";
            po.SalesOrderNumber = "0003285205";
            po.DefaultShipMethod = "UPS";
            po.PremiumShipMethod = "UPS Next Day";
            po.Comments = "Rush processing";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "M105", Description = "Brake Caliper Tool Kit", Comment = "Processing",
                    Ordered = 2, Received = 0, Open = 2, Transit = 0, UnitCost = 156.00m, TotalCost = 312.00m, Status = "Open" },
                new() { Item = "AS6SB", Description = "Stubby Air Slide Hammer", Comment = "Processing",
                    Ordered = 1, Received = 0, Open = 1, Transit = 0, UnitCost = 159.99m, TotalCost = 159.99m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-201007")
        {
            po.PoDate = new DateTime(2025, 10, 08);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "MARTIN'S GARAGE";
            po.Attention = "1100 S Main St";
            po.City = "North Canton";
            po.State = "OH";
            po.Zip = "44720";
            po.SalesOrderNumber = "0003285210";
            po.DefaultShipMethod = "UPS Ground";
            po.PremiumShipMethod = "—";
            po.Comments = "Standard order";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "H775", Description = "Mechanic's Gloves L", Comment = "Shipped",
                    Ordered = 10, Received = 5, Open = 5, Transit = 5, UnitCost = 24.99m, TotalCost = 249.90m, Status = "Open" },
                new() { Item = "J880", Description = "Shop Towels (Box of 200)", Comment = "In Transit",
                    Ordered = 8, Received = 0, Open = 8, Transit = 8, UnitCost = 15.99m, TotalCost = 127.92m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-201008")
        {
            po.PoDate = new DateTime(2025, 10, 15);
            po.VendorName = "Cornwell Quality Tools";
            po.ShipToName = "ROBINSON AUTO SERVICE";
            po.Attention = "2500 Fulton Dr NW";
            po.City = "Canton";
            po.State = "OH";
            po.Zip = "44718";
            po.SalesOrderNumber = "0003285215";
            po.DefaultShipMethod = "FedEx";
            po.PremiumShipMethod = "—";
            po.Comments = "Order cancelled by customer";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "CAT3160", Description = "3/16\" Orbital Sander 6\"", Comment = "Cancelled",
                    Ordered = 1, Received = 0, Open = 0, Transit = 0, UnitCost = 299.00m, TotalCost = 299.00m, Status = "Cancelled", CancelFlag = 1 }
            };
        }
        // Local Only POs
        else if (poNumber == "PO-5001")
        {
            po.PoDate = new DateTime(2025, 08, 10);
            po.VendorName = "Local Specialty Supplier";
            po.ShipToName = "GARCIA AUTO WORKS";
            po.Attention = "780 Enterprise Dr";
            po.City = "Barberton";
            po.State = "OH";
            po.Zip = "44203";
            po.SalesOrderNumber = "0003285175";
            po.DefaultShipMethod = "Local Delivery";
            po.PremiumShipMethod = "Same Day";
            po.Comments = "Local specialty order - completed";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "LPN-BC45", Description = "Brake Caliper Piston Tool", Comment = "Delivered",
                    Ordered = 2, Received = 2, Open = 0, Transit = 0, UnitCost = 67.50m, TotalCost = 135.00m, Status = "Closed" },
                new() { Item = "LPN-TW250", Description = "Torque Wrench 3/8\" Drive 25-250 in-lb", Comment = "Delivered",
                    Ordered = 1, Received = 1, Open = 0, Transit = 0, UnitCost = 189.99m, TotalCost = 189.99m, Status = "Closed" }
            };
        }
        else if (poNumber == "PO-5002")
        {
            po.PoDate = new DateTime(2025, 08, 25);
            po.VendorName = "Local Tool Distributors";
            po.ShipToName = "LOPEZ REPAIR CENTER";
            po.Attention = "1450 S Arlington St";
            po.City = "Akron";
            po.State = "OH";
            po.Zip = "44306";
            po.SalesOrderNumber = "0003285182";
            po.DefaultShipMethod = "Local Pickup";
            po.PremiumShipMethod = "Rush Delivery";
            po.Comments = "Specialty tools in stock";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "LPN-HS12", Description = "Hose Clamp Pliers Set", Comment = "Ready for pickup",
                    Ordered = 3, Received = 1, Open = 2, Transit = 0, UnitCost = 54.99m, TotalCost = 164.97m, Status = "Open" },
                new() { Item = "LPN-PB20", Description = "Pry Bar Set 3pc", Comment = "In stock",
                    Ordered = 4, Received = 2, Open = 2, Transit = 0, UnitCost = 39.99m, TotalCost = 159.96m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-5003")
        {
            po.PoDate = new DateTime(2025, 09, 08);
            po.VendorName = "Local Parts & Tools";
            po.ShipToName = "THOMPSON AUTOMOTIVE";
            po.Attention = "3500 Manchester Rd";
            po.City = "Akron";
            po.State = "OH";
            po.Zip = "44319";
            po.SalesOrderNumber = "0003285192";
            po.DefaultShipMethod = "Local Delivery";
            po.PremiumShipMethod = "—";
            po.Comments = "Pending local approval";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "LPN-CH80", Description = "Chisel Set 8pc", Comment = "Pending",
                    Ordered = 2, Received = 0, Open = 2, Transit = 0, UnitCost = 48.50m, TotalCost = 97.00m, Status = "Open" },
                new() { Item = "LPN-FB15", Description = "File Set 5pc with Handle", Comment = "Pending",
                    Ordered = 3, Received = 0, Open = 3, Transit = 0, UnitCost = 32.99m, TotalCost = 98.97m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-5004")
        {
            po.PoDate = new DateTime(2025, 09, 22);
            po.VendorName = "Local Specialty Supplier";
            po.ShipToName = "HARRIS AUTO CLINIC";
            po.Attention = "950 Vernon Odom Blvd";
            po.City = "Akron";
            po.State = "OH";
            po.Zip = "44307";
            po.SalesOrderNumber = "0003285197";
            po.DefaultShipMethod = "Local Delivery";
            po.PremiumShipMethod = "Same Day";
            po.Comments = "Processing special order";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "LPN-TH25", Description = "Thread Repair Kit M10 x 1.5", Comment = "Processing",
                    Ordered = 2, Received = 0, Open = 2, Transit = 0, UnitCost = 78.00m, TotalCost = 156.00m, Status = "Open" },
                new() { Item = "LPN-OT40", Description = "Oil Filter Cap Wrench 74mm", Comment = "Processing",
                    Ordered = 4, Received = 0, Open = 4, Transit = 0, UnitCost = 24.99m, TotalCost = 99.96m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-5005")
        {
            po.PoDate = new DateTime(2025, 10, 05);
            po.VendorName = "Local Tool Distributors";
            po.ShipToName = "CLARK'S SERVICE STATION";
            po.Attention = "1800 Romig Rd";
            po.City = "Akron";
            po.State = "OH";
            po.Zip = "44320";
            po.SalesOrderNumber = "0003285207";
            po.DefaultShipMethod = "Local Pickup";
            po.PremiumShipMethod = "—";
            po.Comments = "Regular local order";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "LPN-ST60", Description = "Spark Plug Socket Set 3pc", Comment = "Ready",
                    Ordered = 5, Received = 2, Open = 3, Transit = 0, UnitCost = 42.50m, TotalCost = 212.50m, Status = "Open" },
                new() { Item = "LPN-FL90", Description = "Flashlight LED Rechargeable", Comment = "Partially Picked Up",
                    Ordered = 6, Received = 3, Open = 3, Transit = 0, UnitCost = 45.99m, TotalCost = 275.94m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-5006")
        {
            po.PoDate = new DateTime(2025, 10, 12);
            po.VendorName = "Local Parts & Tools";
            po.ShipToName = "RODRIGUEZ AUTOMOTIVE";
            po.Attention = "2200 Brittain Rd";
            po.City = "Akron";
            po.State = "OH";
            po.Zip = "44310";
            po.SalesOrderNumber = "0003285212";
            po.DefaultShipMethod = "Local Delivery";
            po.PremiumShipMethod = "Same Day";
            po.Comments = "Awaiting vendor confirmation";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "LPN-MT55", Description = "Multimeter Digital Auto-Ranging", Comment = "Unacknowledged",
                    Ordered = 2, Received = 0, Open = 2, Transit = 0, UnitCost = 89.99m, TotalCost = 179.98m, Status = "Open" },
                new() { Item = "LPN-WR35", Description = "Wire Stripper/Crimper Tool", Comment = "Unacknowledged",
                    Ordered = 5, Received = 0, Open = 5, Transit = 0, UnitCost = 36.50m, TotalCost = 182.50m, Status = "Open" }
            };
        }
        else if (poNumber == "PO-5007")
        {
            po.PoDate = new DateTime(2025, 10, 20);
            po.VendorName = "Local Specialty Supplier";
            po.ShipToName = "PETERSON'S AUTO SHOP";
            po.Attention = "4100 Medina Rd";
            po.City = "Akron";
            po.State = "OH";
            po.Zip = "44333";
            po.SalesOrderNumber = "0003285217";
            po.DefaultShipMethod = "Local Delivery";
            po.PremiumShipMethod = "Rush Delivery";
            po.Comments = "Pending approval";
            po.Lines = new List<PurchaseOrderLine>
            {
                new() { Item = "LPN-VG22", Description = "Vacuum Pump & Gauge Kit", Comment = "Pending",
                    Ordered = 1, Received = 0, Open = 1, Transit = 0, UnitCost = 156.00m, TotalCost = 156.00m, Status = "Open" },
                new() { Item = "LPN-AR18", Description = "A/C Service Hose Set", Comment = "Pending",
                    Ordered = 2, Received = 0, Open = 2, Transit = 0, UnitCost = 124.99m, TotalCost = 249.98m, Status = "Open" },
                new() { Item = "LPN-RW7M", Description = "Ratcheting Wrench 7mm", Comment = "Pending",
                    Ordered = 3, Received = 0, Open = 3, Transit = 0, UnitCost = 28.50m, TotalCost = 85.50m, Status = "Open" }
            };
        }
        else
        {
            // Return empty PO if not found
            return po;
        }

        // Calculate totals
        po.TotalCost = po.Lines.Sum(l => l.TotalCost);
        po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);

        return po;
    }

    public sealed record PoSummary(string Number, DateTime Date, string Status, string Class)
    {
        public string Display => $"{Number} - {Date:MM/dd/yyyy}";
    }

    public sealed class PurchaseOrder
    {
        public string PoNumber { get; set; } = "";
        public DateTime PoDate { get; set; } = DateTime.Today;
        public string VendorName { get; set; } = "";
        public string ShipToName { get; set; } = "";
        public string Attention { get; set; } = "";
        public string Address1 { get; set; } = "";
        public string Address2 { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Zip { get; set; } = "";
        public string SpecialInstructions { get; set; } = "";

        public string SalesOrderNumber { get; set; } = "";
        public string PoStatus { get; set; } = "";
        public string DefaultShipMethod { get; set; } = "";
        public string PremiumShipMethod { get; set; } = "";
        public bool RallyPricing { get; set; }
        public bool NoPaperwork { get; set; }
        public bool LiftGateRequired { get; set; }
        public decimal TotalCost { get; set; }
        public decimal OpenCost { get; set; }
        public string Comments { get; set; } = "";
        public bool ReleaseBackorders { get; set; }
        public bool BackorderUnfilled { get; set; }

        public List<PurchaseOrderLine> Lines { get; set; } = new();
    }

    public sealed class PurchaseOrderLine
    {
        public string Item { get; set; } = "";
        public string Description { get; set; } = "";
        public string Comment { get; set; } = "";
        public int Ordered { get; set; }
        public int Received { get; set; }
        public int Open { get; set; }
        public string DefCbx { get; set; } = "None";
        public int Transit { get; set; }
        public int SalesOrderLine { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
        public int RallyUnit { get; set; }
        public int RallyExt { get; set; }
        public string Status { get; set; } = "";
        public int CancelFlag { get; set; }
    }
}