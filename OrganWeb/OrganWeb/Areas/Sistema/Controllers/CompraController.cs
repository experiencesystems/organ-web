using OrganWeb.Areas.Sistema.Models.Financas;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class CompraController : Controller
    {
        private BancoContext db = new BancoContext();
        // GET: Sistema/Compra
        public ActionResult Index()
        {
            return Redirect("~/Sistema/Financeiro/Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Compra compra)
        {
            var query = db.Database.SqlQuery<Compra>(
                "EXEC [dbo].[GetFunctionByID] @p1",
                new SqlParameter("p1", 200));

            var result = query.ToList();
            return View(compra);
        }
    }
}