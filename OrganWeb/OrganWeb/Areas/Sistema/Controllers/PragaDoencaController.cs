using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.Praga_e_Doenca;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class PragaDoencaController : Controller
    {

        private Area area = new Area();
        private AreaPD areapd = new AreaPD();
        private PragaOrDoenca praga = new PragaOrDoenca();
        private VwPragaOrDoenca vwpraga = new VwPragaOrDoenca();

        // GET: Sistema/PragaDoenca
        public ActionResult Index()
        {
            return View(praga.GetFew());
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwpraga = vwpraga.GetByID(id);
            if (vwpraga == null)
            {
                return HttpNotFound();
            }

            return View(vwpraga);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            praga = praga.GetByID(id);
            if (praga == null)
            {
                return HttpNotFound();
            }

            return View(praga);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(PragaOrDoenca praga)
        {

            if (ModelState.IsValid)
            {
                praga.Update(praga);
                praga.Save();
                return RedirectToAction("Index");
            }
            return View(praga);
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
            if (ModelState.IsValid)
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
                return RedirectToAction("Index");
            }
            return View(pragadoenca);
        }

        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwpraga = vwpraga.GetByID(id);
            if (vwpraga == null)
            {
                return HttpNotFound();
            }

            return View(vwpraga);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(VwPragaOrDoenca vwpraga)  
        {
            praga.Delete(vwpraga.Id);
            praga.Save();

            return RedirectToAction("Index");
        }
    }
}