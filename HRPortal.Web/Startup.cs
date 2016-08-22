using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HRPortal.Web.Startup))]
namespace HRPortal.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
