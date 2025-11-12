using System;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.Components.Pages.POXfer.PO;

public static class PurchaseOrdersSeedData
{
    public static List<PoSummary> GetPoSummaries()
    {
        return new List<PoSummary>
        {
            // CQT Stock POs (from ItemsOnOrderSeedData)
            new PoSummary("PO-0000100802", new DateTime(2025, 08, 15), "Open – Acknowledged", "CQT Stock"),
            new PoSummary("PO-0000100805", new DateTime(2025, 09, 05), "Open – Acknowledged", "CQT Stock"),
            new PoSummary("PO-0000100807", new DateTime(2025, 09, 30), "Open – Acknowledged", "CQT Stock"),
            new PoSummary("PO-0000100821", new DateTime(2025, 09, 28), "Partial", "CQT Stock"),
            new PoSummary("PO-0000100855", new DateTime(2025, 10, 05), "Partial", "CQT Stock"),
            new PoSummary("PO-0000100860", new DateTime(2025, 10, 08), "Partial", "CQT Stock"),
            new PoSummary("PO-0000100870", new DateTime(2025, 10, 12), "Partial", "CQT Stock"),
            new PoSummary("PO-0000100875", new DateTime(2025, 10, 15), "Partial", "CQT Stock"),
            new PoSummary("PO-0000100880", new DateTime(2025, 10, 18), "Partial", "CQT Stock"),
            new PoSummary("PO-0000100890", new DateTime(2025, 10, 20), "Partial", "CQT Stock"),
            new PoSummary("PO-0000100895", new DateTime(2025, 10, 22), "Partial", "CQT Stock"),
            new PoSummary("PO-0000100900", new DateTime(2025, 10, 25), "Partial", "CQT Stock"),
            
            // Local Only POs
            new PoSummary("PO-0000100810", new DateTime(2025, 09, 20), "Partial", "Local Only"),
            new PoSummary("PO-0000100812", new DateTime(2025, 09, 15), "Open – Acknowledged", "Local Only"),
            new PoSummary("PO-0000100815", new DateTime(2025, 09, 10), "Open – Acknowledged", "Local Only"),
            new PoSummary("PO-0000100818", new DateTime(2025, 09, 12), "Partial", "Local Only"),
            new PoSummary("PO-0000100825", new DateTime(2025, 10, 01), "Partial", "Local Only"),
            new PoSummary("PO-0000100830", new DateTime(2025, 10, 03), "Partial", "Local Only")
        };
    }

