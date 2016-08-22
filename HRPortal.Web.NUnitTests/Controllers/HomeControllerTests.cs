using HRPortal.Business;
using HRPortal.Web.Controllers;
using HRPortal.Web.NUnitTests.MockDInjections;
using NUnit.Framework;

namespace HRPortal.Web.NUnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        INewsService newsService;
        public HomeControllerTests()
        {
            var testKernel = new TestDependenciesRoot();
            newsService = testKernel.TryGetInstance<INewsService>();
        }
        [Test]
        public void HomeIndex()
        {          
            HomeController controller = new HomeController(newsService);
            var result = controller.Index();
            Assert.IsNotNull(result);
        }
    }
}
