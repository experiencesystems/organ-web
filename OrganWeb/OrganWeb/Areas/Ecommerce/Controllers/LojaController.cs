using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OrganWeb.Areas.Ecommerce.Models;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
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

        public async Task<ActionResult> Index()
        {
            var anuncios = await anuncio.GetAnuncios();
            return View(anuncios);
        }
        
        public async Task<string> Detalhes()
        {
            string aa;
            var baseAddress = new Uri("http://api.frenet.com.br/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("token", "EFB53FBBRF4CAR4EDARB177R7F076B125EE8");

                using (var response = await httpClient.GetAsync("CEP/Address/05314000"))
                {
                    aa = await response.Content.ReadAsStringAsync();
                }
            }
            return aa;
        }
    }
}