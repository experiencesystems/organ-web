using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.ViewModels;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class PlantioController : Controller
    {
        private Plantio plantio = new Plantio();
        private BancoContext db = new BancoContext();

        // GET: Sistema/Plantio
        public ActionResult Index()
        {
            var select = new ViewPlantio
            {
                Plantios = plantio.GetPlantios()
            };
            return View(select);
        }
        
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plantio = plantio.GetByID(id);
            if (plantio == null)
            {
                return HttpNotFound();
            }
            return View(plantio);
        }

        public PartialViewResult _NovoPlantio()
        {
            Plantio plantio = new Plantio
            {
                //Semente = semente.GetAll()   
            };

            return PartialView("_NovoItem", plantio);
        }
    }
}