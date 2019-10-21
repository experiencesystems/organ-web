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
        private ItensPlantio itensp = new ItensPlantio();
        private Area area = new Area();
        private Semente semente = new Semente();

        // GET: Sistema/Plantio
        public ActionResult Index()
        {
            var select = new ViewPlantio
            {
                Plantios = plantio.GetPlantiosIncompletos()
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
            ViewBag.Sistema = plantio.Sistemas.Where(x => x.Value == plantio.Sistema.ToString()).First().Text;
            ViewBag.Periodo = plantio.Periodos.Where(x => x.Value == plantio.TipoPlantio.ToString()).First().Text;
            return View(plantio);
        }
        
        public ActionResult Editar(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Plantio plantio)
        {
            if (plantio.DataInicio > plantio.DataColheita)
            {
                ModelState.AddModelError("", "Insira uma data de início anterior a da colheita.");
                return View(plantio);
            }
            else if (ModelState.IsValid)
            {
                plantio.Update(plantio);
                plantio.Save();
                return RedirectToAction("Index");
            }
            return View(plantio);
        }

        public ActionResult Create()
        {
            var view = new CreatePlantioViewModel
            {
                Areas = area.AreasDisponiveis(),
                Sementes = semente.GetAll(),
                Sistemas = plantio.Sistemas,
                Periodos = plantio.Periodos
            };
            ViewBag.Areas = new MultiSelectList(view.Areas, "Id", "Nome");
            ViewBag.Sementes = new SelectList(view.Sementes, "IdEstoque", "Nome");
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePlantioViewModel plantio, int[] IdArea, int Sistema, int Tipo)
        {
            if (plantio.Inicio > plantio.Colheita)
            {
                ModelState.AddModelError("", "Insira uma data de início anterior a da colheita.");
                return View(plantio);
            }
            else if (ModelState.IsValid)
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
            plantio.Areas = area.AreasDisponiveis();
            plantio.Sementes = semente.GetAll();
            ViewBag.Areas = new MultiSelectList(plantio.Areas, "Id", "Nome");
            ViewBag.Sementes = new SelectList(plantio.Sementes, "IdEstoque", "Nome");
            return View(plantio);
        }

        public ActionResult Excluir(int? id)
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
            ViewBag.Sistema = plantio.Sistemas.Where(x => x.Value == plantio.Sistema.ToString()).First().Text;
            ViewBag.Periodo = plantio.Periodos.Where(x => x.Value == plantio.TipoPlantio.ToString()).First().Text;
            return View(plantio);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(Plantio plantio)
        {
            areap.DeleteByIdPlantio(plantio);
            itensp.DeleteByIdPlantio(plantio);
            plantio = plantio.GetByID(plantio.Id);
            plantio.Delete(plantio.Id);
            plantio.Save();
            return RedirectToAction("Index");
        }
    }
}