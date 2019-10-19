using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class SementeController : Controller
    {
        private Semente semente = new Semente();
        private Estoque estoque = new Estoque();

        public ActionResult Sementes()
        {
            var select = new ViewSementes
            {
                Semente = semente.GetFew()
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
        public ActionResult Create(Semente semente)
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
                estoque.Save();
                estoque.Dispose();

                semente.IdEstoque = estoque.Id;
                semente.Estoque = null;
                estoque = null;
                semente.Add(semente);
                semente.Save();
                semente.Dispose();

                return RedirectToAction("Index");
            }
            return View(semente);
        }

        public ActionResult Excluir()
        {
            return View();
        }

        public ActionResult Editar()
        {
            return View();
        }
    }
}