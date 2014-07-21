using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustoDN.Web.Startup))]
namespace CustoDN.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
