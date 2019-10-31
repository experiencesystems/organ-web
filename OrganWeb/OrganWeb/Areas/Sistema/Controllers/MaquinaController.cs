using Newtonsoft.Json;
using OrganWeb.Areas.Sistema.Models.API;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Ferramentas;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using RestSharp;
using System.Linq;
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
            var client = new RestClient("https://app.omie.com.br");
            var request = new RestRequest("/api/v1/geral/unidade/?JSON={\"call\":\"ListarUnidades\",\"app_key\":\"1560731700\",\"app_secret\":\"226dcf372489bb45ceede61bfd98f0f1\",\"param\":[{\"codigo\":\"\"}]}", Method.POST);
            var response = client.Execute(request);
            var responseModel = JsonConvert.DeserializeObject<ListarUnidades>(response.Content);

            return View(maquina = new Maquina { Estoque = new Estoque(), Unidades = responseModel.UnidadeCadastros });
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