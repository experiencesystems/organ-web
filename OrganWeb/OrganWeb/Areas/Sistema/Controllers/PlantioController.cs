using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.ViewModels;
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
            return View(await plantio.GetPlantiosIncompletos());
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
        public async Task<ActionResult> Create(CreatePlantioViewModel crplantio, int[] IdArea, int Sistema, int Tipo)
        {//TODO: mudar colocar qnt usada no plantio e densidade na area
            if (crplantio.Inicio > crplantio.Colheita)
            {
                ModelState.AddModelError(string.Empty, "Insira uma data de início anterior a da colheita.");
                crplantio.Areas = await area.AreasDisponiveis();
                crplantio.Sementes = await semente.GetAll();
                crplantio.Sistemas = plantio.Sistemas;
                crplantio.Periodos = plantio.Periodos;
                return View(crplantio);
            }
            semente = await semente.GetByID(crplantio.IdEstoque);
            if (semente.Estoque.Qtd < crplantio.Quantidade)
            {
                ModelState.AddModelError(string.Empty, "Não há sementes o suficiente no estoque para completar a operação");
                crplantio.Areas = await area.AreasDisponiveis();
                crplantio.Sementes = await semente.GetAll();
                crplantio.Sistemas = plantio.Sistemas;
                crplantio.Periodos = plantio.Periodos;
                return View(crplantio);
            }
            else if (ModelState.IsValid)
            {
                var pl = new Plantio
                {
                    Nome = crplantio.Nome,
                    DataInicio = crplantio.Inicio,
                    DataColheita = crplantio.Colheita,
                    Sistema = crplantio.Sistema,
                    TipoPlantio = crplantio.Tipo
                };
                pl.Add(pl);
                await pl.Save();

                var itemcrplantio = new ItensPlantio
                {
                   IdEstoque = crplantio.IdEstoque,
                   IdPlantio = pl.Id,
                   QtdUsada = crplantio.Quantidade
                };
                itemcrplantio.Add(itemcrplantio);
                await itemcrplantio.Save();

                return RedirectToAction("Index");
            }

            // Enviando listas da combobox caso o formulário não seja preenchido corretamente
            crplantio.Areas = await area.AreasDisponiveis();
            crplantio.Sementes = await semente.GetAll();
            crplantio.Sistemas = plantio.Sistemas;
            crplantio.Periodos = plantio.Periodos;
            return View(crplantio);
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