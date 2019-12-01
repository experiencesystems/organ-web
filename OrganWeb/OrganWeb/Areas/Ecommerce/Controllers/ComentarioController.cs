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
    public class ComentarioController : Controller
    {
        private ApplicationUserManager _userManager;

        public ComentarioController()
        {
        }

        public ComentarioController(ApplicationUserManager userManager)
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
        public async Task<ActionResult> NovoComentario(string comentario, int? idAnuncio)
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

            if (string.IsNullOrWhiteSpace(comentario))
                return RedirectToAction("Detalhes", "Anuncio", new { id = idAnuncio });

            var pergunta = new Comentario()
            {
                Data = DateTime.Today,
                IdAnuncio = anuncio.Id,
                Valor = comentario,
                IdUsuario = User.Identity.GetUserId()
            };
            pergunta.Add(pergunta);
            await pergunta.Save();
            return RedirectToAction("Detalhes", "Anuncio", new { id = idAnuncio });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirComentario(int? idComentario)
        {
            if (idComentario == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comentario = await new Comentario().GetByID(idComentario);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            int anuncio = comentario.IdAnuncio;
            comentario.Delete(comentario.Id);
            return RedirectToAction("Detalhes", "Anuncio", new { id = anuncio });
        }
    }
}