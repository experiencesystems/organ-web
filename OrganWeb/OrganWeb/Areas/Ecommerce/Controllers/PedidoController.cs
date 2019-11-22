using Microsoft.AspNet.Identity;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Ecommerce.Models.API;
using OrganWeb.Areas.Ecommerce.Models.Endereco;

namespace OrganWeb.Areas.Ecommerce.Controllers
{
    public class PedidoController : Controller
    {
        private Carrinho carrinho = new Carrinho();
        private Anuncio anuncio = new Anuncio();
        private Estado estado = new Estado();
        private PedidoAnuncio pedidoAnuncio = new PedidoAnuncio();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Checkout()
        {
            //Verifica se ele não tem itens no carrinho
            var itenscarrinho = await carrinho.GetCarrinho();
            if (itenscarrinho?.Count() <= 0)
            {
                //Se não tiver ele volta pro carrinho com uma mensagem de erro que não tem itens lá
                return RedirectToAction("Index", "Carrinho");
            }
            //Envia o carrinho pro checkout
            var (QtdItens, Subtotal) = await carrinho.GetQtdETotalCarrinho();
            return View(new Pedido { Endereco = new Endereco { Logradouro = new Logradouro { Bairro = new Bairro { Cidade = new Cidade { Estados = await estado.GetAll() } } } },
                Carrinhos = itenscarrinho, IdUsuario = User.Identity.GetUserId(), Subtotal = Subtotal });
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

        [HttpPost]
        public async Task<ActionResult> PostFrete(string cep)
        {
            var carrinhos = await carrinho.GetCarrinho();
            string valor = await MetodosAPI.GetFreteFromCarrinhoAsync(carrinhos, cep);
            return Json(new { result = true, frete = valor });
        }
    }
}