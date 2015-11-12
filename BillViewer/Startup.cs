using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BillViewer.Startup))]
namespace BillViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
