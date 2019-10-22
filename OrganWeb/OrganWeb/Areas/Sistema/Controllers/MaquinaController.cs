using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Ferramentas;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class MaquinaController : Controller
    {
        private Maquina maquina = new Maquina();
        private Estoque estoque = new Estoque();

        public ActionResult Index()
        {
            var select = new ViewMaquina
            {
                Maquina = maquina.GetFew()
            };
            return View(select);
        }

        public ActionResult Create()
        {
            maquina = new Maquina() { Estoque = new Estoque() };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                estoque = maquina.Estoque;
                estoque.Add(estoque);
                estoque.Save();
                maquina.IdEstoque = estoque.Id;
                maquina.Estoque = null;
                estoque = null;
                maquina.Add(maquina);
                maquina.Save();
                return RedirectToAction("Index");


            }
            return View(maquina);
        }

        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            maquina = maquina.GetByID(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }

            return View(maquina);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(Maquina maquina)
        {

            maquina = maquina.GetByID(maquina.IdEstoque);
            maquina.Delete(maquina.IdEstoque);
            maquina.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            maquina = maquina.GetByID(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            return View(maquina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Maquina maquina)
        {

            if (ModelState.IsValid)
            {
                maquina.Update(maquina);
                maquina.Save();
                return RedirectToAction("Index");
            }
            return View(maquina);
        }
    }
}