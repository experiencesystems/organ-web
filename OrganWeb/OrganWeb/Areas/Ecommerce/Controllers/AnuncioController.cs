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
    [Authorize]
    public class AnuncioController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Anuncio anuncio)
        {
            //https://stackoverflow.com/questions/20925822/asp-net-mvc-5-identity-how-to-get-current-applicationuser
            //anuncio.IdProduto = produto.Id;
            //anuncio.IdUsuario = UserManager.FindById(User.Identity.GetUserId());
            anuncio.Add(anuncio);
            await anuncio.Save();
            return View(anuncio);
        }
    }
}