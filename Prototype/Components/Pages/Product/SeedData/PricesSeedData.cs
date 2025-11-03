namespace Prototype.Pages.Product.SeedData;

public static class PricesSeedData
{
    public static List<PricingItem> GetItems()
    {
        var random = new Random(42); // Fixed seed for consistent data

        var items = new List<PricingItem>
        {
            // Items with future price changes
            new PricingItem
            {
                Item = "1/4",
                Description = "1/4\" Ball (25-Pack)",
                ListPrice = 5.09m,
                Discount = "A",
                Class = "CQT Stock",
                Category = "Drive Tools",
                Subcategory = "3/8\" Ratchets",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2099, 1, 1), LocalList = 6.19m, Disc = "A", HasCalendar = true },
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 5.09m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 4.79m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2023, 1, 1), LocalList = 4.49m, Disc = "A", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 4.59m, PromoType = "Black Friday" },
                    new PromotionEntry { Year = "2025", Month = "December", PromoList = 4.59m, PromoType = "Holiday Sale" }
                }
            },
            new PricingItem
            {
                Item = "1/8",
                Description = "1/8\" Ball Bearing",
                ListPrice = 4.25m,
                Discount = "A",
                Class = "CQT Stock",
                Category = "Drive Tools",
                Subcategory = "3/8\" Ratchets",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 4.25m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 6, 1), LocalList = 3.99m, Disc = "A", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>()
            },

            // Socket Sets with price history
            new PricingItem
            {
                Item = "S38SET-42",
                Description = "3/8\" Socket Set 42pc Metric",
                ListPrice = 299.99m,
                Discount = "C",
                Class = "CQT Stock",
                Category = "Hand Tools",
                Subcategory = "Sockets",
                TaxExempt = false,
                IsPriceConfirm = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2026, 1, 1), LocalList = 329.99m, Disc = "C", HasCalendar = true },
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 299.99m, Disc = "C", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 279.99m, Disc = "C", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2023, 7, 1), LocalList = 259.99m, Disc = "C", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 269.99m, PromoType = "Black Friday" },
                    new PromotionEntry { Year = "2025", Month = "September", PromoList = 279.99m, PromoType = "Back to Work" }
                }
            },
            new PricingItem
            {
                Item = "S12SET-SAE",
                Description = "1/2\" Socket Set 28pc SAE",
                ListPrice = 349.99m,
                Discount = "C",
                Class = "CQT Stock",
                Category = "Hand Tools",
                Subcategory = "Sockets",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 349.99m, Disc = "C", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 329.99m, Disc = "C", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 299.99m, PromoType = "Black Friday" }
                }
            },

            // Wrenches
            new PricingItem
            {
                Item = "CW-SET10M",
                Description = "Combination Wrench Set 10pc Metric",
                ListPrice = 189.99m,
                Discount = "B",
                Class = "CQT Stock",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 189.99m, Disc = "B", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 179.99m, Disc = "B", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2023, 1, 1), LocalList = 169.99m, Disc = "B", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "December", PromoList = 169.99m, PromoType = "Year End" }
                }
            },
            new PricingItem
            {
                Item = "LPN-RW7M",
                Description = "Ratcheting Wrench Set 7pc Metric",
                ListPrice = 249.99m,
                Discount = "B",
                Class = "Local Only",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 8, 1), LocalList = 249.99m, Disc = "B", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 8, 1), LocalList = 229.99m, Disc = "B", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>()
            },
            new PricingItem
            {
                Item = "CFWS10MM",
                Description = "10mm Flex Head Ratcheting Wrench",
                ListPrice = 34.99m,
                Discount = "B",
                Class = "CQT Stock",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 34.99m, Disc = "B", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 32.99m, Disc = "B", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 29.99m, PromoType = "Black Friday" }
                }
            },

            // Discontinued items (Local Only)
            new PricingItem
            {
                Item = "LPN-1002",
                Description = "Combination Wrench Set 10pc SAE (Discontinued)",
                ListPrice = 89.99m,
                Discount = "A",
                Class = "Local Only",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                IsDiscontinued = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 89.99m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 84.99m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2023, 1, 1), LocalList = 79.99m, Disc = "A", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 69.99m, PromoType = "Clearance" }
                }
            },
            new PricingItem
            {
                Item = "LPN-1003",
                Description = "Adjustable Wrench 12\" (Discontinued)",
                ListPrice = 45.50m,
                Discount = "A",
                Class = "Local Only",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                IsDiscontinued = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 45.50m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 6, 1), LocalList = 42.99m, Disc = "A", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 35.99m, PromoType = "Clearance" }
                }
            },

            // Storage items
            new PricingItem
            {
                Item = "10030A25E717",
                Description = "Tool Box Lock Assembly",
                ListPrice = 125.00m,
                Discount = "A",
                Class = "CQT Stock",
                Category = "Storage",
                Subcategory = "Tool Boxes",
                TaxExempt = false,
                IsPriceConfirm = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 125.00m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 119.99m, Disc = "A", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>()
            },
            new PricingItem
            {
                Item = "CTBR7219",
                Description = "72\" Rolling Tool Cabinet - Black",
                ListPrice = 4599.99m,
                Discount = "PROMO",
                Class = "CQT Stock",
                Category = "Storage",
                Subcategory = "Tool Boxes",
                TaxExempt = false,
                IsDropShip = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2026, 1, 1), LocalList = 4899.99m, Disc = "PROMO", HasCalendar = true },
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 4599.99m, Disc = "PROMO", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 4399.99m, Disc = "PROMO", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 3999.99m, PromoType = "Black Friday" },
                    new PromotionEntry { Year = "2025", Month = "December", PromoList = 4199.99m, PromoType = "Holiday Sale" }
                }
            },

            // Power Tools
            new PricingItem
            {
                Item = "1008DA21PNE709",
                Description = "20V Cordless Drill/Driver Kit",
                ListPrice = 299.99m,
                Discount = "A",
                Class = "CQT Stock",
                Category = "Power Tools",
                Subcategory = "Drills",
                TaxExempt = false,
                IsDropShip = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 299.99m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 279.99m, Disc = "A", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 249.99m, PromoType = "Black Friday" },
                    new PromotionEntry { Year = "2025", Month = "December", PromoList = 269.99m, PromoType = "Holiday Sale" }
                }
            },
            new PricingItem
            {
                Item = "IW38",
                Description = "3/8\" Cordless Impact Wrench",
                ListPrice = 449.99m,
                Discount = "B",
                Class = "CQT Stock",
                Category = "Power Tools",
                Subcategory = "Impact Tools",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 449.99m, Disc = "B", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 429.99m, Disc = "B", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 399.99m, PromoType = "Black Friday" }
                }
            },

            // Diagnostics (Tax Exempt)
            new PricingItem
            {
                Item = "SCAN5500",
                Description = "Professional Diagnostic Scanner",
                ListPrice = 3299.99m,
                Discount = "TECH",
                Class = "CQT Stock",
                Category = "Diagnostics",
                Subcategory = "Scanners",
                TaxExempt = true,
                IsPriceConfirm = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2026, 1, 1), LocalList = 3499.99m, Disc = "TECH", HasCalendar = true },
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 3299.99m, Disc = "TECH", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 3099.99m, Disc = "TECH", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2023, 7, 1), LocalList = 2899.99m, Disc = "TECH", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 2999.99m, PromoType = "Tech Week" }
                }
            },
            new PricingItem
            {
                Item = "MM500",
                Description = "Digital Multimeter Pro",
                ListPrice = 189.99m,
                Discount = "A",
                Class = "CQT Stock",
                Category = "Diagnostics",
                Subcategory = "Meters",
                TaxExempt = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 189.99m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 179.99m, Disc = "A", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>()
            },

            // Screwdriver Sets
            new PricingItem
            {
                Item = "SD-SET20",
                Description = "Screwdriver Set 20pc Professional",
                ListPrice = 124.99m,
                Discount = "B",
                Class = "CQT Stock",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                IsPriceConfirm = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 124.99m, Disc = "B", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 119.99m, Disc = "B", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2023, 1, 1), LocalList = 109.99m, Disc = "B", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 99.99m, PromoType = "Black Friday" }
                }
            },

            // Pliers
            new PricingItem
            {
                Item = "PL-SET5",
                Description = "Pliers Set 5pc Professional",
                ListPrice = 159.99m,
                Discount = "A",
                Class = "CQT Stock",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 159.99m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 149.99m, Disc = "A", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "December", PromoList = 139.99m, PromoType = "Holiday Sale" }
                }
            },

            // Hex Keys
            new PricingItem
            {
                Item = "LPN-HK22M",
                Description = "Hex Key Set 22pc Metric Ball End",
                ListPrice = 89.99m,
                Discount = "A",
                Class = "Local Only",
                Category = "Hand Tools",
                Subcategory = "Extensions",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 89.99m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 84.99m, Disc = "A", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>()
            },

            // Impact Sockets
            new PricingItem
            {
                Item = "IMP-SET19",
                Description = "1/2\" Impact Socket Set 19pc Deep",
                ListPrice = 399.99m,
                Discount = "C",
                Class = "CQT Stock",
                Category = "Hand Tools",
                Subcategory = "Sockets",
                TaxExempt = false,
                IsDropShip = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2026, 1, 1), LocalList = 429.99m, Disc = "C", HasCalendar = true },
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 399.99m, Disc = "C", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 379.99m, Disc = "C", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 349.99m, PromoType = "Black Friday" }
                }
            },

            // Combo Kit
            new PricingItem
            {
                Item = "STARTER-KIT",
                Description = "Technician Starter Tool Kit 150pc",
                ListPrice = 1299.99m,
                Discount = "TECH",
                Class = "CQT Stock",
                Category = "Hand Tools",
                Subcategory = "Sockets",
                TaxExempt = false,
                IsPriceConfirm = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2026, 1, 1), LocalList = 1399.99m, Disc = "TECH", HasCalendar = true },
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 1299.99m, Disc = "TECH", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 1199.99m, Disc = "TECH", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 1099.99m, PromoType = "Black Friday" },
                    new PromotionEntry { Year = "2025", Month = "September", PromoList = 1199.99m, PromoType = "Back to School" }
                }
            },

            // Specialty Tools
            new PricingItem
            {
                Item = "LPN-BRK24",
                Description = "Brake Service Tool Kit 24pc",
                ListPrice = 449.99m,
                Discount = "B",
                Class = "Local Only",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 449.99m, Disc = "B", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 429.99m, Disc = "B", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 399.99m, PromoType = "Black Friday" }
                }
            },
            new PricingItem
            {
                Item = "ELECTRIC-KIT",
                Description = "Electrical Diagnostic Kit 18pc",
                ListPrice = 549.99m,
                Discount = "TECH",
                Class = "CQT Stock",
                Category = "Diagnostics",
                Subcategory = "Meters",
                TaxExempt = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 549.99m, Disc = "TECH", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 529.99m, Disc = "TECH", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 499.99m, PromoType = "Tech Week" }
                }
            },

            // Torque Wrenches
            new PricingItem
            {
                Item = "TQ-SET3",
                Description = "Torque Wrench Set 3pc Professional",
                ListPrice = 899.99m,
                Discount = "TECH",
                Class = "CQT Stock",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 899.99m, Disc = "TECH", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 849.99m, Disc = "TECH", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 799.99m, PromoType = "Black Friday" }
                }
            },

            // Files and Punches
            new PricingItem
            {
                Item = "FILE-SET8",
                Description = "File Set 8pc Professional",
                ListPrice = 119.99m,
                Discount = "A",
                Class = "CQT Stock",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 119.99m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 109.99m, Disc = "A", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>()
            },
            new PricingItem
            {
                Item = "PUNCH-SET12",
                Description = "Punch and Chisel Set 12pc",
                ListPrice = 79.99m,
                Discount = "A",
                Class = "CQT Stock",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 79.99m, Disc = "A", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 74.99m, Disc = "A", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 69.99m, PromoType = "Black Friday" }
                }
            },

            // Tap and Die
            new PricingItem
            {
                Item = "LPN-TD40M",
                Description = "Tap and Die Set 40pc Metric",
                ListPrice = 299.99m,
                Discount = "B",
                Class = "Local Only",
                Category = "Hand Tools",
                Subcategory = "Wrenches",
                TaxExempt = false,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 299.99m, Disc = "B", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 279.99m, Disc = "B", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "December", PromoList = 269.99m, PromoType = "Holiday Sale" }
                }
            },

            // Inspection Camera
            new PricingItem
            {
                Item = "10995DI",
                Description = "Digital Inspection Camera",
                ListPrice = 599.99m,
                Discount = "TECH",
                Class = "CQT Stock",
                Category = "Diagnostics",
                Subcategory = "Scanners",
                TaxExempt = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 599.99m, Disc = "TECH", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 7, 1), LocalList = 549.99m, Disc = "TECH", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>
                {
                    new PromotionEntry { Year = "2025", Month = "November", PromoList = 529.99m, PromoType = "Tech Week" }
                }
            },

            // Measurement Tools
            new PricingItem
            {
                Item = "1040305331",
                Description = "Precision Measurement Tool Set",
                ListPrice = 749.99m,
                Discount = "TECH",
                Class = "CQT Stock",
                Category = "Diagnostics",
                Subcategory = "Meters",
                TaxExempt = true,
                IsCSI = true,
                PriceHistory = new List<PriceHistoryEntry>
                {
                    new PriceHistoryEntry { Effective = new DateTime(2025, 7, 10), LocalList = 749.99m, Disc = "TECH", HasCalendar = false },
                    new PriceHistoryEntry { Effective = new DateTime(2024, 1, 1), LocalList = 699.99m, Disc = "TECH", HasCalendar = false }
                },
                Promotions = new List<PromotionEntry>()
            }
        };

        // Add more items with varied pricing history (increase to 30 for more data)
        for (int i = 1; i <= 30; i++)
        {
            var hasPromo = random.Next(0, 3) == 0;
            var hasFuturePrice = random.Next(0, 4) == 0;
            var basePrice = 29.99m + (i * 5m);

            // Determine class - 70% CQT Stock, 30% Local Only
            var itemClass = random.Next(0, 10) < 7 ? "CQT Stock" : "Local Only";
            var itemPrefix = itemClass == "Local Only" ? "LPN-PRC" : "PRCGEN";

            var priceHistory = new List<PriceHistoryEntry>
            {
                new PriceHistoryEntry
                {
                    Effective = new DateTime(2025, 7, 10),
                    LocalList = basePrice,
                    Disc = random.Next(0, 2) == 0 ? "A" : "B",
                    HasCalendar = false
                },
                new PriceHistoryEntry
                {
                    Effective = new DateTime(2024, 1, 1),
                    LocalList = basePrice - 5m,
                    Disc = random.Next(0, 2) == 0 ? "A" : "B",
                    HasCalendar = false
                }
            };

            if (hasFuturePrice)
            {
                priceHistory.Insert(0, new PriceHistoryEntry
                {
                    Effective = new DateTime(2026, random.Next(1, 7), 1),
                    LocalList = basePrice + 10m,
                    Disc = priceHistory[0].Disc,
                    HasCalendar = true
                });
            }

            var promotions = hasPromo ? new List<PromotionEntry>
            {
                new PromotionEntry
                {
                    Year = "2025",
                    Month = "November",
                    PromoList = basePrice - 10m,
                    PromoType = "Black Friday"
                }
            } : new List<PromotionEntry>();

            items.Add(new PricingItem
            {
                Item = $"{itemPrefix}{i:D4}",
                Description = $"Generic Pricing Item {i}",
                ListPrice = basePrice,
                Discount = new[] { "A", "B", "C", "TECH" }[random.Next(4)],
                Class = itemClass,
                Category = new[] { "Hand Tools", "Drive Tools", "Power Tools" }[random.Next(3)],
                Subcategory = new[] { "Wrenches", "Sockets", "Extensions", "3/8\" Ratchets" }[random.Next(4)],
                TaxExempt = random.Next(0, 5) == 0,
                IsDiscontinued = random.Next(0, 20) == 0,
                IsPriceConfirm = random.Next(0, 10) == 0,
                IsDropShip = random.Next(0, 8) == 0,
                PriceHistory = priceHistory,
                Promotions = promotions
            });
        }

        return items;
    }

    public sealed class PricingItem
    {
        public string Item { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal ListPrice { get; set; }
        public string Discount { get; set; } = "A";
        public string Class { get; set; } = "CQT Stock";
        public string Category { get; set; } = "";
        public string Subcategory { get; set; } = "";
        public bool TaxExempt { get; set; }

        public bool IsDiscontinued { get; set; }
        public bool IsPriceConfirm { get; set; }
        public bool IsCSI { get; set; }
        public bool IsDropShip { get; set; }
        public bool IsSpecialOrder { get; set; }

        public List<PriceHistoryEntry> PriceHistory { get; set; } = new();
        public List<PromotionEntry> Promotions { get; set; } = new();
    }

    public sealed class PriceHistoryEntry
    {
        public DateTime Effective { get; set; }
        public decimal LocalList { get; set; }
        public string Disc { get; set; } = "";
        public bool HasCalendar { get; set; }
    }

    public sealed class PromotionEntry
    {
        public string Year { get; set; } = "";
        public string Month { get; set; } = "";
        public decimal? PromoList { get; set; }
        public string PromoType { get; set; } = "";
    }
}