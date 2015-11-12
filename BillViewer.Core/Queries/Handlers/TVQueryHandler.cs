namespace BillViewer.Core.Queries.Handlers
{
    using System.Threading.Tasks;

    using BillViewer.Core;
    using BillViewer.Core.Models.Bill;
    using BillViewer.Core.Models.View;
    using BillViewer.Core.Queries;

    public class TVQueryHandler : IQueryHandlerAsync<BillQuery, TVViewModel>
    {
        private readonly IQueryHandlerAsync<BillQuery, Bill> queryHandler;

        public TVQueryHandler(IQueryHandlerAsync<BillQuery, Bill> queryHandler)
        {
            this.queryHandler = queryHandler;
        }

        public async Task<TVViewModel> Execute(BillQuery query)
        {
            var bill = await queryHandler.Execute(query);

            var package = bill.PackageName(PackageType.tv);

            var model = new TVViewModel()
            {
                Package = package?.Name,
                BuyAndKeep = bill.SkyStore.BuyAndKeep,
                Rentals = bill.SkyStore.BuyAndKeep,
                Total = bill.Total
            };

            return model;
        }
    }
}