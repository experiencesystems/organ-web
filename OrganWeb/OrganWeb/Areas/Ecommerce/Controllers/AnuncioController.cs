using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Sistema.Models.API;
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
        private ListarUnidades unmd = new ListarUnidades();
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
            unmd = await unmd.GetListarUnidades();
            return View("Novo", new Anuncio { Anunciante = new Anunciante { Usuario = await UserManager.FindByIdAsync(User.Identity.GetUserId()) }, Produto = new Produto { Nome = Nome, Unidades = unmd.UnidadeCadastros }, Quantidade = Qtd });
        }

        public async Task<ActionResult> Novo()
        {
            unmd = await unmd.GetListarUnidades();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            anuncio = new Anuncio { Anunciante = new Anunciante { Usuario = user }, Produto = new Produto { Unidades = unmd.UnidadeCadastros } };
            return View(anuncio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Novo(Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                produto.Add(anuncio.Produto);
                await produto.Save();

                anuncio.Status = true;
                anuncio.IdProduto = produto.Id;
                anuncio.IdAnunciante = User.Identity.GetUserId();
                anuncio.Add(anuncio);
                await anuncio.Save();

                return RedirectToAction("Detalhes", new { id = anuncio.Id });
            }
            anuncio.Produto.Unidades = unmd.UnidadeCadastros;
            return View(anuncio);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            anuncio = await anuncio.GetAnuncio(id);
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
            unmd = await unmd.GetListarUnidades();
            anuncio.Produto.Unidades = unmd.UnidadeCadastros;
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
                if (await unmd.GetByID(anuncio.Produto.UM) == null)
                {
                    anuncio.Produto.Unidades = unmd.UnidadeCadastros;
                    var uncd = new UnidadeCadastro()
                    {
                        Id = anuncio.Produto.UM,
                        Desc = anuncio.Produto.Unidades.Where(x => x.Id == anuncio.Produto.UM).Select(x => x.Desc).FirstOrDefault().ToString()
                    };
                    unmd.Add(uncd);
                    await unmd.Save();
                }
                anuncio.Produto.Update(anuncio.Produto);
                await anuncio.Produto.Save();
                //TODO: testar alteração de anúncio
                ViewBag.SuccessMessage = "Seu anúncio " + anuncio.Nome + " foi alterado com sucesso.";
                ViewBag.Id = anuncio.Id;
                ModelState.Clear();
                return View();
            }
            anuncio.Produto.Unidades = unmd.UnidadeCadastros;
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