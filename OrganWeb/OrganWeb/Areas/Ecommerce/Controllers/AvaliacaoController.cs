using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Ecommerce.Controllers
{
    public class AvaliacaoController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovaAvaliacao(int idAnuncio, int Estrelas, string comentario)
        {
            return RedirectToAction("Detalhes", "Anuncio", new { id = idAnuncio });
        }
    }
}