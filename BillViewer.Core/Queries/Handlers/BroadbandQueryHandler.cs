namespace BillViewer.Core.Queries.Handlers
{
    using System.Threading.Tasks;

    using BillViewer.Core.Models.Bill;
    using BillViewer.Core.Models.View;

    public class BroadbandQueryHandler : IQueryHandlerAsync<BillQuery, BroadbandViewModel>
    {
        private readonly IQueryHandlerAsync<BillQuery, Bill> queryHandler;

        public BroadbandQueryHandler(IQueryHandlerAsync<BillQuery, Bill> queryHandler)
        {
            this.queryHandler = queryHandler;
        }

        public async Task<BroadbandViewModel> Execute(BillQuery query)
        {
            var bill = await queryHandler.Execute(query);

            var package = bill?.PackageName(PackageType.broadband);

            if (package?.Cost != null)
            {
                var model = new BroadbandViewModel()
                {
                    Package = package.Name,
                    Total = package.Cost
                };

                return model;
            }

            return null;
        }
    }
}