    public static PurchaseOrder GetPoDetail(string poNumber, string status)
    {
        // CQT Stock POs
        if (poNumber == "PO-0000100807")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 09, 30),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "PRECISION AUTO SERVICE",
                Attention = "1200 Commerce Dr",
                City = "Canton",
                State = "OH",
                Zip = "44720",
                SalesOrderNumber = "0002916666",
                DefaultShipMethod = "UPS Ground",
                PremiumShipMethod = "UPS Next Day",
                Comments = "Orbital sander order",
                SpecialInstructions = "Handle with care - electronic equipment",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "CAT3160", Description = "3/16\" Orbital Sander 6\"", Comment = "Backordered",
                        Ordered = 2, Received = 0, Open = 2, Transit = 0, SalesOrderLine = 1,
                        UnitCost = 299.00m, TotalCost = 598.00m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100821")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 09, 28),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "JOHNSON AUTO REPAIR",
                Attention = "2340 Main Street",
                City = "Akron",
                State = "OH",
                Zip = "44310",
                SalesOrderNumber = "0002917777",
                DefaultShipMethod = "UPS Ground",
                PremiumShipMethod = "UPS 2-Day",
                Comments = "Adapter set order",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "A20", Description = "3/8\" Drive (F) x 1/2\" (M) Adapter", Comment = "In Transit",
                        Ordered = 5, Received = 3, Open = 2, Transit = 2, SalesOrderLine = 3,
                        UnitCost = 45.00m, TotalCost = 225.00m, Status = "Open" },
                    new() { Item = "A02", Description = "1/4\" Drive (F) x 3/8\" (M) Adapter", Comment = "Backordered",
                        Ordered = 1, Received = 0, Open = 1, Transit = 0, SalesOrderLine = 4,
                        UnitCost = 23.50m, TotalCost = 23.50m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100855")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 10, 05),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "BROWN'S AUTOMOTIVE",
                Attention = "880 State Route 44",
                City = "Ravenna",
                State = "OH",
                Zip = "44266",
                SalesOrderNumber = "0002918888",
                DefaultShipMethod = "UPS Ground",
                PremiumShipMethod = "UPS Next Day",
                Comments = "Impact socket set - large order",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "B450", Description = "1/2\" Impact Socket Set, 15pc", Comment = "Partially Received",
                        Ordered = 10, Received = 5, Open = 5, Transit = 3, SalesOrderLine = 1,
                        UnitCost = 189.99m, TotalCost = 1899.90m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100802")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 08, 15),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "SMITH'S GARAGE",
                Attention = "550 Industrial Pkwy",
                City = "Cleveland",
                State = "OH",
                Zip = "44114",
                SalesOrderNumber = "0002915555",
                DefaultShipMethod = "UPS Ground",
                PremiumShipMethod = "UPS 2-Day",
                Comments = "Torque wrench - backordered",
                SpecialInstructions = "Customer needs ASAP - follow up with vendor",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "C120", Description = "Torque Wrench 1/2\" Drive 50-250 ft-lb", Comment = "Backordered",
                        Ordered = 2, Received = 0, Open = 2, Transit = 0, SalesOrderLine = 2,
                        UnitCost = 425.00m, TotalCost = 850.00m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100870")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 10, 12),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "ANDERSON'S AUTO",
                Attention = "1650 Market Ave N",
                City = "Canton",
                State = "OH",
                Zip = "44714",
                SalesOrderNumber = "0002919999",
                DefaultShipMethod = "UPS Ground",
                PremiumShipMethod = "UPS Next Day",
                Comments = "Ratcheting wrench set order",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "D330", Description = "Ratcheting Wrench Set SAE 10pc", Comment = "In Transit",
                        Ordered = 8, Received = 2, Open = 6, Transit = 4, SalesOrderLine = 1,
                        UnitCost = 195.00m, TotalCost = 1560.00m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100890")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 10, 20),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "MILLER AUTO CLINIC",
                Attention = "425 W Market St",
                City = "Warren",
                State = "OH",
                Zip = "44481",
                SalesOrderNumber = "0002920000",
                DefaultShipMethod = "UPS Ground",
                PremiumShipMethod = "—",
                Comments = "Bulk air tool oil order",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "AF12201", Description = "1220-1 AIR TOOL OIL PINT", Comment = "Large Bulk Order",
                        Ordered = 50, Received = 32, Open = 18, Transit = 18, SalesOrderLine = 1,
                        UnitCost = 10.00m, TotalCost = 500.00m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100895")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 10, 22),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "WILSON'S SERVICE CENTER",
                Attention = "2800 Cleveland Rd",
                City = "Wooster",
                State = "OH",
                Zip = "44691",
                SalesOrderNumber = "0002920111",
                DefaultShipMethod = "UPS Ground",
                PremiumShipMethod = "UPS Next Day",
                Comments = "Mechanic's gloves - bulk order",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "H770", Description = "Mechanic's Gloves XL", Comment = "Bulk Order",
                        Ordered = 30, Received = 10, Open = 20, Transit = 15, SalesOrderLine = 2,
                        UnitCost = 24.99m, TotalCost = 749.70m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100880")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 10, 18),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "TAYLOR AUTO REPAIR",
                Attention = "500 E Waterloo Rd",
                City = "Akron",
                State = "OH",
                Zip = "44319",
                SalesOrderNumber = "0002919500",
                DefaultShipMethod = "UPS Ground",
                PremiumShipMethod = "UPS Next Day",
                Comments = "Brake caliper tool kit",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "M105", Description = "Brake Caliper Tool Kit", Comment = "In Transit",
                        Ordered = 6, Received = 2, Open = 4, Transit = 2, SalesOrderLine = 3,
                        UnitCost = 156.00m, TotalCost = 936.00m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100805")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 09, 05),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "DAVIS REPAIR SHOP",
                Attention = "3200 Pearl Rd",
                City = "Medina",
                State = "OH",
                Zip = "44256",
                SalesOrderNumber = "0002916000",
                DefaultShipMethod = "UPS Ground",
                PremiumShipMethod = "UPS 2-Day",
                Comments = "Creeper - backordered",
                SpecialInstructions = "Customer waiting - expedite when available",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "K990", Description = "Creeper Low Profile", Comment = "Backordered",
                        Ordered = 2, Received = 0, Open = 2, Transit = 0, SalesOrderLine = 1,
                        UnitCost = 125.00m, TotalCost = 250.00m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100860")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 10, 08),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "MARTIN'S GARAGE",
                Attention = "1100 S Main St",
                City = "North Canton",
                State = "OH",
                Zip = "44720",
                SalesOrderNumber = "0002919100",
                DefaultShipMethod = "UPS Ground",
                PremiumShipMethod = "—",
                Comments = "Deep socket set",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "B451", Description = "3/8\" Deep Socket Set, 12pc", Comment = "Partially Received",
                        Ordered = 4, Received = 1, Open = 3, Transit = 1, SalesOrderLine = 2,
                        UnitCost = 145.50m, TotalCost = 582.00m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100875")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 10, 15),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "ROBINSON AUTO SERVICE",
                Attention = "2500 Fulton Dr NW",
                City = "Canton",
                State = "OH",
                Zip = "44718",
                SalesOrderNumber = "0002919300",
                DefaultShipMethod = "FedEx Ground",
                PremiumShipMethod = "FedEx Priority",
                Comments = "Dead blow hammer",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "G665", Description = "Dead Blow Hammer 3lb", Comment = "In Transit",
                        Ordered = 4, Received = 1, Open = 3, Transit = 1, SalesOrderLine = 1,
                        UnitCost = 58.99m, TotalCost = 235.96m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100900")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 10, 25),
                VendorName = "Cornwell Quality Tools",
                ShipToName = "GARCIA AUTO WORKS",
                Attention = "780 Enterprise Dr",
                City = "Barberton",
                State = "OH",
                Zip = "44203",
                SalesOrderNumber = "0002920500",
                DefaultShipMethod = "UPS Ground",
                PremiumShipMethod = "—",
                Comments = "Magnetic pick-up tools - bulk order",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "P305", Description = "Magnetic Pick-Up Tool Telescoping", Comment = "Bulk Order",
                        Ordered = 20, Received = 5, Open = 15, Transit = 12, SalesOrderLine = 1,
                        UnitCost = 19.99m, TotalCost = 399.80m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        // Local Only POs
        else if (poNumber == "PO-0000100810")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 09, 20),
                VendorName = "Local Specialty Supplier",
                ShipToName = "LOPEZ REPAIR CENTER",
                Attention = "1450 S Arlington St",
                City = "Akron",
                State = "OH",
                Zip = "44306",
                SalesOrderNumber = "0002917000",
                DefaultShipMethod = "Local Delivery",
                PremiumShipMethod = "Same Day",
                Comments = "Local specialty brake tools",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "LPN-BC45", Description = "Brake Caliper Piston Tool", Comment = "Partially Received",
                        Ordered = 5, Received = 2, Open = 3, Transit = 1, SalesOrderLine = 5,
                        UnitCost = 67.50m, TotalCost = 337.50m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100815")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 09, 10),
                VendorName = "Local Specialty Supplier",
                ShipToName = "THOMPSON AUTOMOTIVE",
                Attention = "3500 Manchester Rd",
                City = "Akron",
                State = "OH",
                Zip = "44319",
                SalesOrderNumber = "0002917111",
                DefaultShipMethod = "Local Pickup",
                PremiumShipMethod = "Rush Delivery",
                Comments = "Torque wrench - special order",
                SpecialInstructions = "Special order item - awaiting supplier",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "LPN-TW250", Description = "Torque Wrench 3/8\" Drive 25-250 in-lb", Comment = "Backordered",
                        Ordered = 2, Received = 0, Open = 2, Transit = 0, SalesOrderLine = 1,
                        UnitCost = 189.99m, TotalCost = 379.98m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100825")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 10, 01),
                VendorName = "Local Tool Distributors",
                ShipToName = "HARRIS AUTO CLINIC",
                Attention = "950 Vernon Odom Blvd",
                City = "Akron",
                State = "OH",
                Zip = "44307",
                DefaultShipMethod = "Local Delivery",
                PremiumShipMethod = "Same Day",
                Comments = "Multimeter order",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "LPN-MT55", Description = "Multimeter Digital Auto-Ranging", Comment = "Partially Received",
                        Ordered = 4, Received = 1, Open = 3, Transit = 1, SalesOrderLine = 2,
                        UnitCost = 89.99m, TotalCost = 359.96m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100812")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 09, 15),
                VendorName = "Local Parts & Tools",
                ShipToName = "CLARK'S SERVICE STATION",
                Attention = "1800 Romig Rd",
                City = "Akron",
                State = "OH",
                Zip = "44320",
                SalesOrderNumber = "0002917200",
                DefaultShipMethod = "Local Delivery",
                PremiumShipMethod = "—",
                Comments = "Vacuum pump kit - special order",
                SpecialInstructions = "Special order - checking availability",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "LPN-VG22", Description = "Vacuum Pump & Gauge Kit", Comment = "Backordered",
                        Ordered = 1, Received = 0, Open = 1, Transit = 0, SalesOrderLine = 1,
                        UnitCost = 156.00m, TotalCost = 156.00m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100830")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 10, 03),
                VendorName = "Local Tool Distributors",
                ShipToName = "RODRIGUEZ AUTOMOTIVE",
                Attention = "2200 Brittain Rd",
                City = "Akron",
                State = "OH",
                Zip = "44310",
                SalesOrderNumber = "0002918200",
                DefaultShipMethod = "Local Delivery",
                PremiumShipMethod = "Same Day",
                Comments = "Chisel set order",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "LPN-CH80", Description = "Chisel Set 8pc", Comment = "Partially Received",
                        Ordered = 5, Received = 2, Open = 3, Transit = 1, SalesOrderLine = 1,
                        UnitCost = 48.50m, TotalCost = 242.50m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }
        else if (poNumber == "PO-0000100818")
        {
            var po = new PurchaseOrder
            {
                PoNumber = poNumber,
                PoStatus = status,
                PoDate = new DateTime(2025, 09, 12),
                VendorName = "Local Specialty Supplier",
                ShipToName = "PETERSON'S AUTO SHOP",
                Attention = "4100 Medina Rd",
                City = "Akron",
                State = "OH",
                Zip = "44333",
                SalesOrderNumber = "0002917500",
                DefaultShipMethod = "Local Delivery",
                PremiumShipMethod = "Rush Delivery",
                Comments = "A/C service hose set",
                RallyPricing = false,
                NoPaperwork = true,
                LiftGateRequired = false,
                ReleaseBackorders = false,
                BackorderUnfilled = false,
                Lines = new List<PurchaseOrderLine>
                {
                    new() { Item = "LPN-AR18", Description = "A/C Service Hose Set", Comment = "Partially Received",
                        Ordered = 3, Received = 1, Open = 2, Transit = 1, SalesOrderLine = 1,
                        UnitCost = 124.99m, TotalCost = 374.97m, Status = "Open" }
                }
            };
            po.TotalCost = po.Lines.Sum(l => l.TotalCost);
            po.OpenCost = po.Lines.Where(l => l.Open > 0).Sum(l => l.TotalCost);
            return po;
        }

        // Return empty PO if not found
        return new PurchaseOrder();
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