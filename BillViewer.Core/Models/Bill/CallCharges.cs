namespace BillViewer.Core.Models.Bill
{
    using System.Collections.Generic;

    public class CallCharges
    {
        public List<Call> Calls { get; set; }

        public decimal Total { get; set; }
    }
}