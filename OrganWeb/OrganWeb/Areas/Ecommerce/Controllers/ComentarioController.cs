using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult> NovoComentario(string comentario, int idAnuncio)
        {
            if (string.IsNullOrWhiteSpace(comentario))
                return RedirectToAction("Detalhes", "Anuncio", new { id = idAnuncio });

            var pergunta = new Comentario()
            {
                Data = DateTime.Today,
                IdAnuncio = idAnuncio,
                Valor = comentario,
                IdUsuario = User.Identity.GetUserId()
            };
            pergunta.Add(pergunta);
            await pergunta.Save();
            return RedirectToAction("Detalhes", "Anuncio", new { id = idAnuncio });
        }
    }
}