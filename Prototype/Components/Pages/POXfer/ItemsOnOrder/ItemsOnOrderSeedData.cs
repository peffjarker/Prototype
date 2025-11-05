using System;
using System.Collections.Generic;

namespace Prototype.Components.Pages.POXfer.ItemsOnOrder;

public static class ItemsOnOrderSeedData
{
    public static List<Row> GetItems()
    {
        var items = new List<Row>();

        // CQT Stock items
        items.AddRange(new[]
        {
            new Row("A02", "1/4\" Drive (F) x 3/8\" (M) Adapter", "CQT Stock", 1, 0, 0, 1, 23.50m),
            new Row("A20", "3/8\" Drive (F) x 1/2\" (M) Adapter", "CQT Stock", 2, 2, 0, 0, 45.00m),
            new Row("A23", "3/8\" Drive (F) x 1/2\" (M) Adapter", "CQT Stock", 1, 1, 0, 1, 22.10m),
            new Row("A35", "1/2\" Drive (F) x 3/4\" (M) Adapter", "CQT Stock", 1, 0, 0, 0, 18.75m),
            new Row("AF12201", "1220-1 AIR TOOL OIL PINT", "CQT Stock", 18, 18, 0, 0, 10.00m),
            new Row("AP328", "1/2\"Dr.(F) x 1/2\"(M) Imp Adapt,Ball Type", "CQT Stock", 1, 1, 0, 0, 19.30m),
            new Row("AS6SB", "Stubby Air Slide Hammer", "CQT Stock", 1, 1, 0, 0, 159.99m),
            new Row("CAT3160", "3/16\" Orbital Sander 6\"", "CQT Stock", 1, 2, 0, 1, 299.00m),
            new Row("B450", "1/2\" Impact Socket Set, 15pc", "CQT Stock", 5, 3, 2, 0, 189.99m),
            new Row("B451", "3/8\" Deep Socket Set, 12pc", "CQT Stock", 3, 1, 1, 1, 145.50m),
            new Row("C120", "Torque Wrench 1/2\" Drive 50-250 ft-lb", "CQT Stock", 2, 0, 0, 2, 425.00m),
            new Row("C125", "Digital Torque Adapter 1/4\" Drive", "CQT Stock", 4, 2, 1, 1, 275.00m),
            new Row("D330", "Ratcheting Wrench Set SAE 10pc", "CQT Stock", 6, 4, 1, 1, 195.00m),
            new Row("D335", "Ratcheting Wrench Set Metric 10pc", "CQT Stock", 5, 3, 2, 0, 195.00m),
            new Row("E440", "Pliers Set 4pc", "CQT Stock", 8, 6, 2, 0, 89.99m),
            new Row("E445", "Diagonal Cutters 8\"", "CQT Stock", 12, 10, 1, 1, 45.50m),
            new Row("F550", "Screwdriver Set 10pc", "CQT Stock", 10, 8, 2, 0, 67.99m),
            new Row("F555", "Precision Screwdriver Set 6pc", "CQT Stock", 15, 12, 3, 0, 34.99m),
            new Row("G660", "Ball Peen Hammer 24oz", "CQT Stock", 4, 2, 1, 1, 42.50m),
            new Row("G665", "Dead Blow Hammer 3lb", "CQT Stock", 3, 1, 1, 1, 58.99m),
            new Row("H770", "Mechanic's Gloves XL", "CQT Stock", 20, 15, 5, 0, 24.99m),
            new Row("H775", "Mechanic's Gloves L", "CQT Stock", 18, 14, 4, 0, 24.99m),
            new Row("J880", "Shop Towels (Box of 200)", "CQT Stock", 25, 20, 5, 0, 15.99m),
            new Row("J885", "Parts Cleaner Spray 16oz", "CQT Stock", 30, 25, 5, 0, 12.50m),
            new Row("K990", "Creeper Low Profile", "CQT Stock", 2, 0, 0, 2, 125.00m),
            new Row("K995", "Mechanic's Stool Adjustable", "CQT Stock", 3, 1, 1, 1, 89.99m),
            new Row("M100", "Oil Filter Wrench Set 3pc", "CQT Stock", 6, 4, 2, 0, 45.99m),
            new Row("M105", "Brake Caliper Tool Kit", "CQT Stock", 4, 2, 1, 1, 156.00m),
            new Row("N200", "Battery Terminal Cleaner", "CQT Stock", 8, 6, 2, 0, 18.50m),
            new Row("N205", "Circuit Tester 12V-24V", "CQT Stock", 10, 8, 2, 0, 28.99m),
            new Row("P300", "Magnetic Parts Tray 6\"", "CQT Stock", 12, 10, 2, 0, 14.99m),
            new Row("P305", "Magnetic Pick-Up Tool Telescoping", "CQT Stock", 15, 12, 3, 0, 19.99m)
        });

        // Local Only items (LPN- prefix)
        items.AddRange(new[]
        {
            new Row("LPN-RW7M", "Ratcheting Wrench 7mm", "Local Only", 2, 0, 1, 1, 28.50m),
            new Row("LPN-1002", "Specialty Socket 32mm Deep", "Local Only", 1, 0, 0, 1, 45.00m),
            new Row("LPN-BC45", "Brake Caliper Piston Tool", "Local Only", 3, 1, 1, 1, 67.50m),
            new Row("LPN-TW250", "Torque Wrench 3/8\" Drive 25-250 in-lb", "Local Only", 2, 0, 1, 1, 189.99m),
            new Row("LPN-HS12", "Hose Clamp Pliers Set", "Local Only", 4, 2, 1, 1, 54.99m),
            new Row("LPN-PB20", "Pry Bar Set 3pc", "Local Only", 5, 3, 2, 0, 39.99m),
            new Row("LPN-CH80", "Chisel Set 8pc", "Local Only", 3, 1, 1, 1, 48.50m),
            new Row("LPN-FB15", "File Set 5pc with Handle", "Local Only", 6, 4, 2, 0, 32.99m),
            new Row("LPN-TH25", "Thread Repair Kit M10 x 1.5", "Local Only", 2, 0, 1, 1, 78.00m),
            new Row("LPN-OT40", "Oil Filter Cap Wrench 74mm", "Local Only", 4, 2, 2, 0, 24.99m),
            new Row("LPN-ST60", "Spark Plug Socket Set 3pc", "Local Only", 5, 3, 1, 1, 42.50m),
            new Row("LPN-FL90", "Flashlight LED Rechargeable", "Local Only", 8, 6, 2, 0, 45.99m),
            new Row("LPN-MT55", "Multimeter Digital Auto-Ranging", "Local Only", 3, 1, 1, 1, 89.99m),
            new Row("LPN-WR35", "Wire Stripper/Crimper Tool", "Local Only", 6, 4, 2, 0, 36.50m),
            new Row("LPN-VG22", "Vacuum Pump & Gauge Kit", "Local Only", 1, 0, 0, 1, 156.00m),
            new Row("LPN-AR18", "A/C Service Hose Set", "Local Only", 2, 1, 0, 1, 124.99m)
        });

        return items;
    }

