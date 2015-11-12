namespace BillViewer.Core.Queries.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BillViewer.Core;
    using BillViewer.Core.Models.Bill;
    using BillViewer.Core.Models.View;

    public class PhoneQueryHandler : IQueryHandlerAsync<BillQuery, PhoneViewModel>
    {
        private readonly IQueryHandlerAsync<BillQuery, Bill> billQueryHandler;

        public PhoneQueryHandler(IQueryHandlerAsync<BillQuery, Bill> billQueryHandler)
        {
            this.billQueryHandler = billQueryHandler;
        }

        public async Task<PhoneViewModel> Execute(BillQuery query)
        {
            var bill = await billQueryHandler.Execute(query);

            var package = bill.PackageName(PackageType.talk);

            decimal totalCost = 0;
            if (package != null)
            {
                totalCost = package.Cost;
            }

            if (bill?.CallCharges?.Total != null)
            {
                totalCost = totalCost + bill.CallCharges.Total;
            }

            var model = new PhoneViewModel()
            {
                Calls = bill?.CallCharges?.Calls,
                Package = package?.Name,
                Total = totalCost
            };

            return model;
        }
    }
}