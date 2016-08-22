using HRPortal.Web.App_Start.DInjection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using HRPortal.Web.App_Start;
using HRPortal.Model;

namespace HRPortal.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DataBaseInitializator.InitDB();
            MapperFactory.RegisterMappings();

            //Registering API routing
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Dependencies for MVC Controllers
            DependencyResolver.SetResolver(new HRPortalDependencyResolver());
            //Dependencies for WEB API
            NinjectWebCommon.CreateKernel();
        }
    }
}
