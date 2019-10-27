using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Safras;
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
    public class SementeController : Controller
    {
        private Semente semente = new Semente();
        private Estoque estoque = new Estoque();
        
        public async Task<ActionResult> Sementes()
        {
            var select = new ViewSementes
            {
                Semente = await semente.GetFew()
            };
            return View(select);
        }

        public ActionResult Create()
        {
            semente = new Semente()
            {
                Estoque = new Estoque()
            };
            return View(semente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Semente semente)
        {
            if (ModelState.IsValid)
            {
                var estoque = new Estoque()
                {
                    Qtd = semente.Estoque.Qtd,
                    UM = semente.Estoque.UM,
                    ValorUnit = semente.Estoque.ValorUnit
                };
                estoque.Add(estoque);
                await estoque.Save();
                estoque.Dispose();

                semente.IdEstoque = estoque.Id;
                semente.Estoque = null;
                estoque = null;
                semente.Add(semente);
                await semente.Save();
                semente.Dispose();

                return RedirectToAction("Index");
            }
            return View(semente);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            semente = await semente.GetByID(id);
            if (semente == null)
            {
                return HttpNotFound();
            }
            return View(semente);
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            semente = await semente.GetByID(id);
            if (semente == null)
            {
                return HttpNotFound();
            }
            return View(semente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Semente semente)
        {
            if (ModelState.IsValid)
            {
                semente.Update(semente);
                await semente.Save();
                return RedirectToAction("Index");
            }
            return View(semente);
        }

        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            semente = await semente.GetByID(id);
            if (semente == null)
            {
                return HttpNotFound();
            }
            return View(semente);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmado(Semente semente)
        {
            semente.Delete(semente.IdEstoque);
            await semente.Save();

            return RedirectToAction("Index");
        }
    }
}