    public static List<OpenPOLine> GetPOLines()
    {
        return new List<OpenPOLine>
        {
            // CAT3160 PO Lines
            new OpenPOLine
            {
                Item = "CAT3160",
                PONumber = "PO-0000100807", Line = 1, SONumber = "0002916666",
                Vendor = "CQT", Date = new DateTime(2025, 09, 30),
                QtyOrdered = 2, Open = 2, InTransit = 0, Alloc = 0, BKO = 1,
                UnitCost = 299.00m, Rcv = 0, Status = "Open"
            },
            
            // A20 PO Lines
            new OpenPOLine
            {
                Item = "A20",
                PONumber = "PO-0000100821", Line = 3, SONumber = "0002917777",
                Vendor = "CQT", Date = new DateTime(2025, 09, 28),
                QtyOrdered = 5, Open = 2, InTransit = 2, Alloc = 0, BKO = 0,
                UnitCost = 45.00m, Rcv = 3, Status = "Partial"
            },
            
            // A02 PO Lines
            new OpenPOLine
            {
                Item = "A02",
                PONumber = "PO-0000100821", Line = 4, SONumber = "0002917777",
                Vendor = "CQT", Date = new DateTime(2025, 09, 28),
                QtyOrdered = 1, Open = 1, InTransit = 0, Alloc = 0, BKO = 1,
                UnitCost = 23.50m, Rcv = 0, Status = "Open"
            },
            
            // B450 PO Lines
            new OpenPOLine
            {
                Item = "B450",
                PONumber = "PO-0000100855", Line = 1, SONumber = "0002918888",
                Vendor = "CQT", Date = new DateTime(2025, 10, 05),
                QtyOrdered = 10, Open = 5, InTransit = 3, Alloc = 2, BKO = 0,
                UnitCost = 189.99m, Rcv = 5, Status = "Partial"
            },
            
            // C120 PO Lines (Backordered)
            new OpenPOLine
            {
                Item = "C120",
                PONumber = "PO-0000100802", Line = 2, SONumber = "0002915555",
                Vendor = "CQT", Date = new DateTime(2025, 08, 15),
                QtyOrdered = 2, Open = 2, InTransit = 0, Alloc = 0, BKO = 2,
                UnitCost = 425.00m, Rcv = 0, Status = "Open"
            },
            
            // D330 PO Lines
            new OpenPOLine
            {
                Item = "D330",
                PONumber = "PO-0000100870", Line = 1, SONumber = "0002919999",
                Vendor = "CQT", Date = new DateTime(2025, 10, 12),
                QtyOrdered = 8, Open = 6, InTransit = 4, Alloc = 1, BKO = 1,
                UnitCost = 195.00m, Rcv = 2, Status = "Partial"
            },
            
            // AF12201 PO Lines (Large order)
            new OpenPOLine
            {
                Item = "AF12201",
                PONumber = "PO-0000100890", Line = 1, SONumber = "0002920000",
                Vendor = "CQT", Date = new DateTime(2025, 10, 20),
                QtyOrdered = 50, Open = 18, InTransit = 18, Alloc = 0, BKO = 0,
                UnitCost = 10.00m, Rcv = 32, Status = "Partial"
            },
            
            // H770 PO Lines (Gloves)
            new OpenPOLine
            {
                Item = "H770",
                PONumber = "PO-0000100895", Line = 2, SONumber = "0002920111",
                Vendor = "CQT", Date = new DateTime(2025, 10, 22),
                QtyOrdered = 30, Open = 20, InTransit = 15, Alloc = 5, BKO = 0,
                UnitCost = 24.99m, Rcv = 10, Status = "Partial"
            },
            
            // LPN-BC45 Local PO Line
            new OpenPOLine
            {
                Item = "LPN-BC45",
                PONumber = "PO-0000100810", Line = 5, SONumber = "0002917000",
                Vendor = "Local Supplier", Date = new DateTime(2025, 09, 20),
                QtyOrdered = 5, Open = 3, InTransit = 1, Alloc = 1, BKO = 1,
                UnitCost = 67.50m, Rcv = 2, Status = "Partial"
            },
            
            // LPN-TW250 Local PO Line (Backordered)
            new OpenPOLine
            {
                Item = "LPN-TW250",
                PONumber = "PO-0000100815", Line = 1, SONumber = "0002917111",
                Vendor = "Local Supplier", Date = new DateTime(2025, 09, 10),
                QtyOrdered = 2, Open = 2, InTransit = 0, Alloc = 1, BKO = 1,
                UnitCost = 189.99m, Rcv = 0, Status = "Open"
            },
            
            // M105 PO Line
            new OpenPOLine
            {
                Item = "M105",
                PONumber = "PO-0000100880", Line = 3, SONumber = "0002919500",
                Vendor = "CQT", Date = new DateTime(2025, 10, 18),
                QtyOrdered = 6, Open = 4, InTransit = 2, Alloc = 1, BKO = 1,
                UnitCost = 156.00m, Rcv = 2, Status = "Partial"
            },
            
            // K990 PO Line (Backordered)
            new OpenPOLine
            {
                Item = "K990",
                PONumber = "PO-0000100805", Line = 1, SONumber = "0002916000",
                Vendor = "CQT", Date = new DateTime(2025, 09, 05),
                QtyOrdered = 2, Open = 2, InTransit = 0, Alloc = 0, BKO = 2,
                UnitCost = 125.00m, Rcv = 0, Status = "Open"
            },
            
            // LPN-MT55 Local PO Line
            new OpenPOLine
            {
                Item = "LPN-MT55",
                PONumber = "PO-0000100825", Line = 2, SONumber = "0002918000",
                Vendor = "Local Supplier", Date = new DateTime(2025, 10, 01),
                QtyOrdered = 4, Open = 3, InTransit = 1, Alloc = 1, BKO = 1,
                UnitCost = 89.99m, Rcv = 1, Status = "Partial"
            },
            
            // P305 PO Line
            new OpenPOLine
            {
                Item = "P305",
                PONumber = "PO-0000100900", Line = 1, SONumber = "0002920500",
                Vendor = "CQT", Date = new DateTime(2025, 10, 25),
                QtyOrdered = 20, Open = 15, InTransit = 12, Alloc = 3, BKO = 0,
                UnitCost = 19.99m, Rcv = 5, Status = "Partial"
            },
            
            // LPN-VG22 Local PO Line (Backordered)
            new OpenPOLine
            {
                Item = "LPN-VG22",
                PONumber = "PO-0000100812", Line = 1, SONumber = "0002917200",
                Vendor = "Local Supplier", Date = new DateTime(2025, 09, 15),
                QtyOrdered = 1, Open = 1, InTransit = 0, Alloc = 0, BKO = 1,
                UnitCost = 156.00m, Rcv = 0, Status = "Open"
            },
            
            // B451 PO Line
            new OpenPOLine
            {
                Item = "B451",
                PONumber = "PO-0000100860", Line = 2, SONumber = "0002919100",
                Vendor = "CQT", Date = new DateTime(2025, 10, 08),
                QtyOrdered = 4, Open = 3, InTransit = 1, Alloc = 1, BKO = 1,
                UnitCost = 145.50m, Rcv = 1, Status = "Partial"
            },
            
            // LPN-CH80 Local PO Line
            new OpenPOLine
            {
                Item = "LPN-CH80",
                PONumber = "PO-0000100830", Line = 1, SONumber = "0002918200",
                Vendor = "Local Supplier", Date = new DateTime(2025, 10, 03),
                QtyOrdered = 5, Open = 3, InTransit = 1, Alloc = 1, BKO = 1,
                UnitCost = 48.50m, Rcv = 2, Status = "Partial"
            },
            
            // G665 PO Line
            new OpenPOLine
            {
                Item = "G665",
                PONumber = "PO-0000100875", Line = 1, SONumber = "0002919300",
                Vendor = "CQT", Date = new DateTime(2025, 10, 15),
                QtyOrdered = 4, Open = 3, InTransit = 1, Alloc = 1, BKO = 1,
                UnitCost = 58.99m, Rcv = 1, Status = "Partial"
            },
            
            // LPN-AR18 Local PO Line (Backordered)
            new OpenPOLine
            {
                Item = "LPN-AR18",
                PONumber = "PO-0000100818", Line = 1, SONumber = "0002917500",
                Vendor = "Local Supplier", Date = new DateTime(2025, 09, 12),
                QtyOrdered = 3, Open = 2, InTransit = 1, Alloc = 0, BKO = 1,
                UnitCost = 124.99m, Rcv = 1, Status = "Partial"
            }
        };
    }

