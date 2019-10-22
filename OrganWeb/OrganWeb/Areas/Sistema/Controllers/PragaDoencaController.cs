using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.Praga_e_Doenca;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class PragaDoencaController : Controller
    {

        private Area area = new Area();
        private AreaPD areapd = new AreaPD();

        // GET: Sistema/PragaDoenca
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var view = new CreatePragaOrDoencaViewModel
            {
                Areas = area.GetAll()
            };
            ViewBag.Areas = new MultiSelectList(view.Areas,
           "Id", "Nome");
            return View(view);
        }


        [HttpPost]
        public ActionResult Create(CreatePragaOrDoencaViewModel pragadoenca, int[] IdArea)
        {
            var pd = new PragaOrDoenca
            {
                Nome = pragadoenca.Nome,
                PD = pragadoenca.PD 
            };
            pd.Add(pd);
            pd.Save();


            foreach (var item in IdArea)
            {
             areapd.Add(new AreaPD { IdArea = item, IdPd = pd.Id });
               areapd.Save();
            }
            return View();
        }
    }
}