namespace BillViewer.Core.Queries.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BillViewer.Core.Models.Bill;
    using BillViewer.Core.Models.View;

    public class OverviewQueryHandler : IQueryHandlerAsync<BillQuery, OverviewViewModel>
    {
        private readonly IQueryHandlerAsync<BillQuery, Bill> billQueryHandler;

        public OverviewQueryHandler(IQueryHandlerAsync<BillQuery, Bill> queryHandler)
        {
            this.billQueryHandler = queryHandler;
        }

        public async Task<OverviewViewModel> Execute(BillQuery query)
        {
            var bill = await this.billQueryHandler.Execute(query);

            var overview = new OverviewViewModel
            {
                PaymentDate = bill.Statement.Due,
                Packages = bill.Package.Subscriptions,
                Total = bill.Total
            };

            return overview;
        }
    }
}