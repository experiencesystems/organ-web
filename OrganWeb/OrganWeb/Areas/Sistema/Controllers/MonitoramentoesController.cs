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
    public class MonitoramentoesController : Controller
    {
        private BancoContext db = new BancoContext();
        //only gerado blz tem q customizar e os caraio
        // GET: Sistema/Monitoramentoes
        public ActionResult Index()
        {
            return View(db.Monitoramentos.ToList());
        }

        // GET: Sistema/Monitoramentoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monitoramento monitoramento = db.Monitoramentos.Find(id);
            if (monitoramento == null)
            {
                return HttpNotFound();
            }
            return View(monitoramento);
        }

        // GET: Sistema/Monitoramentoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sistema/Monitoramentoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Observacao,Status,Resultado")] Monitoramento monitoramento)
        {
            if (ModelState.IsValid)
            {
                db.Monitoramentos.Add(monitoramento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(monitoramento);
        }

        // GET: Sistema/Monitoramentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monitoramento monitoramento = db.Monitoramentos.Find(id);
            if (monitoramento == null)
            {
                return HttpNotFound();
            }
            return View(monitoramento);
        }

        // POST: Sistema/Monitoramentoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Observacao,Status,Resultado")] Monitoramento monitoramento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monitoramento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(monitoramento);
        }

        // GET: Sistema/Monitoramentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monitoramento monitoramento = db.Monitoramentos.Find(id);
            if (monitoramento == null)
            {
                return HttpNotFound();
            }
            return View(monitoramento);
        }

        // POST: Sistema/Monitoramentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Monitoramento monitoramento = db.Monitoramentos.Find(id);
            db.Monitoramentos.Remove(monitoramento);
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
