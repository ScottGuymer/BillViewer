namespace BillViewer.Tests.QueryHandlerTests
{
    using System.Collections.Generic;
    using System.Linq;

    using BillViewer.Core;
    using BillViewer.Core.Models.Bill;
    using BillViewer.Core.Queries;
    using BillViewer.Core.Queries.Handlers;

    using NSubstitute;

    using NUnit.Framework;

    public class OverviewQueryHandlerTests
    {
        private IQueryHandlerAsync<BillQuery, Bill> billQueryHandler;

        private Bill bill;

        [SetUp]
        public void Setup()
        {
            billQueryHandler = Substitute.For<IQueryHandlerAsync<BillQuery, Bill>>();

            billQueryHandler.Execute(Arg.Any<BillQuery>()).Returns(info => bill);

            bill = new Bill
            {
                Package = new Package { Subscriptions = new List<Subscription>() },
                CallCharges = new CallCharges(),
                Statement = new Statement(),
                SkyStore = new SkyStore()
            };
        }

        [Test]
        public async void Should_return_packages()
        {
            var query = new OverviewQueryHandler(billQueryHandler);

            bill.Package.Subscriptions.Add(new Subscription()
            {
                Type = PackageType.broadband.ToString(),
                Name = "mybroadband",
                Cost = 17.87M
            });

            bill.Package.Subscriptions.Add(new Subscription()
            {
                Type = PackageType.tv.ToString(),
                Name = "mybroadband",
                Cost = 10.0M
            });

            var model = await query.Execute(new BillQuery());

            model.Packages.Count().ShouldEqual(2);      
            
            model.Packages.Sum(x => x.TotalCost).ShouldEqual(27.87M);      
        }

        [Test]
        public async void Should_return_total_price()
        {
            var query = new OverviewQueryHandler(billQueryHandler);

            bill.Total = 198.56M;
                       
            var model = await query.Execute(new BillQuery());

            model.Total.ShouldEqual(198.56M);
        }
    }
}