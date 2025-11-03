namespace Prototype.Pages.Product.SeedData;

public static class SetMaintenanceSeedData
{
    public static List<SetMaintenanceItem> GetItems()
    {
        var random = new Random(42); // Fixed seed for consistent data

        var items = new List<SetMaintenanceItem>
        {
            // Socket Sets
            new SetMaintenanceItem
            {
                Item = "S38SET-42",
                Description = "3/8\" Socket Set 42pc Metric",
                ListPrice = 299.99m,
                Discount = "C",
                Class = "CQT Stock",
                OnHand = 24,
                Pending = 4,
                OnOrder = 10,
                Alloc = 6,
                BKO = 0,
                InTransit = 5,
                Needs = 0,
                Interests = 9,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "S38-8MM", Description = "3/8\" Socket 8mm", AdjQty = 1, OnHand = 156, PrintLabel = true },
                    new SetComponent { Component = "S38-9MM", Description = "3/8\" Socket 9mm", AdjQty = 1, OnHand = 142, PrintLabel = true },
                    new SetComponent { Component = "S38-10MM", Description = "3/8\" Socket 10mm", AdjQty = 1, OnHand = 189, PrintLabel = true },
                    new SetComponent { Component = "S38-11MM", Description = "3/8\" Socket 11mm", AdjQty = 1, OnHand = 134, PrintLabel = true },
                    new SetComponent { Component = "S38-12MM", Description = "3/8\" Socket 12mm", AdjQty = 1, OnHand = 167, PrintLabel = true },
                    new SetComponent { Component = "S38-13MM", Description = "3/8\" Socket 13mm", AdjQty = 1, OnHand = 145, PrintLabel = true },
                    new SetComponent { Component = "S38-14MM", Description = "3/8\" Socket 14mm", AdjQty = 1, OnHand = 123, PrintLabel = true },
                    new SetComponent { Component = "S38-15MM", Description = "3/8\" Socket 15mm", AdjQty = 1, OnHand = 156, PrintLabel = true },
                    new SetComponent { Component = "S38-16MM", Description = "3/8\" Socket 16mm", AdjQty = 1, OnHand = 134, PrintLabel = true },
                    new SetComponent { Component = "S38-17MM", Description = "3/8\" Socket 17mm", AdjQty = 1, OnHand = 145, PrintLabel = true },
                    new SetComponent { Component = "S38-18MM", Description = "3/8\" Socket 18mm", AdjQty = 1, OnHand = 167, PrintLabel = true },
                    new SetComponent { Component = "S38-19MM", Description = "3/8\" Socket 19mm", AdjQty = 1, OnHand = 142, PrintLabel = true },
                    new SetComponent { Component = "S38-RATCH", Description = "3/8\" Ratchet Handle", AdjQty = 1, OnHand = 78, PrintLabel = false },
                    new SetComponent { Component = "S38-EXT3", Description = "3/8\" Extension 3\"", AdjQty = 1, OnHand = 89, PrintLabel = false },
                    new SetComponent { Component = "S38-EXT6", Description = "3/8\" Extension 6\"", AdjQty = 1, OnHand = 92, PrintLabel = false },
                    new SetComponent { Component = "CASE-S38", Description = "Blow Mold Case", AdjQty = 1, OnHand = 45, PrintLabel = false }
                }
            },
            new SetMaintenanceItem
            {
                Item = "S12SET-SAE",
                Description = "1/2\" Socket Set 28pc SAE",
                ListPrice = 349.99m,
                Discount = "C",
                Class = "CQT Stock",
                OnHand = 18,
                Pending = 3,
                OnOrder = 8,
                Alloc = 4,
                BKO = 0,
                InTransit = 4,
                Needs = 0,
                Interests = 7,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "S12-3/8", Description = "1/2\" Socket 3/8\"", AdjQty = 1, OnHand = 98, PrintLabel = true },
                    new SetComponent { Component = "S12-7/16", Description = "1/2\" Socket 7/16\"", AdjQty = 1, OnHand = 87, PrintLabel = true },
                    new SetComponent { Component = "S12-1/2", Description = "1/2\" Socket 1/2\"", AdjQty = 1, OnHand = 102, PrintLabel = true },
                    new SetComponent { Component = "S12-9/16", Description = "1/2\" Socket 9/16\"", AdjQty = 1, OnHand = 95, PrintLabel = true },
                    new SetComponent { Component = "S12-5/8", Description = "1/2\" Socket 5/8\"", AdjQty = 1, OnHand = 89, PrintLabel = true },
                    new SetComponent { Component = "S12-11/16", Description = "1/2\" Socket 11/16\"", AdjQty = 1, OnHand = 76, PrintLabel = true },
                    new SetComponent { Component = "S12-3/4", Description = "1/2\" Socket 3/4\"", AdjQty = 1, OnHand = 92, PrintLabel = true },
                    new SetComponent { Component = "S12-13/16", Description = "1/2\" Socket 13/16\"", AdjQty = 1, OnHand = 84, PrintLabel = true },
                    new SetComponent { Component = "S12-7/8", Description = "1/2\" Socket 7/8\"", AdjQty = 1, OnHand = 78, PrintLabel = true },
                    new SetComponent { Component = "S12-15/16", Description = "1/2\" Socket 15/16\"", AdjQty = 1, OnHand = 71, PrintLabel = true },
                    new SetComponent { Component = "S12-1", Description = "1/2\" Socket 1\"", AdjQty = 1, OnHand = 82, PrintLabel = true },
                    new SetComponent { Component = "S12-RATCH", Description = "1/2\" Ratchet Handle", AdjQty = 1, OnHand = 56, PrintLabel = false },
                    new SetComponent { Component = "S12-EXT5", Description = "1/2\" Extension 5\"", AdjQty = 1, OnHand = 67, PrintLabel = false },
                    new SetComponent { Component = "S12-EXT10", Description = "1/2\" Extension 10\"", AdjQty = 1, OnHand = 63, PrintLabel = false },
                    new SetComponent { Component = "CASE-S12", Description = "Metal Case", AdjQty = 1, OnHand = 34, PrintLabel = false }
                }
            },

            // Wrench Sets
            new SetMaintenanceItem
            {
                Item = "CW-SET10M",
                Description = "Combination Wrench Set 10pc Metric",
                ListPrice = 189.99m,
                Discount = "B",
                Class = "CQT Stock",
                OnHand = 32,
                Pending = 5,
                OnOrder = 15,
                Alloc = 8,
                BKO = 0,
                InTransit = 7,
                Needs = 0,
                Interests = 12,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "CW-8MM", Description = "Combination Wrench 8mm", AdjQty = 1, OnHand = 234, PrintLabel = true },
                    new SetComponent { Component = "CW-10MM", Description = "Combination Wrench 10mm", AdjQty = 1, OnHand = 267, PrintLabel = true },
                    new SetComponent { Component = "CW-11MM", Description = "Combination Wrench 11mm", AdjQty = 1, OnHand = 198, PrintLabel = true },
                    new SetComponent { Component = "CW-12MM", Description = "Combination Wrench 12mm", AdjQty = 1, OnHand = 245, PrintLabel = true },
                    new SetComponent { Component = "CW-13MM", Description = "Combination Wrench 13mm", AdjQty = 1, OnHand = 223, PrintLabel = true },
                    new SetComponent { Component = "CW-14MM", Description = "Combination Wrench 14mm", AdjQty = 1, OnHand = 189, PrintLabel = true },
                    new SetComponent { Component = "CW-15MM", Description = "Combination Wrench 15mm", AdjQty = 1, OnHand = 212, PrintLabel = true },
                    new SetComponent { Component = "CW-17MM", Description = "Combination Wrench 17mm", AdjQty = 1, OnHand = 187, PrintLabel = true },
                    new SetComponent { Component = "CW-19MM", Description = "Combination Wrench 19mm", AdjQty = 1, OnHand = 156, PrintLabel = true },
                    new SetComponent { Component = "CW-21MM", Description = "Combination Wrench 21mm", AdjQty = 1, OnHand = 145, PrintLabel = true },
                    new SetComponent { Component = "ROLL-10", Description = "Wrench Roll 10pc", AdjQty = 1, OnHand = 67, PrintLabel = false }
                }
            },
            new SetMaintenanceItem
            {
                Item = "LPN-RW7M",
                Description = "Ratcheting Wrench Set 7pc Metric",
                ListPrice = 249.99m,
                Discount = "B",
                Class = "Local Only",
                OnHand = 15,
                Pending = 3,
                OnOrder = 8,
                Alloc = 4,
                BKO = 0,
                InTransit = 4,
                Needs = 0,
                Interests = 8,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "RW-10MM", Description = "Ratcheting Wrench 10mm", AdjQty = 1, OnHand = 134, PrintLabel = true },
                    new SetComponent { Component = "RW-12MM", Description = "Ratcheting Wrench 12mm", AdjQty = 1, OnHand = 145, PrintLabel = true },
                    new SetComponent { Component = "RW-13MM", Description = "Ratcheting Wrench 13mm", AdjQty = 1, OnHand = 128, PrintLabel = true },
                    new SetComponent { Component = "RW-14MM", Description = "Ratcheting Wrench 14mm", AdjQty = 1, OnHand = 112, PrintLabel = true },
                    new SetComponent { Component = "RW-15MM", Description = "Ratcheting Wrench 15mm", AdjQty = 1, OnHand = 123, PrintLabel = true },
                    new SetComponent { Component = "RW-17MM", Description = "Ratcheting Wrench 17mm", AdjQty = 1, OnHand = 98, PrintLabel = true },
                    new SetComponent { Component = "RW-19MM", Description = "Ratcheting Wrench 19mm", AdjQty = 1, OnHand = 87, PrintLabel = true },
                    new SetComponent { Component = "ROLL-7", Description = "Wrench Roll 7pc", AdjQty = 1, OnHand = 45, PrintLabel = false }
                }
            },

            // Screwdriver Sets
            new SetMaintenanceItem
            {
                Item = "SD-SET20",
                Description = "Screwdriver Set 20pc Professional",
                ListPrice = 124.99m,
                Discount = "B",
                Class = "CQT Stock",
                OnHand = 28,
                Pending = 5,
                OnOrder = 15,
                Alloc = 6,
                BKO = 0,
                InTransit = 8,
                Needs = 0,
                Interests = 11,
                IsPriceConfirm = true,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "SD-PH1", Description = "Phillips #1 Screwdriver", AdjQty = 2, OnHand = 178, PrintLabel = true },
                    new SetComponent { Component = "SD-PH2", Description = "Phillips #2 Screwdriver", AdjQty = 2, OnHand = 245, PrintLabel = true },
                    new SetComponent { Component = "SD-PH3", Description = "Phillips #3 Screwdriver", AdjQty = 2, OnHand = 167, PrintLabel = true },
                    new SetComponent { Component = "SD-FL4", Description = "Flathead 1/4\" Screwdriver", AdjQty = 2, OnHand = 189, PrintLabel = true },
                    new SetComponent { Component = "SD-FL6", Description = "Flathead 3/16\" Screwdriver", AdjQty = 2, OnHand = 156, PrintLabel = true },
                    new SetComponent { Component = "SD-T10", Description = "Torx T10 Screwdriver", AdjQty = 2, OnHand = 134, PrintLabel = true },
                    new SetComponent { Component = "SD-T15", Description = "Torx T15 Screwdriver", AdjQty = 2, OnHand = 145, PrintLabel = true },
                    new SetComponent { Component = "SD-T20", Description = "Torx T20 Screwdriver", AdjQty = 2, OnHand = 128, PrintLabel = true },
                    new SetComponent { Component = "SD-T25", Description = "Torx T25 Screwdriver", AdjQty = 2, OnHand = 112, PrintLabel = true },
                    new SetComponent { Component = "SD-T30", Description = "Torx T30 Screwdriver", AdjQty = 2, OnHand = 98, PrintLabel = true },
                    new SetComponent { Component = "RACK-SD20", Description = "Screwdriver Rack", AdjQty = 1, OnHand = 56, PrintLabel = false }
                }
            },

            // Pliers Sets
            new SetMaintenanceItem
            {
                Item = "PL-SET5",
                Description = "Pliers Set 5pc Professional",
                ListPrice = 159.99m,
                Discount = "A",
                Class = "CQT Stock",
                OnHand = 22,
                Pending = 4,
                OnOrder = 10,
                Alloc = 5,
                BKO = 0,
                InTransit = 5,
                Needs = 0,
                Interests = 9,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "PL-NEEDLE6", Description = "Needle Nose Pliers 6\"", AdjQty = 1, OnHand = 145, PrintLabel = true },
                    new SetComponent { Component = "PL-NEEDLE8", Description = "Needle Nose Pliers 8\"", AdjQty = 1, OnHand = 128, PrintLabel = true },
                    new SetComponent { Component = "PL-SLIP8", Description = "Slip Joint Pliers 8\"", AdjQty = 1, OnHand = 156, PrintLabel = true },
                    new SetComponent { Component = "PL-DIAG6", Description = "Diagonal Cutters 6\"", AdjQty = 1, OnHand = 134, PrintLabel = true },
                    new SetComponent { Component = "PL-LINEMAN9", Description = "Lineman Pliers 9\"", AdjQty = 1, OnHand = 112, PrintLabel = true },
                    new SetComponent { Component = "ROLL-PL5", Description = "Pliers Roll 5pc", AdjQty = 1, OnHand = 43, PrintLabel = false }
                }
            },

            // Hex Key Sets
            new SetMaintenanceItem
            {
                Item = "LPN-HK22M",
                Description = "Hex Key Set 22pc Metric Ball End",
                ListPrice = 89.99m,
                Discount = "A",
                Class = "Local Only",
                OnHand = 45,
                Pending = 8,
                OnOrder = 20,
                Alloc = 12,
                BKO = 0,
                InTransit = 10,
                Needs = 0,
                Interests = 18,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "HK-1.5MM", Description = "Hex Key 1.5mm", AdjQty = 1, OnHand = 267, PrintLabel = true },
                    new SetComponent { Component = "HK-2MM", Description = "Hex Key 2mm", AdjQty = 1, OnHand = 289, PrintLabel = true },
                    new SetComponent { Component = "HK-2.5MM", Description = "Hex Key 2.5mm", AdjQty = 1, OnHand = 245, PrintLabel = true },
                    new SetComponent { Component = "HK-3MM", Description = "Hex Key 3mm", AdjQty = 1, OnHand = 298, PrintLabel = true },
                    new SetComponent { Component = "HK-4MM", Description = "Hex Key 4mm", AdjQty = 1, OnHand = 312, PrintLabel = true },
                    new SetComponent { Component = "HK-5MM", Description = "Hex Key 5mm", AdjQty = 1, OnHand = 278, PrintLabel = true },
                    new SetComponent { Component = "HK-6MM", Description = "Hex Key 6mm", AdjQty = 1, OnHand = 245, PrintLabel = true },
                    new SetComponent { Component = "HK-8MM", Description = "Hex Key 8mm", AdjQty = 1, OnHand = 198, PrintLabel = true },
                    new SetComponent { Component = "HK-10MM", Description = "Hex Key 10mm", AdjQty = 1, OnHand = 167, PrintLabel = true },
                    new SetComponent { Component = "HOLD-HK22", Description = "Hex Key Holder", AdjQty = 1, OnHand = 89, PrintLabel = false }
                }
            },

            // Impact Socket Sets
            new SetMaintenanceItem
            {
                Item = "IMP-SET19",
                Description = "1/2\" Impact Socket Set 19pc Deep",
                ListPrice = 399.99m,
                Discount = "C",
                Class = "CQT Stock",
                OnHand = 12,
                Pending = 2,
                OnOrder = 6,
                Alloc = 3,
                BKO = 0,
                InTransit = 3,
                Needs = 0,
                Interests = 6,
                IsDropShip = true,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "IMP-10MM", Description = "Impact Socket 10mm Deep", AdjQty = 1, OnHand = 98, PrintLabel = true },
                    new SetComponent { Component = "IMP-11MM", Description = "Impact Socket 11mm Deep", AdjQty = 1, OnHand = 87, PrintLabel = true },
                    new SetComponent { Component = "IMP-12MM", Description = "Impact Socket 12mm Deep", AdjQty = 1, OnHand = 102, PrintLabel = true },
                    new SetComponent { Component = "IMP-13MM", Description = "Impact Socket 13mm Deep", AdjQty = 1, OnHand = 95, PrintLabel = true },
                    new SetComponent { Component = "IMP-14MM", Description = "Impact Socket 14mm Deep", AdjQty = 1, OnHand = 89, PrintLabel = true },
                    new SetComponent { Component = "IMP-15MM", Description = "Impact Socket 15mm Deep", AdjQty = 1, OnHand = 76, PrintLabel = true },
                    new SetComponent { Component = "IMP-16MM", Description = "Impact Socket 16mm Deep", AdjQty = 1, OnHand = 92, PrintLabel = true },
                    new SetComponent { Component = "IMP-17MM", Description = "Impact Socket 17mm Deep", AdjQty = 1, OnHand = 84, PrintLabel = true },
                    new SetComponent { Component = "IMP-18MM", Description = "Impact Socket 18mm Deep", AdjQty = 1, OnHand = 78, PrintLabel = true },
                    new SetComponent { Component = "IMP-19MM", Description = "Impact Socket 19mm Deep", AdjQty = 1, OnHand = 145, PrintLabel = true },
                    new SetComponent { Component = "IMP-21MM", Description = "Impact Socket 21mm Deep", AdjQty = 1, OnHand = 134, PrintLabel = true },
                    new SetComponent { Component = "IMP-22MM", Description = "Impact Socket 22mm Deep", AdjQty = 1, OnHand = 112, PrintLabel = true },
                    new SetComponent { Component = "CASE-IMP19", Description = "Impact Socket Case", AdjQty = 1, OnHand = 28, PrintLabel = false }
                }
            },

            // Combo Sets
            new SetMaintenanceItem
            {
                Item = "STARTER-KIT",
                Description = "Technician Starter Tool Kit 150pc",
                ListPrice = 1299.99m,
                Discount = "TECH",
                Class = "CQT Stock",
                OnHand = 8,
                Pending = 2,
                OnOrder = 4,
                Alloc = 2,
                BKO = 0,
                InTransit = 2,
                Needs = 0,
                Interests = 5,
                IsPriceConfirm = true,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "S38SET-42", Description = "3/8\" Socket Set 42pc", AdjQty = 1, OnHand = 24, PrintLabel = false },
                    new SetComponent { Component = "CW-SET10M", Description = "Wrench Set 10pc", AdjQty = 1, OnHand = 32, PrintLabel = false },
                    new SetComponent { Component = "SD-SET20", Description = "Screwdriver Set 20pc", AdjQty = 1, OnHand = 28, PrintLabel = false },
                    new SetComponent { Component = "PL-SET5", Description = "Pliers Set 5pc", AdjQty = 1, OnHand = 22, PrintLabel = false },
                    new SetComponent { Component = "LPN-HK22M", Description = "Hex Key Set 22pc", AdjQty = 1, OnHand = 45, PrintLabel = false },
                    new SetComponent { Component = "CHEST-26", Description = "26\" Tool Chest", AdjQty = 1, OnHand = 12, PrintLabel = false }
                }
            },

            // Discontinued Set
            new SetMaintenanceItem
            {
                Item = "LPN-OLD15",
                Description = "Legacy Socket Set 15pc (Discontinued)",
                ListPrice = 149.99m,
                Discount = "A",
                Class = "Local Only",
                OnHand = 3,
                Pending = 0,
                OnOrder = 0,
                Alloc = 0,
                BKO = 0,
                InTransit = 0,
                Needs = 0,
                Interests = 0,
                IsDiscontinued = true,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "OLD-S10", Description = "Old Style Socket 10mm", AdjQty = 1, OnHand = 23, PrintLabel = true },
                    new SetComponent { Component = "OLD-S11", Description = "Old Style Socket 11mm", AdjQty = 1, OnHand = 18, PrintLabel = true },
                    new SetComponent { Component = "OLD-S12", Description = "Old Style Socket 12mm", AdjQty = 1, OnHand = 15, PrintLabel = true },
                    new SetComponent { Component = "OLD-S13", Description = "Old Style Socket 13mm", AdjQty = 1, OnHand = 12, PrintLabel = true },
                    new SetComponent { Component = "OLD-RATCH", Description = "Old Style Ratchet", AdjQty = 1, OnHand = 8, PrintLabel = false }
                }
            },

            // Torque Wrench Set
            new SetMaintenanceItem
            {
                Item = "TQ-SET3",
                Description = "Torque Wrench Set 3pc Professional",
                ListPrice = 899.99m,
                Discount = "TECH",
                Class = "CQT Stock",
                OnHand = 6,
                Pending = 1,
                OnOrder = 3,
                Alloc = 1,
                BKO = 0,
                InTransit = 2,
                Needs = 0,
                Interests = 4,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "TQ-14-1", Description = "1/4\" Torque Wrench 20-150in-lb", AdjQty = 1, OnHand = 34, PrintLabel = true },
                    new SetComponent { Component = "TQ-38-1", Description = "3/8\" Torque Wrench 10-80ft-lb", AdjQty = 1, OnHand = 28, PrintLabel = true },
                    new SetComponent { Component = "TQ-12-1", Description = "1/2\" Torque Wrench 25-250ft-lb", AdjQty = 1, OnHand = 23, PrintLabel = true },
                    new SetComponent { Component = "CASE-TQ3", Description = "Torque Wrench Case", AdjQty = 1, OnHand = 15, PrintLabel = false }
                }
            },

            // Specialty Sets
            new SetMaintenanceItem
            {
                Item = "LPN-BRK24",
                Description = "Brake Service Tool Kit 24pc",
                ListPrice = 449.99m,
                Discount = "B",
                Class = "Local Only",
                OnHand = 14,
                Pending = 3,
                OnOrder = 7,
                Alloc = 3,
                BKO = 0,
                InTransit = 4,
                Needs = 0,
                Interests = 7,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "BRK-PISTON", Description = "Brake Piston Tool", AdjQty = 1, OnHand = 45, PrintLabel = true },
                    new SetComponent { Component = "BRK-SPRING", Description = "Spring Compressor", AdjQty = 1, OnHand = 38, PrintLabel = true },
                    new SetComponent { Component = "BRK-CALIPER", Description = "Caliper Hanger Set", AdjQty = 2, OnHand = 67, PrintLabel = true },
                    new SetComponent { Component = "BRK-BLEED", Description = "Brake Bleeder Kit", AdjQty = 1, OnHand = 32, PrintLabel = true },
                    new SetComponent { Component = "BRK-GAUGE", Description = "Brake Fluid Tester", AdjQty = 1, OnHand = 28, PrintLabel = true },
                    new SetComponent { Component = "CASE-BRK", Description = "Brake Tool Case", AdjQty = 1, OnHand = 22, PrintLabel = false }
                }
            },

            new SetMaintenanceItem
            {
                Item = "ELECTRIC-KIT",
                Description = "Electrical Diagnostic Kit 18pc",
                ListPrice = 549.99m,
                Discount = "TECH",
                Class = "CQT Stock",
                OnHand = 11,
                Pending = 2,
                OnOrder = 5,
                Alloc = 2,
                BKO = 0,
                InTransit = 3,
                Needs = 0,
                Interests = 6,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "MM-DIGITAL", Description = "Digital Multimeter", AdjQty = 1, OnHand = 56, PrintLabel = true },
                    new SetComponent { Component = "TEST-LIGHT", Description = "Test Light Pro", AdjQty = 1, OnHand = 78, PrintLabel = true },
                    new SetComponent { Component = "WIRE-STRIP", Description = "Wire Stripper Tool", AdjQty = 1, OnHand = 92, PrintLabel = true },
                    new SetComponent { Component = "CRIMP-SET", Description = "Terminal Crimp Set", AdjQty = 1, OnHand = 45, PrintLabel = true },
                    new SetComponent { Component = "FUSE-TEST", Description = "Fuse Tester", AdjQty = 1, OnHand = 67, PrintLabel = true },
                    new SetComponent { Component = "PROBE-SET", Description = "Test Probe Set", AdjQty = 1, OnHand = 89, PrintLabel = true },
                    new SetComponent { Component = "CASE-ELEC", Description = "Electrical Tool Case", AdjQty = 1, OnHand = 18, PrintLabel = false }
                }
            },

            // Additional realistic sets
            new SetMaintenanceItem
            {
                Item = "PUNCH-SET12",
                Description = "Punch and Chisel Set 12pc",
                ListPrice = 79.99m,
                Discount = "A",
                Class = "CQT Stock",
                OnHand = 38,
                Pending = 6,
                OnOrder = 18,
                Alloc = 9,
                BKO = 0,
                InTransit = 9,
                Needs = 0,
                Interests = 14,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "PUNCH-1/16", Description = "Pin Punch 1/16\"", AdjQty = 1, OnHand = 156, PrintLabel = true },
                    new SetComponent { Component = "PUNCH-3/32", Description = "Pin Punch 3/32\"", AdjQty = 1, OnHand = 145, PrintLabel = true },
                    new SetComponent { Component = "PUNCH-1/8", Description = "Pin Punch 1/8\"", AdjQty = 1, OnHand = 167, PrintLabel = true },
                    new SetComponent { Component = "PUNCH-5/32", Description = "Pin Punch 5/32\"", AdjQty = 1, OnHand = 134, PrintLabel = true },
                    new SetComponent { Component = "CHISEL-1/4", Description = "Cold Chisel 1/4\"", AdjQty = 1, OnHand = 123, PrintLabel = true },
                    new SetComponent { Component = "CHISEL-3/8", Description = "Cold Chisel 3/8\"", AdjQty = 1, OnHand = 112, PrintLabel = true },
                    new SetComponent { Component = "CHISEL-1/2", Description = "Cold Chisel 1/2\"", AdjQty = 1, OnHand = 98, PrintLabel = true },
                    new SetComponent { Component = "PUNCH-CTR", Description = "Center Punch", AdjQty = 1, OnHand = 178, PrintLabel = true },
                    new SetComponent { Component = "ROLL-P12", Description = "Punch Roll", AdjQty = 1, OnHand = 67, PrintLabel = false }
                }
            },

            new SetMaintenanceItem
            {
                Item = "LPN-TD40M",
                Description = "Tap and Die Set 40pc Metric",
                ListPrice = 299.99m,
                Discount = "B",
                Class = "Local Only",
                OnHand = 9,
                Pending = 2,
                OnOrder = 5,
                Alloc = 2,
                BKO = 0,
                InTransit = 3,
                Needs = 0,
                Interests = 5,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "TAP-M6", Description = "Tap M6x1.0", AdjQty = 1, OnHand = 87, PrintLabel = true },
                    new SetComponent { Component = "TAP-M8", Description = "Tap M8x1.25", AdjQty = 1, OnHand = 76, PrintLabel = true },
                    new SetComponent { Component = "TAP-M10", Description = "Tap M10x1.5", AdjQty = 1, OnHand = 92, PrintLabel = true },
                    new SetComponent { Component = "TAP-M12", Description = "Tap M12x1.75", AdjQty = 1, OnHand = 84, PrintLabel = true },
                    new SetComponent { Component = "DIE-M6", Description = "Die M6x1.0", AdjQty = 1, OnHand = 78, PrintLabel = true },
                    new SetComponent { Component = "DIE-M8", Description = "Die M8x1.25", AdjQty = 1, OnHand = 71, PrintLabel = true },
                    new SetComponent { Component = "DIE-M10", Description = "Die M10x1.5", AdjQty = 1, OnHand = 82, PrintLabel = true },
                    new SetComponent { Component = "DIE-M12", Description = "Die M12x1.75", AdjQty = 1, OnHand = 67, PrintLabel = true },
                    new SetComponent { Component = "HANDLE-TAP", Description = "Tap Handle Adjustable", AdjQty = 2, OnHand = 45, PrintLabel = false },
                    new SetComponent { Component = "HANDLE-DIE", Description = "Die Stock Handle", AdjQty = 2, OnHand = 38, PrintLabel = false },
                    new SetComponent { Component = "CASE-TD40", Description = "Metal Index Case", AdjQty = 1, OnHand = 15, PrintLabel = false }
                }
            },

            new SetMaintenanceItem
            {
                Item = "FILE-SET8",
                Description = "File Set 8pc Professional",
                ListPrice = 119.99m,
                Discount = "A",
                Class = "CQT Stock",
                OnHand = 27,
                Pending = 4,
                OnOrder = 12,
                Alloc = 6,
                BKO = 0,
                InTransit = 6,
                Needs = 0,
                Interests = 10,
                Components = new List<SetComponent>
                {
                    new SetComponent { Component = "FILE-FLAT8", Description = "Flat File 8\" Bastard", AdjQty = 1, OnHand = 134, PrintLabel = true },
                    new SetComponent { Component = "FILE-HALF8", Description = "Half Round File 8\"", AdjQty = 1, OnHand = 112, PrintLabel = true },
                    new SetComponent { Component = "FILE-ROUND8", Description = "Round File 8\"", AdjQty = 1, OnHand = 98, PrintLabel = true },
                    new SetComponent { Component = "FILE-TRI8", Description = "Triangle File 8\"", AdjQty = 1, OnHand = 87, PrintLabel = true },
                    new SetComponent { Component = "FILE-SQUARE8", Description = "Square File 8\"", AdjQty = 1, OnHand = 76, PrintLabel = true },
                    new SetComponent { Component = "FILE-NEEDLE", Description = "Needle File Set", AdjQty = 1, OnHand = 145, PrintLabel = true },
                    new SetComponent { Component = "HANDLE-FILE", Description = "File Handle", AdjQty = 3, OnHand = 234, PrintLabel = false },
                    new SetComponent { Component = "ROLL-F8", Description = "File Roll", AdjQty = 1, OnHand = 56, PrintLabel = false }
                }
            }
        };

        // Add more variety with generated sets (increase to 30 for more data)
        for (int i = 1; i <= 30; i++)
        {
            // Determine class - 70% CQT Stock, 30% Local Only
            var itemClass = random.Next(0, 10) < 7 ? "CQT Stock" : "Local Only";
            var itemPrefix = itemClass == "Local Only" ? "LPN-" : "GENSET-";

            items.Add(new SetMaintenanceItem
            {
                Item = $"{itemPrefix}{i:D3}",
                Description = $"Universal Tool Set {i * 5}pc",
                ListPrice = 99.99m + (i * 25m),
                Discount = new[] { "A", "B", "C", "TECH" }[random.Next(4)],
                Class = itemClass,
                OnHand = random.Next(5, 50),
                Pending = random.Next(0, 10),
                OnOrder = random.Next(0, 25),
                Alloc = random.Next(0, 15),
                BKO = random.Next(0, 5),
                InTransit = random.Next(0, 15),
                Needs = random.Next(0, 10),
                Interests = random.Next(0, 20),
                IsDiscontinued = random.Next(0, 20) == 0,
                IsPriceConfirm = random.Next(0, 10) == 0,
                IsDropShip = random.Next(0, 8) == 0,
                Components = Enumerable.Range(1, random.Next(3, 12)).Select(j => new SetComponent
                {
                    Component = $"COMP-{i:D3}-{j:D2}",
                    Description = $"Component {j} for Set {i}",
                    AdjQty = random.Next(1, 5),
                    OnHand = random.Next(10, 350),
                    PrintLabel = random.Next(0, 3) < 2
                }).ToList()
            });
        }

        return items;
    }

    public sealed class SetMaintenanceItem
    {
        public string Item { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal ListPrice { get; set; }
        public string Discount { get; set; } = "A5-H";
        public string Class { get; set; } = "CQT Stock";

        public int OnHand { get; set; }
        public int Pending { get; set; }
        public int OnOrder { get; set; }
        public int Alloc { get; set; }
        public int BKO { get; set; }
        public int InTransit { get; set; }
        public int Needs { get; set; }
        public int Interests { get; set; }

        public bool IsDiscontinued { get; set; }
        public bool IsPriceConfirm { get; set; }
        public bool IsCSI { get; set; }
        public bool IsDropShip { get; set; }
        public bool IsSpecialOrder { get; set; }

        public List<SetComponent> Components { get; set; } = new();
    }

    public sealed class SetComponent
    {
        public string Component { get; set; } = "";
        public string Description { get; set; } = "";
        public int AdjQty { get; set; }
        public int OnHand { get; set; }
        public bool PrintLabel { get; set; }
    }
}