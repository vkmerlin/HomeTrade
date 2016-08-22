using HRPortal.Business;
using HRPortal.Data;
using HRPortal.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Web.NUnitTests.MockDInjections
{
    public abstract class AbstarctFactory
    {
        protected INewsService newsService;
        protected INLogManager nLogManager;
        protected INewsRepository newsDao;
        protected INewsCategoryRepository newsCatsDao;

        public AbstarctFactory()
        {
            TestDependenciesRoot testKernel = new TestDependenciesRoot();
            newsService = testKernel.TryGetInstance<INewsService>();
            nLogManager = testKernel.TryGetInstance<INLogManager>();
            newsDao = testKernel.TryGetInstance<INewsRepository>();
            newsCatsDao = testKernel.TryGetInstance<INewsCategoryRepository>();
        }
    }

    public class ServiceFactory : AbstarctFactory
    {
        public HomeController HomeController => GetHomeController();

        private HomeController GetHomeController()
        {
            return new HomeController(newsService);
        }

        public NewsApiController NewsApiController => GetNewsApiController();

        private NewsApiController GetNewsApiController()
        {
            return new NewsApiController(newsService, nLogManager);
        }

        public NewsService NewsService => newsService as NewsService;
    }
}
