using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class SementeController : Controller
    {
        private Semente semente = new Semente();

        public ActionResult Sementes()
        {
            var select = new ViewSementes
            {
                Semente = semente.GetFew()
            };
            return View(select);
        }


        public ActionResult Create()
            {
               
                return View();
            }

        public ActionResult Excluir() {
            return View();
        }

        public ActionResult Editar() {
            return View();
        }
        
    }
}