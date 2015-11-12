namespace BillViewer.Core.Models.View
{
    using System.Collections.Generic;

    using BillViewer.Core.Models.Bill;

    public class TVViewModel
    {
        public string Package { get; set; }

        public List<BuyAndKeep> BuyAndKeep { get; set; }

        public List<BuyAndKeep> Rentals { get; set; }

        public decimal Total { get; set; }
    }
}