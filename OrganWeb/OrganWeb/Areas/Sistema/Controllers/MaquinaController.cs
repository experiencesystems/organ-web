using OrganWeb.Areas.Sistema.Models.Ferramentas;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class MaquinaController : Controller
    {
        private Maquina maquina = new Maquina();

        public ActionResult Index()
        {
            var select = new ViewMaquina
            {
                Maquina = maquina.GetFew()
            };
            return View(select);
        }
    }
}