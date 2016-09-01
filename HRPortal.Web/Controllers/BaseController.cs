using HRPortal.Business;
using HRPortal.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRPortal.Web.Controllers
{
    public class BaseController : Controller
    {
        ICacheProvider cacheProvider;

        public BaseController()
        {
            cacheProvider = NinjectWebCommon.TryGetInstance<ICacheProvider>();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CheckCacheObjects();
        }

        private void CheckCacheObjects()
        {
            if (cacheProvider.IsSet(CacheKeys.CategoryNames.ToString()))
                ViewBag.Categories = cacheProvider.Get(CacheKeys.CategoryNames.ToString());
            else
            {
                ITradeService tradeService = NinjectWebCommon.TryGetInstance<ITradeService>();
                cacheProvider.Set(CacheKeys.CategoryNames.ToString(), tradeService.GetAllCategories(), 360);
                ViewBag.Categories = cacheProvider.Get(CacheKeys.CategoryNames.ToString());
            }
        }
    }
}