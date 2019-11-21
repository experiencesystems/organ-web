using Microsoft.AspNet.Identity;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Ecommerce.Controllers
{
    public class PedidoController : Controller
    {
        private Carrinho carrinho = new Carrinho();
        private Anuncio anuncio = new Anuncio();
        private PedidoAnuncio pedidoAnuncio = new PedidoAnuncio();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Checkout(int? idAnuncio)
        {
            //Recebe o anúncio que quer comprar
            if (idAnuncio == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Verifica se ele existe
            anuncio = await anuncio.GetByID(idAnuncio);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            //Vê se ele ta no carrinho
            var itemcarrinho = await carrinho.GetItemCarrinho(anuncio);
            if (itemcarrinho == null)
            {
                //Se não estiver ele adiciona e manda o usuário pro carrinho
                await carrinho.AddAoCarrinho(anuncio, 1);
                return RedirectToAction("Index", "Carrinho");
            }
            //Verifica se ele não tem itens no carrinho
            var itenscarrinho = await carrinho.GetCarrinho();
            if (itenscarrinho?.Count() <= 0)
            {
                //Se não tiver ele volta pro carrinho com uma mensagem de erro que não tem itens lá
                return RedirectToAction("Index", "Carrinho");
            }
            //Envia o carrinho pro checkout
            var (QtdItens, Subtotal) = await carrinho.GetQtdETotalCarrinho();
            return View(new Pedido { Carrinhos = itenscarrinho, IdUsuario = User.Identity.GetUserId(), Subtotal = Subtotal });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.Status = 1;
                pedido.Data = DateTime.Today;
                foreach (var item in pedido.Carrinhos)
                {
                    pedido.Add(pedido);
                    await pedido.Save();
                    //TODO: colocar campos de endereço no pedido
                    pedidoAnuncio = new PedidoAnuncio
                    {
                        IdAnuncio = item.IdAnuncio,
                        IdPedido = pedido.Id
                    };
                    pedidoAnuncio.Add(pedidoAnuncio);
                    await pedidoAnuncio.Save();
                }
                ViewBag.SuccessMessage = "Seu pedido foi efetuado com sucesso! Aguarde a confirmação do anunciante.";
                ModelState.Clear();
                return View();
            }
            return View(pedido);
        }
    }
}