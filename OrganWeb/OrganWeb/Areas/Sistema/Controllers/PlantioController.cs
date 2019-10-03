using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Models.Banco;
using OrganWeb.Areas.Sistema.Models.Administrativo;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class PlantioController : Controller
    {
        private Plantio plantio = new Plantio();
        private AreaPlantio areap = new AreaPlantio();
        private Area area = new Area();
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

        public ActionResult Create()
        {
            var view = new AreaPlantio
            {
                Areas = db.Areas.Where(a => a.Disp == 1).ToList()  
            };
            ViewBag.Areas = new MultiSelectList(view.Areas, "Id", "Nome");
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Plantio plantio)
        {
            if (ModelState.IsValid)
            {
                var areass = new AreaPlantio
                {
                    IdArea = Guid.NewGuid()
                };

                plantio.Add(plantio);
                plantio.Save();
                return RedirectToAction("Index");
            }
            plantio.Areas = area.GetAll();
            return View(plantio);
        }
    }
}