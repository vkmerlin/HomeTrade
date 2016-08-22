using HRPortal.Business;
using HRPortal.Data;
using HRPortal.Web.NUnitTests.MockDInjections;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Web.NUnitTests.Services
{
    public class NewsServiceTests
    {
        INewsRepository newsDao;
        INewsCategoryRepository newsCatsDao;
        public NewsServiceTests()
        {
            var testKernel = new TestDependenciesRoot();
            newsDao = testKernel.TryGetInstance<INewsRepository>();
            newsCatsDao = testKernel.TryGetInstance<INewsCategoryRepository>();
        }

        [Test]
        public void NewsCategoriesGetAll()
        {
            ServiceFactory factory = new ServiceFactory();
            // Arrange
            NewsService service = factory.NewsService;

            // Act
            var result = service.GetNewsCategories();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}
