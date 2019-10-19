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
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Models.Banco;
using OrganWeb.Models.Endereco;
using OrganWeb.Models.Telefone;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FornecedorController : Controller
    {
        private Fornecedor fornecedor = new Fornecedor();
        private Estado estado = new Estado();
        private DDD ddd = new DDD();
        
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Financeiro");
        }
        
        public ActionResult Fornecedores()
        {
            return View();
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornecedor = fornecedor.GetByID(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }
        
        public ActionResult Create()
        {
            var select = new CreateFornecedorViewModel
            {
                Estados = estado.GetAll(),
                DDDs = ddd.GetAll()
            };
            // aqui ele tá enviando os estados e ddds pro usuário selecionar qnd ele for criar um fornecedor
            return View(select);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFornecedorViewModel fornecedor)
        {
            if (ModelState.IsValid)
            {
                //fornecedor.Add(fornecedor);
                //fornecedor.Save();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornecedor = fornecedor.GetByID(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(fornecedor).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornecedor = fornecedor.GetByID(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Fornecedor fornecedor = db.Fornecedors.Find(id);
            //db.Fornecedors.Remove(fornecedor);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
