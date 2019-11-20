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
    public class WishlistController : Controller
    {
        private Wishlist wishlist = new Wishlist();
        private Anuncio anuncio = new Anuncio();

        public async Task<ActionResult> Index(int? page)
        {
            int pagina = page ?? 1;
            return View(await wishlist.GetPagedAll(pagina));
        }

        [HttpPost]
        public async Task<ActionResult> AddWishlist(int? idAnuncio)
        {
            if (idAnuncio == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            anuncio = await anuncio.GetByID(idAnuncio);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            await wishlist.AddWishlist(anuncio);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> RemoverDaWishlist(int? idAnuncio)
        {
            if (idAnuncio == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            anuncio = await anuncio.GetByID(idAnuncio);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            await wishlist.DeleteItemWishlist(anuncio);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> ApagarWishlist()
        {
            await wishlist.LimparWishlist();
            return RedirectToAction("Index");
        }
    }
}