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
        public void Should_return_bill()
        {
            this.httpTest.RespondWithJson(new Bill());

            var query = new BillQueryHandler().Execute(new BillQuery());

            query.Wait();

            var bill = query.Result;

            bill.ShouldNotEqual(null);
        }

        [Test, ExpectedException]
        public void Should_not_return_bill()
        {
            this.httpTest.RespondWith(500, "no bill");
            var query = new BillQueryHandler().Execute(new BillQuery());

            query.Wait();

            var bill = query.Result;

            bill.ShouldNotEqual(null);
        }
    }
}