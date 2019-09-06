using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models;
using System.Web.Mvc;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Controllers
{
    [Authorize]
    public class EstoqueController : Controller
    {
        private BancoContext db = new BancoContext();

        // GET: Sistema/Estoque
        public ActionResult Index()
        {
            var estoque = new ViewEstoque
            {
                VwItems = db.VwItems.ToList()
            };

            return View(estoque);
        }
    }
}