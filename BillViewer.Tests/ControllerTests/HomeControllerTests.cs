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
    public class HomeControllerTests
    {
        [Test]
        public async void Should_return_model()
        {
            var mockQuery = Substitute.For<IQueryHandlerAsync<BillQuery, OverviewViewModel>>();

            mockQuery.Execute(Arg.Any<BillQuery>()).Returns(Task.FromResult(new OverviewViewModel() { Total = 199.99M }));

            var controller = new HomeController(mockQuery);

            var result = await controller.Index() as ViewResult;

            var model = result.Model as OverviewViewModel;

            model.Total.ShouldEqual(199.99M);
        }
    }
}