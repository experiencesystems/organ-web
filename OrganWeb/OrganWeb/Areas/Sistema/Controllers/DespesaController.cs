using OrganWeb.Areas.Sistema.Models.Financas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class DespesaController : Controller
    {
        private Despesa despesa = new Despesa();

        public ActionResult Index()
        {
            return Redirect("~/Sistema/Financeiro/Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                despesa.Add(despesa);
                despesa.Save();
                return RedirectToAction("Index");
            }
            return View(despesa);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            despesa = despesa.GetByID(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            return View(despesa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                despesa.Update(despesa);
                despesa.Save();
                return RedirectToAction("Index");
            }
            return View(despesa);
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            despesa = despesa.GetByID(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            return View(despesa);
        }

        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            despesa = despesa.GetByID(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            return View(despesa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(Despesa despesa)
        {
            despesa.Delete(despesa.Id);
            despesa.Save();
            return RedirectToAction("Index");
        }
    }
}