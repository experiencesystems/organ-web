using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Controllers
{
    [Authorize]
    public class FazendaController : Controller
    {
        private BancoContext db = new BancoContext();

        // GET: Sistema/Fazenda
        //[Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            //https://stackoverflow.com/questions/11241341/mvc-entity-framework-select-data-from-multiple-models
            //https://stackoverflow.com/questions/37298612/display-data-for-two-tables-to-layout-page-mvc-5

            var fazenda = new ViewFazenda
            {
                Funcionario = db.Funcionarios
                    .Include(f => f.Cargo).Take(15).OrderBy(f => f.Id)
                    .AsQueryable(),
                Fazenda = db.Fazendas.Include(f => f.Localizacao).AsQueryable()
            };
            return View(fazenda);
        }

        public partial class _Default : System.Web.UI.Page
        {
            static bool _isSqlTypesLoaded = false;

            public _Default()
            {
                if (!_isSqlTypesLoaded)
                {
                    SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~"));
                    _isSqlTypesLoaded = true;
                }

            }
        }
    }
}
