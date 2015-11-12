namespace BillViewer.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using BillViewer.Core;
    using BillViewer.Core.Models.View;
    using BillViewer.Core.Queries;

    public class HomeController : Controller
    {
        private readonly IQueryHandlerAsync<BillQuery, OverviewViewModel> queryHandler;

        public HomeController(IQueryHandlerAsync<BillQuery, OverviewViewModel> queryHandler)
        {
            this.queryHandler = queryHandler;
        }

        public async Task<ActionResult> Index()
        {
            var query = new BillQuery();
            var model = await queryHandler.Execute(query);
            return View(model);
        }

        public ActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
