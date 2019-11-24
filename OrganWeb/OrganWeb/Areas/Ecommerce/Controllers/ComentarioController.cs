using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Ecommerce.Controllers
{
    public class ComentarioController : Controller
    {
        public ActionResult NovoComentario(string comentario, int idAnuncio)
        {
            return View();
        }
    }
}