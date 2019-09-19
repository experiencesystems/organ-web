using OrganWeb.Areas.Sistema.Models;
using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.Ferramentas;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class ManutencaoController : Controller
    {
        private readonly ManutencaoMaquina manutencao = new ManutencaoMaquina();
        private readonly BancoContext db = new BancoContext();

        // GET: Sistema/Manutencao
        public ActionResult Index()
        {
            return View(manutencao.GetFew());
        }
    }
}