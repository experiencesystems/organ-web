using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class SementeController : Controller
    {
        private dborganwebEntities db = new dborganwebEntities();

        // GET: Sistema/Semente
        public ActionResult Index()
        {
            return View(db.Sementes.ToList());
        }

        // GET: Sistema/Semente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbsemente tbsemente = db.Sementes.Find(id);
            if (tbsemente == null)
            {
                return HttpNotFound();
            }
            return View(tbsemente);
        }

        // GET: Sistema/Semente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sistema/Semente/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NOME,DESCRICAO")] tbsemente tbsemente)
        {
            if (ModelState.IsValid)
            {
                db.Sementes.Add(tbsemente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbsemente);
        }

        // GET: Sistema/Semente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbsemente tbsemente = db.Sementes.Find(id);
            if (tbsemente == null)
            {
                return HttpNotFound();
            }
            return View(tbsemente);
        }

        // POST: Sistema/Semente/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NOME,DESCRICAO")] tbsemente tbsemente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbsemente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbsemente);
        }

        // GET: Sistema/Semente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbsemente tbsemente = db.Sementes.Find(id);
            if (tbsemente == null)
            {
                return HttpNotFound();
            }
            return View(tbsemente);
        }

        // POST: Sistema/Semente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbsemente tbsemente = db.Sementes.Find(id);
            db.Sementes.Remove(tbsemente);
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
