using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Armazenamento;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class ItemController : Controller
    {
        /*// GET: Sistema/Item
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Categoria).Include(i => i.Estoque).Include(i => i.Fornecedor);
            return View();
        }

        // GET: Sistema/Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Sistema/Item/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nome");
            ViewBag.IdEstoque = new SelectList(db.Estoques, "Id", "UnidadeMedida");
            ViewBag.IdFornecedor = new SelectList(db.Sementes, "Id", "Nome");
            return View();
        }

        // POST: Sistema/Item/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,ValorUnit,IdEstoque,IdCategoria,IdFornecedor")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nome", item.IdCategoria);
            ViewBag.IdEstoque = new SelectList(db.Estoques, "Id", "UnidadeMedida", item.IdEstoque);
            ViewBag.IdFornecedor = new SelectList(db.Sementes, "Id", "Nome", item.IdFornecedor);
            return View(item);
        }

        // GET: Sistema/Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nome", item.IdCategoria);
            ViewBag.IdEstoque = new SelectList(db.Estoques, "Id", "UnidadeMedida", item.IdEstoque);
            ViewBag.IdFornecedor = new SelectList(db.Sementes, "Id", "Nome", item.IdFornecedor);
            return View(item);
        }

        // POST: Sistema/Item/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,ValorUnit,IdEstoque,IdCategoria,IdFornecedor")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nome", item.IdCategoria);
            ViewBag.IdEstoque = new SelectList(db.Estoques, "Id", "UnidadeMedida", item.IdEstoque);
            ViewBag.IdFornecedor = new SelectList(db.Sementes, "Id", "Nome", item.IdFornecedor);
            return View(item);
        }

        // GET: Sistema/Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Sistema/Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
