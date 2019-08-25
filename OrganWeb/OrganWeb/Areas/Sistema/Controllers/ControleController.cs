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
    public class ControleController : Controller
    {
        private BancoContext db = new BancoContext();

        // GET: Sistema/Controle
        public ActionResult Index()
        {
            var controles = db.Controles.Include(c => c.Estadio);
            return View(controles.ToList());
        }

        // GET: Sistema/Controle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Controle controle = db.Controles.Find(id);
            if (controle == null)
            {
                return HttpNotFound();
            }
            return View(controle);
        }

        // GET: Sistema/Controle/Create
        public ActionResult Create()
        {
            ViewBag.IdEstadio = new SelectList(db.Estadios, "Id", "Nome");
            return View();
        }

        // POST: Sistema/Controle/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status,Tipo,Descricao,Eficiencia,NumeroLiberacoes,IdEstadio")] Controle controle)
        {
            if (ModelState.IsValid)
            {
                db.Controles.Add(controle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEstadio = new SelectList(db.Estadios, "Id", "Nome", controle.IdEstadio);
            return View(controle);
        }

        // GET: Sistema/Controle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Controle controle = db.Controles.Find(id);
            if (controle == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEstadio = new SelectList(db.Estadios, "Id", "Nome", controle.IdEstadio);
            return View(controle);
        }

        // POST: Sistema/Controle/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status,Tipo,Descricao,Eficiencia,NumeroLiberacoes,IdEstadio")] Controle controle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(controle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEstadio = new SelectList(db.Estadios, "Id", "Nome", controle.IdEstadio);
            return View(controle);
        }

        // GET: Sistema/Controle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Controle controle = db.Controles.Find(id);
            if (controle == null)
            {
                return HttpNotFound();
            }
            return View(controle);
        }

        // POST: Sistema/Controle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Controle controle = db.Controles.Find(id);
            db.Controles.Remove(controle);
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
