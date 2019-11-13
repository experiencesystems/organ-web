using Microsoft.AspNet.Identity;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Ecommerce.Controllers
{
    public class AnuncioController : Controller
    {
        private Produto produto = new Produto();

        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                produto.Add(new Produto
                {
                    Nome = anuncio.Produto.Nome,
                    ValorUnit = anuncio.Produto.ValorUnit,
                    Quantidade = anuncio.Produto.Quantidade
                });
                await produto.Save();

                anuncio.Status = true;
                anuncio.IdProduto = produto.Id;
                anuncio.IdUsuario = User.Identity.GetUserId();
                anuncio.Add(anuncio);
                await anuncio.Save();

                return RedirectToAction("Index", "Loja");
            }
            return View(anuncio);
        }
    }
}