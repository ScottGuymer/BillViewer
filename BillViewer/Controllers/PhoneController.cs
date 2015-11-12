namespace BillViewer.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using BillViewer.Core;
    using BillViewer.Core.Models.View;
    using BillViewer.Core.Queries;

    public class PhoneController : Controller
    {
        private readonly IQueryHandlerAsync<BillQuery, PhoneViewModel> phoneQuery;

        public PhoneController(IQueryHandlerAsync<BillQuery, PhoneViewModel> phoneQuery)
        {
            this.phoneQuery = phoneQuery;
        }

        public async Task<ActionResult> Index()
        {
            var model = await phoneQuery.Execute(new BillQuery());
            return this.View(model);
        }
    }
}