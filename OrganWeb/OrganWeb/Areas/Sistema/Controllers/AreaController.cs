using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    [Authorize]
    public class AreaController : Controller
    {
        private Area area = new Area();
        private Solo solo = new Solo();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Fazenda");
        }

        public async Task<ActionResult> Create()
        {
            return View(new Area { Solos = await solo.GetAll() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Area area)
        {
            if (ModelState.IsValid)
            {
                area.Add(area);
                await area.Save();
                return RedirectToAction("Index");
            }
            area.Solos = await solo.GetAll();
            return View(area);
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            area = await area.GetByID(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            area.Solos = await solo.GetAll();
            return View(area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Area area)
        {
            if (ModelState.IsValid)
            {
                area.Update(area);
                await area.Save();
                return RedirectToAction("Index");
            }
            area.Solos = await solo.GetAll();
            return View(area);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            area = await area.GetByID(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }
    }

    

    
}