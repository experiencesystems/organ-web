using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models;
using System.Web.Mvc;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Models.Banco;
using System.Net;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using System.Threading.Tasks;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class EstoqueController : Controller
    { 
        //TODO: categorias máquina
        //TODO: view histórico estoque
        private Insumo insumo = new Insumo();
        private Estoque estoque = new Estoque();
        private Categoria categoria = new Categoria();
        private VwItems vwItems = new VwItems();
        private VwFornecedor vwFornecedor = new VwFornecedor();
        private HistoricoEstoque historicoEstoque = new HistoricoEstoque();
        private ViewEstoque viewestoque = new ViewEstoque();

        public async Task<ActionResult> Index()
        {
            viewestoque = new ViewEstoque()
            {
                VwItems = await vwItems.GetFew(),
                HistoricoEstoques = await historicoEstoque.GetFew(),
                Fornecedors = await vwFornecedor.GetAll()
            };
            return View(viewestoque);
        }

        public async Task<ActionResult> Create()
        {
            insumo = new Insumo()
            {
                Estoque = new Estoque(),
                Categorias = await categoria.GetAll()
            };
            return View(insumo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                estoque = insumo.Estoque;
                estoque.Add(estoque);
                await estoque.Save();

                insumo.IdEstoque = estoque.Id;
                insumo.Estoque = null;
                estoque = null;
                insumo.Add(insumo);
                await insumo.Save();

                return RedirectToAction("Index");
            }
            insumo.Categorias = await categoria.GetAll();
            ViewBag.UnidadeMedida = insumo.Estoque.UnidadesDeMedida.Where(x => x.Value == insumo.Estoque.UM.ToString()).First().Text;
            return View(insumo);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insumo = await insumo.GetByID(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnidadeMedida = insumo.Estoque.UnidadesDeMedida.Where(x => x.Value == insumo.Estoque.UM.ToString()).First().Text;
            return View(insumo);
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insumo = await insumo.GetByID(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            insumo.Categorias = await categoria.GetAll();
            ViewBag.UnidadeMedida = insumo.Estoque.UnidadesDeMedida.Where(x => x.Value == insumo.Estoque.UM.ToString()).First().Text;
            return View(insumo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                estoque = await estoque.GetByID(insumo.IdEstoque);
                estoque.Update(estoque);
                await estoque.Save();

                insumo.Estoque = null;
                estoque = null;
                insumo.Update(insumo);
                await insumo.Save();

                return RedirectToAction("Index");
            }
            insumo.Categorias = await categoria.GetAll();
            ViewBag.UnidadeMedida = insumo.Estoque.UnidadesDeMedida.Where(x => x.Value == insumo.Estoque.UM.ToString()).First().Text;
            return View(insumo);
        }

        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insumo = await insumo.GetByID(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnidadeMedida = insumo.Estoque.UnidadesDeMedida.Where(x => x.Value == insumo.Estoque.UM.ToString()).First().Text;
            return View(insumo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(Insumo insumo)
        {
            insumo.Delete(insumo.IdEstoque);
            await insumo.Save(); //TODO: excluir da tabela estoque conflito itenscontrole
            return RedirectToAction("Index");
        }
    }
}