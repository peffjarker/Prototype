// Services/Reports/ItemsOnOrderPdfService.cs
using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Prototype.Components.Services.Reports
{
    public interface IItemsOnOrderPdfService
    {
        Task<byte[]> GenerateAsync(ItemsOnOrderReport report, CancellationToken ct = default);
    }

    public sealed class ItemsOnOrderPdfService : IItemsOnOrderPdfService
    {
        public async Task<byte[]> GenerateAsync(ItemsOnOrderReport report, CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(report);

            QuestPDF.Settings.License = LicenseType.Community;
            var culture = report.Culture ?? CultureInfo.CurrentCulture;

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(t => t.FontSize(10));
                    page.PageColor(Colors.White);

                    // ===== Header (unchanged) =====
                    page.Header().Element(h =>
                    {
                        h.Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text(report.Title ?? "Items on Order").FontSize(20).SemiBold();
                                if (!string.IsNullOrWhiteSpace(report.Subtitle))
                                    col.Item().Text(report.Subtitle!).FontColor(Colors.Grey.Medium);
                            });

                            row.ConstantItem(180).AlignRight().Column(col =>
                            {
                                col.Item().Text($"Date: {report.ReportDate:MM/dd/yyyy}").AlignRight();
                                if (!string.IsNullOrWhiteSpace(report.Company))
                                    col.Item().Text(report.Company!).AlignRight();
                            });
                        });
                    });

                    // ===== Content =====
                    page.Content().Column(col =>
                    {
                        // 1) Table (your existing implementation)
                        col.Item().Element(content =>
                        {
                            content.Table(table =>
                            {
                                table.ColumnsDefinition(cols =>
                                {
                                    cols.ConstantColumn(70);
                                    cols.RelativeColumn(4);
                                    cols.RelativeColumn(1);
                                    cols.RelativeColumn(1);
                                    cols.RelativeColumn(1);
                                    cols.RelativeColumn(1);
                                    cols.RelativeColumn(1);
                                    cols.RelativeColumn(1.4f);
                                });

                                table.Header(h =>
                                {
                                    h.Cell().Element(HeaderCell).Text("Item");
                                    h.Cell().Element(HeaderCell).Text("Description");
                                    h.Cell().Element(HeaderCell).AlignRight().Text("On Order");
                                    h.Cell().Element(HeaderCell).AlignRight().Text("Open");
                                    h.Cell().Element(HeaderCell).AlignRight().Text("In Transit");
                                    h.Cell().Element(HeaderCell).AlignRight().Text("Alloc");
                                    h.Cell().Element(HeaderCell).AlignRight().Text("BKO");
                                    h.Cell().Element(HeaderCell).AlignRight().Text("Total Open Cost");
                                });

                                var zebra = new[] { Colors.Grey.Darken4, Colors.White };
                                for (int i = 0; i < report.Rows.Count; i++)
                                {
                                    var r = report.Rows[i];
                                    var bg = zebra[i % 2];

                                    table.Cell().Element(c => BodyCell(c, bg)).Text(r.Item ?? string.Empty);
                                    table.Cell().Element(c => BodyCell(c, bg)).Text(r.Description ?? string.Empty);

                                    table.Cell().Element(c => BodyCell(c, bg)).AlignRight().Text(r.OnOrder.ToString(culture));
                                    table.Cell().Element(c => BodyCell(c, bg)).AlignRight().Text(r.Open.ToString(culture));
                                    table.Cell().Element(c => BodyCell(c, bg)).AlignRight().Text(r.InTransit.ToString(culture));
                                    table.Cell().Element(c => BodyCell(c, bg)).AlignRight().Text(r.Alloc.ToString(culture));
                                    table.Cell().Element(c => BodyCell(c, bg)).AlignRight().Text(r.Bko.ToString(culture));
                                    table.Cell().Element(c => BodyCell(c, bg)).AlignRight().Text(r.TotalOpenCost.ToString("C", culture));
                                }

                                // Optional totals row inside the table
                                if (report.ShowTotals)
                                {
                                    var totalsOnOrder = report.Rows.Sum(x => x.OnOrder);
                                    var totalsOpen = report.Rows.Sum(x => x.Open);
                                    var totalsTransit = report.Rows.Sum(x => x.InTransit);
                                    var totalsAlloc = report.Rows.Sum(x => x.Alloc);
                                    var totalsBko = report.Rows.Sum(x => x.Bko);
                                    var totalsCost = report.Rows.Sum(x => x.TotalOpenCost);

                                    table.Cell().ColumnSpan(2).Element(FooterCell).Text("Totals").SemiBold();
                                    table.Cell().Element(FooterCell).AlignRight().Text(totalsOnOrder.ToString(culture)).SemiBold();
                                    table.Cell().Element(FooterCell).AlignRight().Text(totalsOpen.ToString(culture)).SemiBold();
                                    table.Cell().Element(FooterCell).AlignRight().Text(totalsTransit.ToString(culture)).SemiBold();
                                    table.Cell().Element(FooterCell).AlignRight().Text(totalsAlloc.ToString(culture)).SemiBold();
                                    table.Cell().Element(FooterCell).AlignRight().Text(totalsBko.ToString(culture)).SemiBold();
                                    table.Cell().Element(FooterCell).AlignRight().Text(totalsCost.ToString("C", culture)).SemiBold();
                                }

                                static IContainer HeaderCell(IContainer c) =>
                                    c.PaddingVertical(6).PaddingHorizontal(4)
                                     .BorderBottom(1).BorderColor(Colors.Grey.Medium)
                                     .DefaultTextStyle(t => t.SemiBold());

                                static IContainer BodyCell(IContainer c, string bg) =>
                                    c.PaddingVertical(5).PaddingHorizontal(4)
                                     .BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten3);

                                static IContainer FooterCell(IContainer c) =>
                                    c.PaddingVertical(6).PaddingHorizontal(4)
                                     .BorderTop(1).BorderColor(Colors.Grey.Medium);
                            });
                        });

                        // 2) Standalone summary strip AFTER the table
                        col.Item().PaddingTop(14).Element(c =>
                        {
                            var sumInTransit = report.Rows.Sum(r => r.InTransitCost);
                            var sumBackordersAwait = report.Rows.Sum(r => r.BackorderedCost);     // your UI shows the same value twice
                            var sumBackordered = report.Rows.Sum(r => r.BackorderedCost);
                            var sumUnacknowledged = report.Rows.Sum(r => r.UnacknowledgedCost);
                            var sumOpenItems = report.Rows.Sum(r => r.TotalOpenCost);

                            var culture = report.Culture ?? CultureInfo.CurrentCulture;

                            c.Row(row =>
                            {
                                row.ConstantItem(70).Text("Totals:").SemiBold();

                                row.RelativeItem().Text(t =>
                                {
                                    t.Span("In Transit ").SemiBold();
                                    t.Span(sumInTransit.ToString("C", culture)).SemiBold().FontColor(Colors.Green.Medium);
                                });

                                row.RelativeItem().Text(t =>
                                {
                                    t.Span("Backordered ").SemiBold();
                                    t.Span(sumBackordered.ToString("C", culture)).SemiBold().FontColor(Colors.Green.Medium);
                                });

                                row.RelativeItem().Text(t =>
                                {
                                    t.Span("Unacknowledged ").SemiBold();
                                    t.Span(sumUnacknowledged.ToString("C", culture)).SemiBold().FontColor(Colors.Green.Medium);
                                });

                                row.RelativeItem().Text(t =>
                                {
                                    t.Span("Open Items ").SemiBold();
                                    t.Span(sumOpenItems.ToString("C", culture)).SemiBold().FontColor(Colors.Green.Medium);
                                });
                            });
                        });
                    });

                    // ===== Footer (unchanged) =====
                    page.Footer().AlignRight().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.Span(" of ");
                        x.TotalPages();
                    });
                });
            }).GeneratePdf();

            await Task.CompletedTask;
            return pdf;
        }
    }

    // ===== Report Models (UPDATED) =====
    public sealed class ItemsOnOrderReport
    {
        public string? Title { get; init; } = "Items on Order";
        public string? Subtitle { get; init; }
        public string? Company { get; init; }
        public DateTime ReportDate { get; init; } = DateTime.Today;
        public List<ItemsOnOrderRow> Rows { get; init; } = new();
        public bool ShowTotals { get; init; } = true;
        public CultureInfo? Culture { get; init; }
    }

    // Services/Reports/ItemsOnOrderPdfService.cs
    public sealed class ItemsOnOrderRow
    {
        public string? Item { get; init; }
        public string? Description { get; init; }
        public int OnOrder { get; init; }
        public int Open { get; init; }
        public int InTransit { get; init; }
        public int Alloc { get; init; }
        public int Bko { get; init; }

        // Cost fields (these must come from your grid VM)
        public decimal TotalOpenCost { get; init; }       // Open Items
        public decimal InTransitCost { get; init; }       // In Transit
        public decimal BackorderedCost { get; init; }     // Backordered (and “Awaiting Release” per your UI)
        public decimal UnacknowledgedCost { get; init; }  // Unacknowledged
    }
}
