using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Ecommerce/Home
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Loja");
        }
    }
}