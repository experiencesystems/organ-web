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
        private Semente semente = new Semente();
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

        private List<SelectListItem> sistemas = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Convencional", Value = "1" },
            new SelectListItem() { Text = "Mínimo", Value = "2" },
            new SelectListItem() { Text = "Plantio direto", Value = "3" },
            new SelectListItem() { Text = "Sobre-semeadura", Value = "4" }
            };

        private List<SelectListItem> periodo = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Safra", Value = "1" },
            new SelectListItem() { Text = "Entressafra (safrinha)", Value = "2" }
            };
        
        public ActionResult Create()
        {
            var view = new CreatePlantioViewModel
            {
                Areas = db.Areas.Where(a => a.Disp == 1).ToList(),  //TODO: Repositório Areas
                Sementes = semente.GetAll(),
                Sistemas = sistemas,
                Periodos = periodo
            };
            ViewBag.Areas = new MultiSelectList(view.Areas, "Id", "Nome");
            ViewBag.Sementes = new SelectList(view.Sementes, "IdEstoque", "Nome");
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePlantioViewModel plantio, int[] IdArea, int Sistema, int Tipo)
        {
            if (ModelState.IsValid)
            {
                var pl = new Plantio
                {
                    Nome = plantio.Nome,
                    DataInicio = plantio.Inicio,
                    DataColheita = plantio.Colheita,
                    Sistema = plantio.Sistema,
                    TipoPlantio = plantio.Tipo
                };
                pl.Add(pl);
                pl.Save();

                foreach (var item in IdArea)
                {
                   areap.Add(new AreaPlantio { IdArea = item, IdPlantio = pl.Id, Densidade = plantio.Densidade });
                   areap.Save();
                }

                var itemplantio = new ItensPlantio
                {
                   IdEstoque = plantio.IdEstoque,
                   IdPlantio = pl.Id,
                   QtdUsada = plantio.Quantidade
                };
                itemplantio.Add(itemplantio);
                itemplantio.Save();

                return RedirectToAction("Index");
            }

            // Enviando listas da combobox caso o formulário não seja preenchido corretamente
            plantio.Areas = db.Areas.Where(a => a.Disp == 1).ToList();
            plantio.Sementes = semente.GetAll();
            plantio.Sistemas = sistemas;
            plantio.Periodos = periodo;
            ViewBag.Areas = new MultiSelectList(plantio.Areas, "Id", "Nome");
            ViewBag.Sementes = new SelectList(plantio.Sementes, "IdEstoque", "Nome");
            return View(plantio);
        }
    }
}