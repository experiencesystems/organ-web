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
    public class FazendaController : Controller
    {
        private BancoContext db = new BancoContext();

        // GET: Sistema/Fazenda
        public ActionResult Index()
        {
            var fazendas = db.Fazendas.Include(f => f.Localizacao);
            return View(fazendas.ToList());
        }

        // GET: Sistema/Fazenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fazenda fazenda = db.Fazendas.Find(id);
            if (fazenda == null)
            {
                return HttpNotFound();
            }
            return View(fazenda);
        }

        // GET: Sistema/Fazenda/Create
        public ActionResult Create()
        {
            ViewBag.Numero = new SelectList(db.Localizacaos, "Numero", "Endereco");
            return View();
        }

        // POST: Sistema/Fazenda/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Area,Perimetro,Numero,Coordenadas")] Fazenda fazenda)
        {
            if (ModelState.IsValid)
            {
                db.Fazendas.Add(fazenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Numero = new SelectList(db.Localizacaos, "Numero", "Endereco", fazenda.Numero);
            return View(fazenda);
        }

        // GET: Sistema/Fazenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fazenda fazenda = db.Fazendas.Find(id);
            if (fazenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.Numero = new SelectList(db.Localizacaos, "Numero", "Endereco", fazenda.Numero);
            return View(fazenda);
        }

        // POST: Sistema/Fazenda/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Area,Perimetro,Numero,Coordenadas")] Fazenda fazenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fazenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Numero = new SelectList(db.Localizacaos, "Numero", "Endereco", fazenda.Numero);
            return View(fazenda);
        }

        // GET: Sistema/Fazenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fazenda fazenda = db.Fazendas.Find(id);
            if (fazenda == null)
            {
                return HttpNotFound();
            }
            return View(fazenda);
        }

        // POST: Sistema/Fazenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fazenda fazenda = db.Fazendas.Find(id);
            db.Fazendas.Remove(fazenda);
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
