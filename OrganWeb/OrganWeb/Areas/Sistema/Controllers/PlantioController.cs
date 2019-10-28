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
using System.Threading.Tasks;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class PlantioController : Controller
    {
        private Plantio plantio = new Plantio();
        private AreaPlantio areap = new AreaPlantio();
        private ItensPlantio itensp = new ItensPlantio();
        private Area area = new Area();
        private Semente semente = new Semente();
        
        public async Task<ActionResult> Index()
        {
            var select = new ViewPlantio
            {
                Plantios = await plantio.GetPlantiosIncompletos()
            };
            return View(select);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plantio = await plantio.GetByID(id);
            if (plantio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sistema = plantio.Sistemas.Where(x => x.Value == plantio.Sistema.ToString()).First().Text;
            ViewBag.Periodo = plantio.Periodos.Where(x => x.Value == plantio.TipoPlantio.ToString()).First().Text;
            return View(plantio);
        }
        
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plantio = await plantio.GetByID(id);
            if (plantio == null)
            {
                return HttpNotFound();
            }
            return View(plantio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Plantio plantio)
        {
            if (plantio.DataInicio > plantio.DataColheita)
            {
                ModelState.AddModelError("", "Insira uma data de início anterior a da colheita.");
                return View(plantio);
            }
            else if (ModelState.IsValid)
            {
                plantio.Update(plantio);
                await plantio.Save();
                return RedirectToAction("Index");
            }
            return View(plantio);
        }

        public async Task<ActionResult> Create()
        {
            var view = new CreatePlantioViewModel
            {
                Areas = await area.AreasDisponiveis(),
                Sementes = await semente.GetAll(),
                Sistemas = plantio.Sistemas,
                Periodos = plantio.Periodos
            };
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePlantioViewModel plantio, int[] IdArea, int Sistema, int Tipo)
        {
            if (plantio.Inicio > plantio.Colheita)
            {
                ModelState.AddModelError("", "Insira uma data de início anterior a da colheita.");
                return View(plantio);
            }
            semente = await semente.GetByID(plantio.IdEstoque);
            if (semente.Estoque.Qtd < plantio.Quantidade)
            {
                ModelState.AddModelError("", "Não há sementes o suficiente no estoque para completar a operação");
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
                await pl.Save();

                foreach (var item in IdArea)
                {
                   areap.Add(new AreaPlantio { IdArea = item, IdPlantio = pl.Id, Densidade = plantio.Densidade });
                   await areap.Save();
                }

                var itemplantio = new ItensPlantio
                {
                   IdEstoque = plantio.IdEstoque,
                   IdPlantio = pl.Id,
                   QtdUsada = plantio.Quantidade
                };
                itemplantio.Add(itemplantio);
                await itemplantio.Save();

                return RedirectToAction("Index");
            }

            // Enviando listas da combobox caso o formulário não seja preenchido corretamente
            plantio.Areas = await area.AreasDisponiveis();
            plantio.Sementes = await semente.GetAll();
            return View(plantio);
        }

        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plantio = await plantio.GetByID(id);
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
        public async Task<ActionResult> ExcluirConfirmado(Plantio plantio)
        {
            areap.DeleteByIdPlantio(plantio);
            itensp.DeleteByIdPlantio(plantio);
            plantio = await plantio.GetByID(plantio.Id);
            plantio.Delete(plantio.Id);
            await plantio.Save();
            return RedirectToAction("Index");
        }
    }
}