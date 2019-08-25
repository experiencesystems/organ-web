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
    public class PragaController : Controller
    {
        private BancoContext db = new BancoContext();

        // GET: Sistema/Praga
        public ActionResult Index()
        {
            return View(db.Pragas.ToList());
        }

        // GET: Sistema/Praga/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Praga praga = db.Pragas.Find(id);
            if (praga == null)
            {
                return HttpNotFound();
            }
            return View(praga);
        }

        // GET: Sistema/Praga/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sistema/Praga/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomePopular,NomeCientifico,Descricao")] Praga praga)
        {
            if (ModelState.IsValid)
            {
                db.Pragas.Add(praga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(praga);
        }

        // GET: Sistema/Praga/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Praga praga = db.Pragas.Find(id);
            if (praga == null)
            {
                return HttpNotFound();
            }
            return View(praga);
        }

        // POST: Sistema/Praga/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomePopular,NomeCientifico,Descricao")] Praga praga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(praga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(praga);
        }

        // GET: Sistema/Praga/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Praga praga = db.Pragas.Find(id);
            if (praga == null)
            {
                return HttpNotFound();
            }
            return View(praga);
        }

        // POST: Sistema/Praga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Praga praga = db.Pragas.Find(id);
            db.Pragas.Remove(praga);
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
