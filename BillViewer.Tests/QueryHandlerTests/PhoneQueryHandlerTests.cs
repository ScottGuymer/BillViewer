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

    [TestFixture]
    public class PhoneQueryHandlerTests
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
            var queryHandler = new PhoneQueryHandler(billQueryHandler);
            bill.Package.Subscriptions.Add(new Subscription()
            {
                Name = "Test",
                Type = PackageType.talk.ToString()
            });

            var model = await queryHandler.Execute(new BillQuery());

            model.Package.ShouldEqual("Test");
        }

        [Test]
        public async void Should_not_return_package_name_if_package_not_found()
        {
            var queryHandler = new PhoneQueryHandler(billQueryHandler);
            bill.Package.Subscriptions = new List<Subscription>();

            var model = await queryHandler.Execute(new BillQuery());

            model.Package.ShouldEqual(null);
        }

        [Test]
        public async void Should_return_call_charges()
        {
            var queryHandler = new PhoneQueryHandler(billQueryHandler);
            bill.CallCharges.Calls = new List<Call>()
                                         {
                                             new Call() { Cost = 1.99M, Duration = "10", Called = "07712312312" },
                                             new Call() { Cost = 1.99M, Duration = "10", Called = "07712312312" },
                                             new Call() { Cost = 1.99M, Duration = "10", Called = "07712312312" }
                                         };

            var model = await queryHandler.Execute(new BillQuery());

            model.Calls.Count().ShouldEqual(3);
        }

        [Test]
        public async void Should_calculate_total_cost()
        {
            var queryHandler = new PhoneQueryHandler(billQueryHandler);
            bill.CallCharges.Total = 5.97M;

            bill.Package.Subscriptions.Add(new Subscription() { Cost = 10.0M, Name = "Test", Type = PackageType.talk.ToString() });

            var model = await queryHandler.Execute(new BillQuery());

            model.Total.ShouldEqual(15.97M);
        }

        [Test]
        public async void Should_calculate_total_cost_with_no_calls()
        {
            var queryHandler = new PhoneQueryHandler(billQueryHandler);            

            bill.Package.Subscriptions.Add(new Subscription() { Cost = 10.0M, Name = "Test", Type = PackageType.talk.ToString() });

            var model = await queryHandler.Execute(new BillQuery());

            model.Total.ShouldEqual(10.0M);
        }

        [Test]
        public async void Should_calculate_total_cost_with_no_package()
        {
            var queryHandler = new PhoneQueryHandler(billQueryHandler);
            bill.CallCharges.Total = 5.97M;

            var model = await queryHandler.Execute(new BillQuery());

            model.Total.ShouldEqual(5.97M);
        }
    }
}