using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
    public class AnuncioController : Controller
    {
        private Produto produto = new Produto();
        private Anuncio anuncio = new Anuncio();
        private ApplicationUserManager _userManager;

        public AnuncioController()
        {
        }

        public AnuncioController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NovoDeEstoque(string Nome, double Qtd)
        {
            return View("Novo", new Anuncio { Anunciante = new Anunciante { Usuario = await UserManager.FindByIdAsync(User.Identity.GetUserId()) }, Produto = new Produto { Nome = Nome, Quantidade = Qtd } });
        }

        public async Task<ActionResult> Novo()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            anuncio = new Anuncio { Anunciante = new Anunciante { Usuario = user } };
            return View(anuncio);
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
                anuncio.IdAnunciante = User.Identity.GetUserId();
                anuncio.Add(anuncio);
                await anuncio.Save();

                return RedirectToAction("Detalhes", new { id = anuncio.Id });
            }
            return View(anuncio);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            anuncio = await anuncio.GetByID(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            anuncio = await anuncio.GetByID(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                anuncio.Update(anuncio);
                await anuncio.Save();
                ViewBag.SuccessMessage = "Seu anúncio " + anuncio.Nome + " foi alterado com sucesso.";
                ViewBag.Id = anuncio.Id;
                ModelState.Clear();
                return View();
            }
            return View(anuncio);
        }

        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            anuncio = await anuncio.GetByID(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmado(Anuncio anuncio)
        {
            anuncio.Status = false;
            anuncio.Update(anuncio);
            await anuncio.Save();
            ViewBag.SuccessMessage = "Seu anúncio " + anuncio.Nome + " foi desativado.";
            ModelState.Clear();
            return View();
        }
    }
}