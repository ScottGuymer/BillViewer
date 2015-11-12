namespace BillViewer.Core.Queries.Handlers
{
    using System;
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
                Packages = bill.Package.Subscriptions.Select(x => Selector(x, bill)),
                Total = bill.Total
            };

            return overview;
        }

        private SubscriptionViewModel Selector(Subscription subscription, Bill bill)
        {
            var subViewModel = new SubscriptionViewModel()
            {
                Name = subscription.Name,
                Type = (PackageType)Enum.Parse(typeof(PackageType), subscription.Type),
                Cost = subscription.Cost
            };

            // bit noddy this should be extended with some sort of provider to do this
            switch (subViewModel.Type)
            {
                case PackageType.broadband:
                    // no extra costs here
                    break;
                case PackageType.talk:
                    if (bill?.CallCharges?.Total != null)
                    {
                        subViewModel.ExtraCosts = bill.CallCharges.Total;
                    }

                    break;
                case PackageType.tv:
                    if (bill?.SkyStore?.Total != null)
                    {
                        subViewModel.ExtraCosts = bill.SkyStore.Total;
                    }

                    break;
            }

            subViewModel.TotalCost = subViewModel.Cost + subViewModel.ExtraCosts;

            return subViewModel;
        }
    }
}