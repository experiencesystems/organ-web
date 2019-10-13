using OrganWeb.Areas.Sistema.Models.Administrativo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class SoloController : Controller
    {
        private Solo solo = new Solo();

        public ActionResult Index()
        {
            return Redirect("~/Sistema/Area/Index");
        }

        public ActionResult Create()
        {
            return View(solo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Solo solo)
        {
            if (ModelState.IsValid)
            {
                solo.Add(solo);
                solo.Save();
                return RedirectToAction("Index");
            }
            return View(solo);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solo = solo.GetByID(id);
            if (solo == null)
            {
                return HttpNotFound();
            }
            return View(solo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Solo solo)
        {
            if (ModelState.IsValid)
            {
                solo.Update(solo);
                solo.Save();
                return RedirectToAction("Index");
            }
            return View(solo);
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solo = solo.GetByID(id);
            if (solo == null)
            {
                return HttpNotFound();
            }
            return View(solo);
        }

        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solo = solo.GetByID(id);
            if (solo == null)
            {
                return HttpNotFound();
            }
            return View(solo);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(Solo solo)
        {
            //TODO: Excluir solo que tem área
            solo.Delete(solo.Id);
            solo.Save();
            return RedirectToAction("Index");
        }
    }
}