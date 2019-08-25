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
    public class MaquinaController : Controller
    {
        private BancoContext db = new BancoContext();

        // GET: Sistema/Maquina
        public ActionResult Index()
        {
            var maquinas = db.Maquinas.Include(m => m.Fornecedor);
            return View(maquinas.ToList());
        }

        // GET: Sistema/Maquina/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquina maquina = db.Maquinas.Find(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            return View(maquina);
        }

        // GET: Sistema/Maquina/Create
        public ActionResult Create()
        {
            ViewBag.Montadora = new SelectList(db.Fornecedors, "Id", "Nome");
            return View();
        }

        // POST: Sistema/Maquina/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,DataCadastro,ValorCadastro,VidaUtil,DepreciacaoAno,DepreciacaoMes,Montadora")] Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                db.Maquinas.Add(maquina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Montadora = new SelectList(db.Fornecedors, "Id", "Nome", maquina.Montadora);
            return View(maquina);
        }

        // GET: Sistema/Maquina/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquina maquina = db.Maquinas.Find(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            ViewBag.Montadora = new SelectList(db.Fornecedors, "Id", "Nome", maquina.Montadora);
            return View(maquina);
        }

        // POST: Sistema/Maquina/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,DataCadastro,ValorCadastro,VidaUtil,DepreciacaoAno,DepreciacaoMes,Montadora")] Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maquina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Montadora = new SelectList(db.Fornecedors, "Id", "Nome", maquina.Montadora);
            return View(maquina);
        }

        // GET: Sistema/Maquina/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maquina maquina = db.Maquinas.Find(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            return View(maquina);
        }

        // POST: Sistema/Maquina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maquina maquina = db.Maquinas.Find(id);
            db.Maquinas.Remove(maquina);
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
