using HRPortal.Business;
using HRPortal.Model.ViewModels;
using System.Web.Mvc;

namespace HRPortal.Web.Controllers
{
    public class HomeController : BaseController
    {
        INewsService newsService;

        public HomeController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public ActionResult Index()
        {
            return View(new NewsViewModel
            {
                NewsCategories = newsService.GetNewsCategories()
            });
        }
    }
}