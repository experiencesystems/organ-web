using OrganWeb.Areas.Sistema.Models.Administrativo;
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
    public class SoloController : Controller
    {
        private Solo solo = new Solo();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Fazenda");
        }

        public ActionResult Create()
        {
            return View(solo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Solo solo)
        {
            if (ModelState.IsValid)
            {
                solo.Add(solo);
                await solo.Save();
                return RedirectToAction("Index");
            }
            return View(solo);
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solo = await solo.GetByID(id);
            if (solo == null)
            {
                return HttpNotFound();
            }
            return View(solo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Solo solo)
        {
            if (ModelState.IsValid)
            {
                solo.Update(solo);
                await solo.Save();
                return RedirectToAction("Index");
            }
            return View(solo);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solo = await solo.GetByID(id);
            if (solo == null)
            {
                return HttpNotFound();
            }
            return View(solo);
        }
    }
}