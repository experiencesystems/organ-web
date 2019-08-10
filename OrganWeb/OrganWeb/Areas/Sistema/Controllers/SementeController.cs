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

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class SementeController : Controller
    {
        private BancoContext db = new BancoContext();

        // GET: Sistema/Semente
        public ActionResult Index()
        {
            var sementes = db.Sementes.Include(s => s.Categoria);
            return View(sementes.ToList());
        }

        // GET: Sistema/Semente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semente semente = db.Sementes.Find(id);
            if (semente == null)
            {
                return HttpNotFound();
            }
            return View(semente);
        }

        // GET: Sistema/Semente/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome");
            return View();
        }

        // POST: Sistema/Semente/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SementeID,Nome,Descricao,CategoriaID")] Semente semente)
        {
            if (ModelState.IsValid)
            {
                db.Sementes.Add(semente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", semente.CategoriaID);
            return View(semente);
        }

        // GET: Sistema/Semente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semente semente = db.Sementes.Find(id);
            if (semente == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", semente.CategoriaID);
            return View(semente);
        }

        // POST: Sistema/Semente/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SementeID,Nome,Descricao,CategoriaID")] Semente semente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", semente.CategoriaID);
            return View(semente);
        }

        // GET: Sistema/Semente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semente semente = db.Sementes.Find(id);
            if (semente == null)
            {
                return HttpNotFound();
            }
            return View(semente);
        }

        // POST: Sistema/Semente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Semente semente = db.Sementes.Find(id);
            db.Sementes.Remove(semente);
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
        }
    }
}
