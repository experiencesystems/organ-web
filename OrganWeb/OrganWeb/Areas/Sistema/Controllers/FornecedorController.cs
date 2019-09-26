using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Estoque;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FornecedorController : Controller
    {
        private Fornecedor fornec = new Fornecedor();
        private BancoContext db = new BancoContext();

        // GET: Sistema/Fornecedor
        public ActionResult Index()
        {
            var fornecedors = db.Fornecedors.Include(f => f.Localizacao);
            return View(fornecedors.ToList());
        }


        public ActionResult Fornecedores()
        {
            var select = new ViewFornecedor
            {
                    Fornecedors = fornec.GetFew()
            };
            return View(select);
        }

        // GET: Sistema/Fornecedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedors.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // GET: Sistema/Fornecedor/Create
        public ActionResult Create()
        {
            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco");
            return View();
        }

        // POST: Sistema/Fornecedor/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,CNPJ,RazaoSocial,Site,Email,Status,CEP,Numero")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Fornecedors.Add(fornecedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco", fornecedor.CEP);
            return View(fornecedor);
        }

        // GET: Sistema/Fornecedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedors.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco", fornecedor.CEP);
            return View(fornecedor);
        }

        // POST: Sistema/Fornecedor/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,CNPJ,RazaoSocial,Site,Email,Status,CEP,Numero")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fornecedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco", fornecedor.CEP);
            return View(fornecedor);
        }

        // GET: Sistema/Fornecedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedors.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Sistema/Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fornecedor fornecedor = db.Fornecedors.Find(id);
            db.Fornecedors.Remove(fornecedor);
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
