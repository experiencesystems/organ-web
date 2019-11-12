using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OrganWeb.Areas.Ecommerce.Models.Usuarios;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Ecommerce.Controllers
{
    [Authorize]
    public class AnuncioController : Controller
    {
        protected Produto produto = new Produto();
        protected EcommerceContext EcommerceContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public AnuncioController()
        {
            this.EcommerceContext = new EcommerceContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.EcommerceContext));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Anuncio anuncio)
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