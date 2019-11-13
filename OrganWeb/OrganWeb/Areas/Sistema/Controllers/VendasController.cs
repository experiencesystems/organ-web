using OrganWeb.Areas.Ecommerce.Models.Vendas;
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
    public class VendasController : Controller
    {
        private Pedido pedido = new Pedido();
        private Venda venda = new Venda();

        public async Task<ActionResult> Index()
        {//TODO: filtro de pedido e venda por status, pagedlist
            var select = new ViewVendas
            {
                Pedidos = await pedido.GetPedidosAnunciante(),
                Vendas = await venda.GetVendasDoAnunciante()
            };
            return View();
        }

        public async Task<ActionResult> AceitarPedido(int? idPedido)
        {
            if (idPedido == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pedido = await pedido.GetByID(idPedido);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        [HttpPost, ActionName("AceitarPedido")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CriarVenda(Pedido pedido)
        {
            pedido.Status = 2;
            pedido.Update(pedido);
            await pedido.Save();

            //TODO: criar venda aqui

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RecusarPedido(int? idPedido)
        {
            if (idPedido == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pedido = await pedido.GetByID(idPedido);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecusarPedido(Pedido pedido)
        {
            pedido.Status = 9;
            pedido.Update(pedido);
            await pedido.Save();
            return View();
        }
    }
}