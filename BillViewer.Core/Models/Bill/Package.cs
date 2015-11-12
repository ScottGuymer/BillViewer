namespace BillViewer.Core.Models.Bill
{
    using System.Collections.Generic;

    public class Package
    {
        public List<Subscription> Subscriptions { get; set; }

        public decimal Total { get; set; }
    }
}