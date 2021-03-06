﻿namespace BillViewer.Core.Models.View
{
    public class SubscriptionViewModel
    {
        public string Name { get; set; }

        public PackageType Type { get; set; }

        public decimal Cost { get; set; }

        public decimal ExtraCosts { get; set; }

        public decimal TotalCost { get; set; }
    }
}