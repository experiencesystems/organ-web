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

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class EstoqueController : Controller
    {   //TODO: arrumar classes bridge
        //TODO: categorias máquina
        //TODO: histórico estoque
        private Insumo insumo = new Insumo();
        private Estoque estoque = new Estoque();
        private Categoria categoria = new Categoria();
        private VwItems vwItems = new VwItems();
        private ViewEstoque viewestoque = new ViewEstoque();

        public ActionResult Index()
        {
            viewestoque = new ViewEstoque()
            {
                VwItems = vwItems.GetFew()
            };
            return View(viewestoque);
        }

        public ActionResult Create()
        {
            insumo = new Insumo()
            {
                Estoque = new Estoque(),
                Categorias = categoria.GetAll()
            };
            return View(insumo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                estoque = insumo.Estoque;
                estoque.Add(estoque);
                estoque.Save();

                insumo.IdEstoque = estoque.Id;
                insumo.Estoque = null;
                estoque = null;
                insumo.Add(insumo);
                insumo.Save();

                return RedirectToAction("Index");
            }
            insumo.Categorias = categoria.GetAll();
            ViewBag.UnidadeMedida = insumo.Estoque.UnidadesDeMedida.Where(x => x.Value == insumo.Estoque.UM.ToString()).First().Text;
            return View(insumo);
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insumo = insumo.GetByID(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnidadeMedida = insumo.Estoque.UnidadesDeMedida.Where(x => x.Value == insumo.Estoque.UM.ToString()).First().Text;
            return View(insumo);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insumo = insumo.GetByID(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            insumo.Categorias = categoria.GetAll();
            ViewBag.UnidadeMedida = insumo.Estoque.UnidadesDeMedida.Where(x => x.Value == insumo.Estoque.UM.ToString()).First().Text;
            return View(insumo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                estoque = estoque.GetByID(insumo.IdEstoque);
                estoque.Update(estoque);
                estoque.Save();

                insumo.Estoque = null;
                estoque = null;
                insumo.Update(insumo);
                insumo.Save();

                return RedirectToAction("Index");
            }
            insumo.Categorias = categoria.GetAll();
            ViewBag.UnidadeMedida = insumo.Estoque.UnidadesDeMedida.Where(x => x.Value == insumo.Estoque.UM.ToString()).First().Text;
            return View(insumo);
        }

        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            insumo = insumo.GetByID(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnidadeMedida = insumo.Estoque.UnidadesDeMedida.Where(x => x.Value == insumo.Estoque.UM.ToString()).First().Text;
            return View(insumo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(Insumo insumo)
        {
            insumo.Delete(insumo.IdEstoque);
            insumo.Save(); //TODO: excluir da tabela estoque conflito itenscontrole
            return RedirectToAction("Index");
        }
    }
}