    public sealed class Row
    {
        public Row(string item, string desc, string cls, int onOrderOpen, int inTransit, int alloc, int bko, decimal unitCost)
        {
            Item = item;
            Description = desc;
            Class = cls;
            OnOrderOpen = onOrderOpen;
            InTransit = inTransit;
            Alloc = alloc;
            BKO = bko;
            UnitCost = unitCost;
        }

        public string Item { get; set; }
        public string Description { get; set; }
        public string Class { get; set; }
        public int OnOrderOpen { get; set; }
        public int InTransit { get; set; }
        public int Alloc { get; set; }
        public int BKO { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalOpenCost => UnitCost * Math.Max(OnOrderOpen, 0);
        public decimal InTransitCost => UnitCost * InTransit;
        public decimal BackorderCost => UnitCost * BKO;
        public bool IsUnacknowledged => OnOrderOpen > 0 && InTransit == 0 && BKO == 0 && Alloc == 0;
        public decimal UnacknowledgedCost => IsUnacknowledged ? TotalOpenCost : 0m;
    }

    public sealed class OpenPOLine
    {
        public string Item { get; set; } = "";
        public string PONumber { get; set; } = "";
        public int Line { get; set; }
        public string SONumber { get; set; } = "";
        public string Vendor { get; set; } = "";
        public DateTime Date { get; set; }
        public int QtyOrdered { get; set; }
        public int Open { get; set; }
        public int InTransit { get; set; }
        public int Alloc { get; set; }
        public int BKO { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalOpenCost => UnitCost * Math.Max(Open, 0);
        public int Rcv { get; set; }
        public string Status { get; set; } = "Open";
    }
}
