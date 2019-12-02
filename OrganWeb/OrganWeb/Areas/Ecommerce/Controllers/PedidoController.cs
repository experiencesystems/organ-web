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
using OrganWeb.Areas.Ecommerce.Models.Financeiro;
using OrganWeb.Areas.Ecommerce.Models.ViewsBanco;

namespace OrganWeb.Areas.Ecommerce.Controllers
{
    public class PedidoController : Controller
    {
        private Carrinho carrinho = new Carrinho();
        private Anuncio anuncio = new Anuncio();
        private Estado estado = new Estado();
        private PedidoAnuncio pedidoAnuncio = new PedidoAnuncio();

        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await new VwPedido().GetPedidosCliente());
        }

        [Authorize]
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
            return View(new Pedido
            {
                Endereco = new Endereco { Logradouro = new Logradouro { Bairro = new Bairro { Cidade = new Cidade { Estados = await estado.GetAll() } } } },
                Carrinhos = itenscarrinho,
                IdUsuario = User.Identity.GetUserId(),
                Subtotal = Subtotal
            });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout(Pedido pedido)
        {
            pedido.Carrinhos = await carrinho.GetCarrinho();
            pedido.IdUsuario = User.Identity.GetUserId();
            pedido.Status = 1;
            pedido.Data = DateTime.Today;
            pedido.Endereco.CEP = pedido.CEPEntrega;
            pedido.Endereco.Logradouro.CEP = pedido.CEPEntrega;

            ModelState.Remove("Endereco.CEP");
            ModelState.Remove("Endereco.Logradouro.CEP");

            if (ModelState.IsValid)
            {   //todo: verificar se o cep existe antes de inserilo
                var cep = await new Endereco().GetByID(pedido.CEPEntrega);
                if (cep == null)
                {
                    cep = new Endereco()
                    {
                        CEP = pedido.CEPEntrega
                    };
                    cep.Add(cep);
                    await cep.Save();
                }

                var cidade = new Cidade()
                {
                    Nome = pedido.Endereco.Logradouro.Bairro.Cidade.Nome,
                    IdEstado = pedido.Endereco.Logradouro.Bairro.Cidade.IdEstado
                };
                cidade.Add(cidade);
                await cidade.Save();

                var bairro = new Bairro()
                {
                    Nome = pedido.Endereco.Logradouro.Bairro.Nome,
                    IdCidade = cidade.Id
                };
                bairro.Add(bairro);
                await bairro.Save();

                var rua = new Logradouro()
                {
                    CEP = cep.CEP,
                    Nome = pedido.Endereco.Logradouro.Nome,
                    IdBairro = bairro.Id
                };
                rua.Add(rua);
                await rua.Save();

                var pagamento = pedido.Pagamento;
                pagamento.Add(pagamento);
                await pagamento.Save();

                pedido.IdPagamento = pagamento.Id;

                var grupoanunciantes = pedido.Carrinhos.GroupBy(p => p.Anuncio.Anunciante, p => p, (key, c) => new { Anunciante = key, pedido.Carrinhos });

                foreach (var anunciante in grupoanunciantes)
                {
                    foreach (var carrito in anunciante.Carrinhos.Where(x => x.Anuncio.Anunciante == anunciante.Anunciante))
                    {
                        var valorfrete = await MetodosAPI.GetValorDoFrete(carrito.Anuncio.Anunciante.CEP, pedido.CEPEntrega);
                        pedido.ValFrete = valorfrete.ValorFrete;
                        pedido.Endereco = null;
                        pedido.Add(pedido);
                        await pedido.Save();
                        pedidoAnuncio = new PedidoAnuncio
                        {
                            IdAnuncio = carrito.IdAnuncio,
                            IdPedido = pedido.Id,
                            Qtd = carrito.Qtd
                        };
                        pedidoAnuncio.Add(pedidoAnuncio);
                        await pedidoAnuncio.Save();
                    }
                }
                ViewBag.SuccessMessage = "Seu pedido foi efetuado com sucesso! Aguarde a confirmação do anunciante.";
                ModelState.Clear();
                return View();
            }
            var (QtdItens, Subtotal) = await carrinho.GetQtdETotalCarrinho();
            pedido.Endereco.Logradouro.Bairro.Cidade.Estados = await estado.GetAll();
            pedido.Subtotal = Subtotal;
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