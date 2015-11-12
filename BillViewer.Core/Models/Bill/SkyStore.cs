namespace BillViewer.Core.Models.Bill
{
    using System.Collections.Generic;

    public class SkyStore
    {
        public List<Rental> Rentals { get; set; }

        public List<BuyAndKeep> BuyAndKeep { get; set; }

        public decimal Total { get; set; }
    }
}