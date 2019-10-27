using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Safras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class ColheitaController : Controller
    {
        private Colheita colheita = new Colheita();
        private Plantio plantio = new Plantio();
        private Produto produto = new Produto();
        private Estoque estoque = new Estoque();

        public ActionResult Index()
        {
            return View(colheita.GetFew());
        }

        public ActionResult Create(int? id) //recebe o id do plantio
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
            colheita = new Colheita()
            {
                IdPlantio = plantio.Id,
                Produto = new Produto()
                {
                    Estoque = new Estoque()
                },
                Plantio = plantio
            };
            ViewBag.Sistema = colheita.Plantio.Sistemas.Where(x => x.Value == colheita.Plantio.Sistema.ToString()).First().Text;
            ViewBag.Periodo = colheita.Plantio.Periodos.Where(x => x.Value == colheita.Plantio.TipoPlantio.ToString()).First().Text;
            return View(colheita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Colheita colheita)
        {
            plantio = plantio.GetByID(colheita.IdPlantio);
            if (plantio.DataInicio > colheita.Data)
            {
                ModelState.AddModelError("", "Insira uma data de colheita posterior à de início do plantio.");
                return View(colheita);
            }
            if (ModelState.IsValid)
            {
                estoque = colheita.Produto.Estoque;
                estoque.Add(estoque);
                estoque.Save();

                produto = colheita.Produto;
                produto.IdEstoque = estoque.Id;
                produto.Estoque = null;
                estoque = null;
                produto.Add(produto);
                produto.Save();

                colheita.IdProd = produto.IdEstoque;
                colheita.Produto = null;
                produto = null;
                colheita.Add(colheita);
                colheita.Save();

                return RedirectToAction("Index");
            }
            return View(colheita);
        }

        public ActionResult Detalhes(int? id, int? id2)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            colheita = colheita.GetByID(id, id2);
            if (colheita == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sistema = colheita.Plantio.Sistemas.Where(x => x.Value == colheita.Plantio.Sistema.ToString()).First().Text;
            ViewBag.Periodo = colheita.Plantio.Periodos.Where(x => x.Value == colheita.Plantio.TipoPlantio.ToString()).First().Text;
            ViewBag.UnidadeMedida = colheita.Produto.Estoque.UnidadesDeMedida.Where(x => x.Value == colheita.Produto.Estoque.UM.ToString()).First().Text;
            return View(colheita);
        }

        public ActionResult Editar(int? id, int? id2)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            colheita = colheita.GetByID(id, id2);
            if (colheita == null)
            {
                return HttpNotFound();
            }
            return View(colheita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Colheita colheita)
        {
            if (ModelState.IsValid)
            {
                estoque = colheita.Produto.Estoque;
                estoque.Update(estoque);
                estoque.Save();

                produto = colheita.Produto;
                produto.Update(produto);
                produto.Save();

                colheita.Update(colheita);
                colheita.Save();
                return RedirectToAction("Index");
            }
            return View(colheita);
        }
    }
}