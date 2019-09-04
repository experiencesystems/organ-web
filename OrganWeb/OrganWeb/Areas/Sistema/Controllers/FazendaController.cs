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
        //[Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            //https://stackoverflow.com/questions/11241341/mvc-entity-framework-select-data-from-multiple-models
            //https://stackoverflow.com/questions/37298612/display-data-for-two-tables-to-layout-page-mvc-5

            var fazenda = new ViewFazenda
            {
                Funcionario = db.Funcionarios
                    .Include(f => f.Cargo)
                    .ToList()
            };

            //var fazendas = db.Fazendas.Include(f => f.Localizacao).ToList();

            return View(fazenda);
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
            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco");
            return View();
        }

        // POST: Sistema/Fazenda/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Area,Perimetro,Coordenadas,CEP,Numero")] Fazenda fazenda)
        {
            if (ModelState.IsValid)
            {
                db.Fazendas.Add(fazenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco", fazenda.CEP);
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
            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco", fazenda.CEP);
            return View(fazenda);
        }

        // POST: Sistema/Fazenda/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Area,Perimetro,Coordenadas,CEP,Numero")] Fazenda fazenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fazenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco", fazenda.CEP);
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
