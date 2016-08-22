using HRPortal.Business;
using HRPortal.Data;
using HRPortal.Web.NUnitTests.MockData;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Web.NUnitTests.MockDInjections
{
    public class TestDependenciesRoot
    {
        public IKernel _kernel;

        public TestDependenciesRoot()
        {
            _kernel = new StandardKernel();

            ////Business Layer
            _kernel.Bind<INLogManager>().To<MockNLogManager>();
            _kernel.Bind<INewsService>().To<NewsService>();

            ////Mock Data layer
            _kernel.Bind<INewsRepository>().To<MockNewsRepository>();
            _kernel.Bind<INewsCategoryRepository>().To<MockNewsCategoriesRepository>();
            //_kernel.Bind<IChartWebDao>().To<XigniteDao>()
            //    .WithConstructorArgument("token", c => _kernel.TryGet<AppConfig>().XigniteToken);

        }

        public T TryGetInstance<T>()
        {
            return _kernel.TryGet<T>();
        }
    }
}
