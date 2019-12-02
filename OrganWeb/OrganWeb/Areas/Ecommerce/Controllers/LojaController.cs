using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OrganWeb.Areas.Ecommerce.Models;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Ecommerce.Controllers
{
    public class LojaController : Controller
    {
        private Anuncio anuncio = new Anuncio();
        private ApplicationUserManager _userManager;

        public LojaController()
        {
        }

        public LojaController(ApplicationUserManager userManager)
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

        public async Task<ActionResult> Pesquisa(int? page, string s)
        {
            int pagina = page ?? 1;
            var anuncios = await anuncio.GetAnuncios();
            return View(anuncio.GetAnunciosFiltro(pagina, s, anuncios));
        }

        public async Task<ActionResult> Index(int? pagedesc, int? pagenovos)
        {
            int pagecdesc = pagedesc ?? 1;
            int pagenovo = pagenovos ?? 1;
            var anuncios = await anuncio.GetAnuncios();
            var view = new ViewLoja
            {
                AnunciosComDesconto = anuncio.GetAnunciosEmPromocao(pagecdesc, anuncios),
                AnunciosRecentes = anuncio.GetAnunciosRecentes(pagenovo, anuncios)
            };
            return View(view);
        }

        public async Task<FileContentResult> FotoDoAnuncio(int? anuncio)
        {
            Anuncio anuncioo = new Anuncio();
            if(anuncio != null)
                anuncioo = await anuncioo.GetByID(anuncio);
            if (anuncio == null ||anuncioo.Foto == null)
            {
                string fileName = HttpContext.Server.MapPath(@"~/Imagens/admin.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);

                return File(imageData, "image/png");
            }
            return new FileContentResult(anuncioo.Foto, "image/png");
        }

        public async Task<ActionResult> Categoria(int? page, int cat)
        {
            int pagina = page ?? 1;
            var anuncios = await anuncio.GetAnuncios();
            ViewBag.Categoria = cat;
            return View(anuncio.GetAnunciosCategoria(pagina, cat, anuncios));
        }
    }
}