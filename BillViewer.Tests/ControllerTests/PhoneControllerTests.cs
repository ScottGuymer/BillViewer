namespace BillViewer.Tests.ControllerTests
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using BillViewer.Controllers;
    using BillViewer.Core;
    using BillViewer.Core.Models.View;
    using BillViewer.Core.Queries;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class PhoneControllerTests
    {
        [Test]
        public async void Should_return_total()
        {
            var mockQuery = Substitute.For<IQueryHandlerAsync<BillQuery, PhoneViewModel>>();

            mockQuery.Execute(Arg.Any<BillQuery>()).Returns(Task.FromResult(new PhoneViewModel() { Total = 199.99M }));

            var controller = new PhoneController(mockQuery);

            var result = await controller.Index() as ViewResult;

            var model = result.Model as PhoneViewModel;

            model.Total.ShouldEqual(199.99M);
        }
    }
}