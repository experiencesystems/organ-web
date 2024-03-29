﻿using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Ecommerce.Controllers
{
    public class CarrinhoController : Controller
    {
        private Carrinho carrinho = new Carrinho();
        private Anuncio anuncio = new Anuncio();
        private List<Carrinho> carrinhos = new List<Carrinho>();

        public async Task<ActionResult> Index()
        {
            carrinhos = await carrinho.GetCarrinho();
            var (QtdItens, ValorTotal) = await carrinho.GetQtdETotalCarrinho();
            var carrinhoViewModel = new CarrinhoViewModel
            {
                Carrinhos = carrinhos,
                ItensTotal = QtdItens,
                ValorTotal = ValorTotal,
            };
            return View(carrinhoViewModel);
        }
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAoCarrinho(int? idAnuncio)
        {
            if (idAnuncio == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            anuncio = await anuncio.GetByID(idAnuncio);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            var carrito = await carrinho.GetItemCarrinho(anuncio);
            if (carrito != null && carrito.Qtd + 1 > anuncio.Quantidade)
                return RedirectToAction("Index");
            await carrinho.AddAoCarrinho(anuncio);
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> RemoverDoCarrinho(int? idAnuncio)
        {
            if (idAnuncio == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            anuncio = await anuncio.GetByID(idAnuncio);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            await carrinho.RemoverDoCarrinho(anuncio);
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> ApagarCarrinho()
        {
            await carrinho.LimparCarrinho();
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public async Task<ActionResult> SumarioCarrinho()
        {
            (int QtdItens, double ValorTotal) = await carrinho.GetQtdETotalCarrinho();
            ViewData["Valor"] = ValorTotal;
            ViewData["CartCount"] = QtdItens;
            return PartialView("SumarioCarrinho");
        }
    }
}