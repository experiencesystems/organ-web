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
    public class AvaliacaoController : Controller
    {
        private ApplicationUserManager _userManager;

        public AvaliacaoController()
        {
        }

        public AvaliacaoController(ApplicationUserManager userManager)
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
        public async Task<ActionResult> NovaAvaliacao(int? idAnuncio, int Estrelas)
        {
            if (idAnuncio == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var anuncio = await new Anuncio().GetByID(idAnuncio);
            if (anuncio == null)
            {
                return HttpNotFound();
            }

            var avaliacao = new Avaliacao()
            {
                IdAnuncio = anuncio.Id,
                IdUsuario = User.Identity.GetUserId(),
                Nota = Estrelas
            };
            avaliacao.Add(avaliacao);
            await avaliacao.Save();
            return RedirectToAction("Detalhes", "Anuncio", new { id = idAnuncio });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirAvaliacao(int? idAnuncio)
        {
            if (idAnuncio == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var anuncio = await new Anuncio().GetByID(idAnuncio);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            new Avaliacao().Delete(User.Identity.GetUserId(), anuncio.Id);
            return RedirectToAction("Detalhes", "Anuncio", new { id = anuncio });
        }
    }
}