using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class AreaController : Controller
    {
        private Area area = new Area();
        private Solo solo = new Solo();

        public ActionResult Index()
        {
            var select = new ViewAreas
            {
                Areas = area.GetAll(),
                Solos = solo.GetAll()
            };
            return View(select);
        }

        public ActionResult Create()
        {
            return View(new Area { Solos = solo.GetAll() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Area area)
        {
            if (ModelState.IsValid)
            {
                area.Add(area);
                area.Save();
                return RedirectToAction("Index");
            }
            area.Solos = solo.GetAll();
            return View(area);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            area = area.GetByID(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            area.Solos = solo.GetAll();
            return View(area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Area area)
        {
            if (ModelState.IsValid)
            {
                area.Update(area);
                area.Save();
                return RedirectToAction("Index");
            }
            area.Solos = solo.GetAll();
            return View(area);
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            area = area.GetByID(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            area = area.GetByID(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(Area area)
        {
            //TODO: Exclusão da área se estiver sendo usada
            area.Delete(area.Id);
            area.Save();
            return RedirectToAction("Index");
        }
    }

    

    
}