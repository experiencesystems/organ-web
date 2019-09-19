using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.Controles;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class DoencasController : Controller
    {
        private BancoContext db = new BancoContext();


        //ta so gerado
        // GET: Sistema/Doencas
        public ActionResult Index()
        {
            return View(db.Doencas.ToList());
        }

        // GET: Sistema/Doencas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doenca doenca = db.Doencas.Find(id);
            if (doenca == null)
            {
                return HttpNotFound();
            }
            return View(doenca);
        }

        // GET: Sistema/Doencas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sistema/Doencas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status,Nome,Sintomas,Tratamento,Descricao")] Doenca doenca)
        {
            if (ModelState.IsValid)
            {
                db.Doencas.Add(doenca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doenca);
        }

        // GET: Sistema/Doencas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doenca doenca = db.Doencas.Find(id);
            if (doenca == null)
            {
                return HttpNotFound();
            }
            return View(doenca);
        }

        // POST: Sistema/Doencas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status,Nome,Sintomas,Tratamento,Descricao")] Doenca doenca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doenca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doenca);
        }

        // GET: Sistema/Doencas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doenca doenca = db.Doencas.Find(id);
            if (doenca == null)
            {
                return HttpNotFound();
            }
            return View(doenca);
        }

        // POST: Sistema/Doencas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doenca doenca = db.Doencas.Find(id);
            db.Doencas.Remove(doenca);
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
