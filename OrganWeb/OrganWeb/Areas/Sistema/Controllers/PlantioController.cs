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
using OrganWeb.Areas.Sistema.Models.Funcionarios;

namespace OrganWeb.Areas.Sistema.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PlantioController : Controller
    {
        private Plantio plantio = new Plantio();
        private AreaPlantio areap = new AreaPlantio();
        private ItensPlantio itensp = new ItensPlantio();
        private Area area = new Area();
        private Semente semente = new Semente();
        
        [HttpGet]
        public async Task<ActionResult> Index(string filtros)
        {
            var listaFiltros = new List<string>
            {
                "Todos",
                "Ativos",
                "Finalizados"
            };
            ViewBag.filtros = new SelectList(listaFiltros);
            if (!String.IsNullOrWhiteSpace(filtros))
            {
                switch (filtros)
                {
                    case "Finalizados":
                        return View(await plantio.GetPlantiosFinalizados());
                    case "Todos":
                        return View(await plantio.GetPlantios());
                    default:
                        return View(await plantio.GetPlantiosAtivos());
                }                
            }
            return View(await plantio.GetPlantiosAtivos());
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
                Periodos = plantio.Periodos,
                Funcionarios = await new Funcionario().GetAll()
            };
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePlantioViewModel crplantio, int[] IdArea, int Sistema, int Tipo, int[] IdFunc)
        {
            if (crplantio.Inicio > crplantio.Colheita)
            {
                ModelState.AddModelError(string.Empty, "Insira uma data de início anterior a da colheita.");
                crplantio.Areas = await area.AreasDisponiveis();
                crplantio.Sementes = await semente.GetAll();
                crplantio.Sistemas = plantio.Sistemas;
                crplantio.Periodos = plantio.Periodos;
                crplantio.Funcionarios = await new Funcionario().GetAll();
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
                crplantio.Funcionarios = await new Funcionario().GetAll();
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

                foreach (var area in IdArea)
                {
                    var areap = new AreaPlantio
                    {
                        IdArea = area,
                        Densidade = 1,
                        IdPlantio = pl.Id
                    };
                    areap.Add(areap);
                    await areap.Save();
                }

                foreach (var func in IdFunc)
                {
                    var funcp = new FuncPlantio
                    {
                        IdFunc = func,
                        IdPlantio = pl.Id
                    };
                    funcp.Add(funcp);
                    await funcp.Save();
                }

                return RedirectToAction("Index");
            }

            // Enviando listas da combobox caso o formulário não seja preenchido corretamente
            crplantio.Areas = await area.AreasDisponiveis();
            crplantio.Sementes = await semente.GetAll();
            crplantio.Sistemas = plantio.Sistemas;
            crplantio.Periodos = plantio.Periodos;
            crplantio.Funcionarios = await new Funcionario().GetAll();
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