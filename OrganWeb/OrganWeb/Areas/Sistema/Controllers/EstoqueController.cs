using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models;
using System.Web.Mvc;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Controllers
{
    [Authorize]
    public class EstoqueController : Controller
    {
        private BancoContext db = new BancoContext();

        // GET: Sistema/Estoque
        public ActionResult Index(Item item)
        {
            var viewestoque = new ViewEstoque
            {
                VwItems = db.VwItems.Take(15).AsQueryable(),
                Items = db.Items
            };

            return View(viewestoque);
        }

        //https://stackoverflow.com/questions/6287015/create-controller-for-partial-view-in-asp-net-mvc

        [ChildActionOnly]
        public ActionResult _NovoItem()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _NovoItem(Item item)
        {
            if (ModelState.IsValid)
            {
                var estoque = new Estoque
                {
                    Quantidade = item.Estoque.Quantidade,
                    UnidadeMedida = item.Estoque.UnidadeMedida
                };
                db.Estoques.Add(estoque);

                item.IdEstoque = estoque.Id;
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}