using HRPortal.Business;
using HRPortal.Data;
using HRPortal.Web.App_Start.DInjection;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace HRPortal.Web.App_Start
{
    public static class NinjectWebCommon
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            // Install our Ninject-based IDependencyResolver into the Web API config
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            return kernel;
        }

        public static void RegisterServices(IKernel kernel)
        {
            // This is where we tell Ninject how to resolve service requests
            kernel.Bind<INewsService>().To<NewsService>();
            kernel.Bind<ITradeService>().To<TradeService>();
            kernel.Bind<INLogManager>().To<NLogManager>();


            ////WebData layer
            kernel.Bind<INewsRepository>().To<NewsRepository>();
            kernel.Bind<INewsCategoryRepository>().To<NewsCategoriesRepository>();
            kernel.Bind<INewsAttachmentsRepository>().To<NewsAttachmentsRepository>();
            kernel.Bind<INewsCommentsRepository>().To<NewsCommentsRepository>();
            kernel.Bind<INewsReplyRepository>().To<NewsReplyRepository>();
            kernel.Bind<ITradeItemRepository>().To<TradeItemRepository>();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
        }
    }
}