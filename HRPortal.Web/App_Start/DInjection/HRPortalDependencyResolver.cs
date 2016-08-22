using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRPortal.Web.App_Start.DInjection
{
    public class HRPortalDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private IKernel kernel;

        public HRPortalDependencyResolver()
        {
            kernel = new StandardKernel();
            NinjectWebCommon.RegisterServices(kernel);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType, new IParameter[0]);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType, new IParameter[0]);
        }
    }
}