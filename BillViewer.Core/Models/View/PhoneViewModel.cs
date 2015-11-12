namespace BillViewer.Core.Models.View
{
    using System.Collections.Generic;

    using BillViewer.Core.Models.Bill;

    public class PhoneViewModel
    {
        public string Package { get; set; }

        public IEnumerable<Call> Calls { get; set; }

        public decimal Total { get; set; }
    }
}