namespace BillViewer.Tests.QueryHandlerTests
{
    using BillViewer.Core.Models.Bill;
    using BillViewer.Core.Queries;
    using BillViewer.Core.Queries.Handlers;

    using Flurl.Http.Testing;

    using NUnit.Framework;

    [TestFixture]
    public class BillQueryHandlerTests
    {
        private HttpTest httpTest;

        [SetUp]
        public void SetUp()
        {

            this.httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            this.httpTest.Dispose();
        }

        [Test]
        public async void Should_return_bill()
        {
            this.httpTest.RespondWithJson(new Bill() {Total = 100});

            var bill = await new BillQueryHandler().Execute(new BillQuery());       

            bill.ShouldNotEqual(null);
            bill.Total.ShouldEqual(100M);
        }

        [Test, ExpectedException]
        public async void Should_not_return_bill()
        {
            // for the moment we expect it to throw but we should add some error handling
            this.httpTest.RespondWith(500, "no bill");
            await new BillQueryHandler().Execute(new BillQuery());
        }

        [Test]
        public async void Should_handle_wrong_json()
        {
            this.httpTest.RespondWithJson(new { nottherightjason = "jason" });
            var bill = await new BillQueryHandler().Execute(new BillQuery());

            bill.ShouldNotEqual(null);
        }
    }
}