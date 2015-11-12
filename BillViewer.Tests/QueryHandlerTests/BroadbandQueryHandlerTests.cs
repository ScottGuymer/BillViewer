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
    public class BroadbandQueryHandlerTests
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
        public async void Should_return_package_and_total()
        {
            var query = new BroadbandQueryHandler(billQueryHandler);

            bill.Package.Subscriptions.Add(new Subscription()
            {
                Type = PackageType.broadband.ToString(),
                Name = "mybroadband",
                Cost = 17.87M
            });

            var model = await query.Execute(new BillQuery());

            model.Package.ShouldEqual("mybroadband");
            model.Total.ShouldEqual(17.87M);
        }
    }
}