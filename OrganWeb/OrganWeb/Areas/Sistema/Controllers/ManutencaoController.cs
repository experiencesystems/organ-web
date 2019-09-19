using OrganWeb.Areas.Sistema.Models;
using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.Ferramentas;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using System.Net;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class ManutencaoController : Controller
    {
        private  ManutencaoMaquina manumaq = new ManutencaoMaquina();
        private readonly BancoContext db = new BancoContext();

        // GET: Sistema/Manutencao
        public ActionResult Index()
        {
            var select = new ViewManutencao
            {
                ManutencaoMaquinas = manumaq.GetFew()
            };
            return View(select);
        }

        public ActionResult Detalhes(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manumaq = manumaq.GetByID(id, id2);
            if (manumaq == null)
            {
                return HttpNotFound();
            }
            return View(manumaq);
        }
    }
}