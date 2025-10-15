namespace Prototype.Components.Pages.POXfer.ItemsOnOrder
{
    public class OpenPOLine
    {
        public string Item { get; set; } = ""; // <— Correlates to Row.Item
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
        public decimal TotalOpenCost => UnitCost * Open;

        public int Rcv { get; set; }
        public string Status { get; set; } = "Open";
    }
}
