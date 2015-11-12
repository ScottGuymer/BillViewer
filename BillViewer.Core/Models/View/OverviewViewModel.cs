namespace BillViewer.Core.Models.View
{
    using System;
    using System.Collections.Generic;

    using BillViewer.Core.Models.Bill;

    public class OverviewViewModel
    {
        public DateTime PaymentDate { get; set; }

        public IEnumerable<SubscriptionViewModel> Packages { get; set; }

        public decimal Total { get; set; }
    }
}