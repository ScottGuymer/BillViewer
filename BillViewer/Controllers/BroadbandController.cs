namespace BillViewer.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using BillViewer.Core;
    using BillViewer.Core.Models.View;
    using BillViewer.Core.Queries;

    public class BroadbandController : Controller
    {
        private readonly IQueryHandlerAsync<BillQuery, BroadbandViewModel> queryHandler;

        public BroadbandController(IQueryHandlerAsync<BillQuery, BroadbandViewModel> queryHandler)
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