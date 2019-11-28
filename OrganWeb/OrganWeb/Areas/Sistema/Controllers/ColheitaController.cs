using OrganWeb.Areas.Sistema.Models.API;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    [Authorize]
    public class ColheitaController : Controller
    {
        private Colheita colheita = new Colheita();
        private VwColheita vwcolheita = new VwColheita();
        private Plantio plantio = new Plantio();
        private Produto produto = new Produto();
        private Estoque estoque = new Estoque();
        private ListarUnidades unmd = new ListarUnidades();
        private UnidadeCadastro uncd = new UnidadeCadastro();

        public async Task<ActionResult> Index()
        {
            return View(await vwcolheita.GetAll());
        }

        public async Task<ActionResult> CriarColheita(int? id, bool? again) //recebe o id do plantio
        {
            if (id == null || again == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool dnv = again ?? false;
            plantio = await plantio.GetByID(id);
            if (plantio == null)
            {
                return HttpNotFound();
            }
            var responseModel = await unmd.GetListarUnidades();
            colheita = new Colheita()
            {
                IdPlantio = plantio.Id,
                Produto = new Produto()
                {
                    Estoque = new Estoque()
                    {
                        Unidades = responseModel.UnidadeCadastros
                    }
                },
                Plantio = plantio
            };
            if (dnv)
                return View("RepeteColheita", colheita);
            else
                return View("Colheita", colheita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Colheita(Colheita colheita)
        {
            ListarUnidades responseModel = new ListarUnidades();
            plantio = await plantio.GetByID(colheita.IdPlantio);
              if (plantio.DataInicio > colheita.Data)
            {
                ModelState.AddModelError("", "Insira uma data de colheita posterior à de início do plantio.");
                responseModel = await unmd.GetListarUnidades();
                colheita.Produto.Estoque.Unidades = responseModel.UnidadeCadastros;
                colheita.Plantio = plantio;
                return View(colheita);
            }
            if (ModelState.IsValid)
            {
                responseModel = await unmd.GetListarUnidades();
                colheita.Produto.Estoque.Unidades = responseModel.UnidadeCadastros;
                if (await unmd.GetByID(colheita.Produto.Estoque.UM) == null)
                {
                    colheita.Produto.Estoque.Unidades = responseModel.UnidadeCadastros;
                    uncd = new UnidadeCadastro()
                    {
                        Id = colheita.Produto.Estoque.UM,
                        Desc = colheita.Produto.Estoque.Unidades.Where(x => x.Id == colheita.Produto.Estoque.UM).Select(x => x.Desc).FirstOrDefault().ToString()
                    };
                    unmd.Add(uncd);
                    await unmd.Save();
                }
                estoque = colheita.Produto.Estoque;
                estoque.Qtd = Convert.ToDouble(colheita.QtdTotal);
                estoque.Add(estoque);
                await estoque.Save();

                produto = colheita.Produto;
                produto.IdEstoque = estoque.Id;
                produto.Estoque = null;
                estoque = null;
                produto.Add(produto);
                await produto.Save();

                colheita.IdProd = produto.IdEstoque;
                colheita.Produto = null;
                produto = null;
                colheita.Add(colheita);
                await colheita.Save();

                plantio.Delete(plantio.Id);
                await plantio.Save();

                return RedirectToAction("Index");
            }
            responseModel = await unmd.GetListarUnidades();
            colheita.Produto.Estoque.Unidades = responseModel.UnidadeCadastros;
            colheita.Plantio = plantio;
            return View(colheita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RepeteColheita(Colheita colheita)
        {
            ListarUnidades responseModel = new ListarUnidades();
            plantio = await plantio.GetByID(colheita.IdPlantio);
            if (plantio.DataInicio > colheita.Data || plantio.DataColheita < colheita.Data)
            {
                ModelState.AddModelError("", "Insira uma data de colheita posterior à de início do plantio.");
                responseModel = await unmd.GetListarUnidades();
                colheita.Produto.Estoque.Unidades = responseModel.UnidadeCadastros;
                colheita.Plantio = plantio;
                return View(colheita);
            }
            if (colheita.QtdPerdas > colheita.QtdTotal)
            {
                ModelState.AddModelError("", "A quantidade de perdas deve ser menor ou igual a quantidade total de itens colhidos.");
                responseModel = await unmd.GetListarUnidades();
                colheita.Produto.Estoque.Unidades = responseModel.UnidadeCadastros;
                colheita.Plantio = plantio;
                return View(colheita);
            }
            if (ModelState.IsValid)
            {
                responseModel = await unmd.GetListarUnidades();
                colheita.Produto.Estoque.Unidades = responseModel.UnidadeCadastros;
                if (await unmd.GetByID(colheita.Produto.Estoque.UM) == null)
                {
                    colheita.Produto.Estoque.Unidades = responseModel.UnidadeCadastros;
                    uncd = new UnidadeCadastro()
                    {
                        Id = colheita.Produto.Estoque.UM,
                        Desc = colheita.Produto.Estoque.Unidades.Where(x => x.Id == colheita.Produto.Estoque.UM).Select(x => x.Desc).FirstOrDefault().ToString()
                    };
                    unmd.Add(uncd);
                    await unmd.Save();
                }
                plantio.DataColheita = colheita.Plantio.DataColheita;
                plantio.DataInicio = colheita.Data;
                plantio.Update(plantio);
                await plantio.Save();

                estoque = colheita.Produto.Estoque;
                estoque.Qtd = Convert.ToDouble(colheita.QtdTotal);
                estoque.Add(estoque);
                await estoque.Save();

                produto = colheita.Produto;
                produto.IdEstoque = estoque.Id;
                produto.Estoque = null;
                estoque = null;
                produto.Add(produto);
                await produto.Save();

                colheita.IdProd = produto.IdEstoque;
                colheita.Produto = null;
                produto = null;
                colheita.Add(colheita);
                await colheita.Save();

                return RedirectToAction("Index");
            }
            responseModel = await unmd.GetListarUnidades();
            colheita.Produto.Estoque.Unidades = responseModel.UnidadeCadastros;
            colheita.Plantio = plantio;
            return View(colheita);
        }
    }
}