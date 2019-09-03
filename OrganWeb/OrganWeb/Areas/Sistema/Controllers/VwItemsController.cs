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
    public class VwItemsController : Controller
    {
        private BancoContext db = new BancoContext();

        // GET: Sistema/VwItems
        public ActionResult Index()
        {
            return View(db.VwItems.ToList());
        }

        // GET: Sistema/VwItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VwItems vwItems = db.VwItems.Find(id);
            if (vwItems == null)
            {
                return HttpNotFound();
            }
            return View(vwItems);
        }

        // GET: Sistema/VwItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sistema/VwItems/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Item,Quantidade,UnidadeMedida,Categoria")] VwItems vwItems)
        {
            if (ModelState.IsValid)
            {
                db.VwItems.Add(vwItems);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vwItems);
        }

        // GET: Sistema/VwItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VwItems vwItems = db.VwItems.Find(id);
            if (vwItems == null)
            {
                return HttpNotFound();
            }
            return View(vwItems);
        }

        // POST: Sistema/VwItems/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Item,Quantidade,UnidadeMedida,Categoria")] VwItems vwItems)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vwItems).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vwItems);
        }

        // GET: Sistema/VwItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VwItems vwItems = db.VwItems.Find(id);
            if (vwItems == null)
            {
                return HttpNotFound();
            }
            return View(vwItems);
        }

        // POST: Sistema/VwItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VwItems vwItems = db.VwItems.Find(id);
            db.VwItems.Remove(vwItems);
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
