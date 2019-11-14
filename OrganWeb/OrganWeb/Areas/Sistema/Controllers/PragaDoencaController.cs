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
        private VwControle vwControle = new VwControle();
        
        public async Task<ActionResult> Index(int? pagedoenca, int? pagecont)
        {
            int pdoenca = pagedoenca ?? 1;
            int pcont = pagecont ?? 1;
            return View(new ViewPragaControle { PragaOrDoencas = await vwpraga.GetPagedAll(pdoenca), Controles = await vwControle.GetPagedAll(pcont)});
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
            //TODO: editar pragadoenca
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
            var view = new PragaOrDoenca
            {
                Areas = await area.GetAll()
            };
            return View(view);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PragaOrDoenca pragadoenca)
        {
            if (ModelState.IsValid)
            {
                //ACTION NÃO FUNCIONANDO!!
                var pd = new PragaOrDoenca
                {
                    Nome = pragadoenca.Nome,
                    PD = pragadoenca.PD
                }; //TODO: preencher de acordo com radiobutton
                pd.Add(pd);
                await pd.Save();
                
                foreach (var item in pragadoenca.IdArea)
                {
                    areapd.Add(new AreaPD { IdArea = item, IdPd = pd.Id, Status = true });
                    await areapd.Save();
                }
                return RedirectToAction("Index");
            }
            return View(pragadoenca);
        }
    }
}