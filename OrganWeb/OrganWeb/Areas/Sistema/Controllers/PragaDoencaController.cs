using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.Praga_e_Doenca;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwpraga = await vwpraga.GetByID(id);
            if (vwpraga == null)
            {
                return HttpNotFound();
            }

            return View(vwpraga);
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            praga = await praga.GetByID(id);
            if (praga == null)
            {
                return HttpNotFound();
            }

            return View(praga);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(PragaOrDoenca praga)
        {
            if (ModelState.IsValid)
            {
                praga.Update(praga);
                await praga.Save();
                return RedirectToAction("Index");
            }
            return View(praga);
        }

        public async Task<ActionResult> Create()
        {
            var view = new CreatePragaOrDoencaViewModel
            {
                Areas = await area.GetAll()
            };
            ViewBag.Areas = new MultiSelectList(view.Areas,
           "Id", "Nome");
            return View(view);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePragaOrDoencaViewModel pragadoenca, int[] IdArea)
        {
            if (ModelState.IsValid)
            {
                var pd = new PragaOrDoenca
                {
                    Nome = pragadoenca.Nome,
                    PD = pragadoenca.PD
                };
                pd.Add(pd);
                await pd.Save();
                
                foreach (var item in IdArea)
                {
                    areapd.Add(new AreaPD { IdArea = item, IdPd = pd.Id });
                    await areapd.Save();
                }
                return RedirectToAction("Index");
            }
            return View(pragadoenca);
        }

        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwpraga = await vwpraga.GetByID(id);
            if (vwpraga == null)
            {
                return HttpNotFound();
            }
            return View(vwpraga);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmado(VwPragaOrDoenca vwpraga)  
        {
            praga.Delete(vwpraga.Id);
            await praga.Save();

            return RedirectToAction("Index");
        }
    }
}