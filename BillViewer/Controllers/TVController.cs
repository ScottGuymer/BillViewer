namespace BillViewer.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using BillViewer.Core;
    using BillViewer.Core.Models.View;
    using BillViewer.Core.Queries;

    public class TVController : Controller
    {
        private readonly IQueryHandlerAsync<BillQuery, TVViewModel> queryHandler;

        public TVController(IQueryHandlerAsync<BillQuery, TVViewModel> queryHandler)
        {
            this.queryHandler = queryHandler;
        }

        public async Task<ActionResult> Index()
        {
            var model = await this.queryHandler.Execute(new BillQuery());
            return this.View(model);
        }

    }
}