using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Web;
using OrganWeb.Areas.Sistema.Models;
using System.Web.Mvc;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Models.Banco;
using System.Net;
using PagedList.EntityFramework;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using System.Threading.Tasks;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class EstoqueController : Controller
    {
        //TODO: categorias máquina
        private Insumo insumo = new Insumo();
        private Estoque estoque = new Estoque();
        private Categoria categoria = new Categoria();
        private VwItems vwItems = new VwItems();
        private VwFornecedor vwFornecedor = new VwFornecedor();
        private HistoricoEstoque historicoEstoque = new HistoricoEstoque();
        private ViewEstoque viewestoque = new ViewEstoque();

        //https://stackoverflow.com/questions/25125329/using-a-pagedlist-with-a-viewmodel-asp-net-mvc

        [HttpGet]
        public async Task<ViewResult> Index(string filtros, string textoPesquisa, int? page)
        {
            int pagina = (page ?? 1);
            var listaFiltros = new List<string>
            {
                "Insumo",
                "Máquina",
                "Produto",
                "Semente"
            };

            ViewBag.filtros = new SelectList(listaFiltros);

            if (textoPesquisa != null)
            {
                page = 1;
            }
            else
            {
                textoPesquisa = filtros;
            }

            var itens = await vwItems.GetAll();

            viewestoque = new ViewEstoque()
            {
                VwItems = await vwItems.GetPagedAll(pagina),
                HistoricoEstoques = await historicoEstoque.GetAll(),
                Fornecedors = await vwFornecedor.GetAll()
            };

            if (!String.IsNullOrEmpty(textoPesquisa))
            {
                viewestoque.VwItems = itens.Where(s => s.Item.IndexOf(textoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0 || s.Tipo.IndexOf(textoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0 || s.Categoria.IndexOf(textoPesquisa, StringComparison.OrdinalIgnoreCase) >= 0).ToPagedList(pagina, 5);
            }

            if (!string.IsNullOrEmpty(filtros))
            {
                viewestoque.VwItems = itens.Where(x => x.Tipo == filtros).ToPagedList(pagina, 5);
            }

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
    }
}