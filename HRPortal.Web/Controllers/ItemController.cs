using HRPortal.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRPortal.Web.Controllers
{
    public class ItemController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category(int id)
        {
            return View();
        }
        
    }
}