using Newtonsoft.Json;
using OrganWeb.Areas.Sistema.Models.API;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Ferramentas;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class MaquinaController : Controller
    {
        private Maquina maquina = new Maquina();
        private Estoque estoque = new Estoque();

        public async Task<ActionResult> Index()
        {
            var select = new ViewMaquina
            {
                Maquina = await maquina.GetFew()
            };
            return View(select);
        }

        public ActionResult Create()
        { 
            return View(maquina = new Maquina { Estoque = new Estoque() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                estoque = maquina.Estoque;
                estoque.Add(estoque);
                await estoque.Save();
                maquina.IdEstoque = estoque.Id;
                maquina.Estoque = null;
                estoque = null;
                maquina.Add(maquina);
                await maquina.Save();
                return RedirectToAction("Index");
            }
            return View(maquina);
        }

        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            maquina = await maquina.GetByID(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            return View(maquina);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmado(Maquina maquina)
        {

            maquina = await maquina.GetByID(maquina.IdEstoque);
            maquina.Delete(maquina.IdEstoque);
            await maquina.Save();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            maquina = await maquina.GetByID(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }
            return View(maquina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Maquina maquina)
        {
            if (ModelState.IsValid)
            {
                maquina.Update(maquina);
                await maquina.Save();
                return RedirectToAction("Index");
            }
            return View(maquina);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            maquina = await maquina.GetByID(id);
            if (maquina == null)
            {
                return HttpNotFound();
            }

            return View(maquina);
        }
    }
}