namespace BillViewer.Tests.QueryHandlerTests
{
    using System.Collections.Generic;

    using BillViewer.Core;
    using BillViewer.Core.Models.Bill;
    using BillViewer.Core.Queries;
    using BillViewer.Core.Queries.Handlers;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class TvQueryHandlerTests
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
        public async void Should_return_package_name()
        {
            var query = new TVQueryHandler(billQueryHandler);
            bill.Package.Subscriptions.Add(new Subscription()
            {
                Name = "my telly package",
                Type = PackageType.tv.ToString()
            });

            var result = await query.Execute(new BillQuery());

            result.Package.ShouldEqual("my telly package");
        }

        [Test]
        public async void Should_return_package_cost()
        {
            var query = new TVQueryHandler(billQueryHandler);
            bill.Package.Subscriptions.Add(new Subscription()
            {
                Name = "my telly package",
                Type = PackageType.tv.ToString(),
                Cost = 20M
            });
            bill.SkyStore.Total = 50.12M;

            var result = await query.Execute(new BillQuery());

            result.Total.ShouldEqual(70.12M);   
        }
    }
}