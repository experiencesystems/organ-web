using Newtonsoft.Json;
using OrganWeb.Areas.Sistema.Models.API;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using RestSharp;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    [Authorize]
    public class MaquinaController : Controller
    {
        private Maquina maquina = new Maquina();
        private Estoque estoque = new Estoque();
        private ListarUnidades unmd = new ListarUnidades();
        private UnidadeCadastro uncd = new UnidadeCadastro();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Estoque");
        }

        public async Task<ActionResult> Create()
        {
            var responseModel = await unmd.GetListarUnidades();
            return View(maquina = new Maquina { Estoque = new Estoque { Unidades = responseModel.UnidadeCadastros, Fornecedores = await new Fornecedor().GetAll() } });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Maquina maquina)
        {
            var responseModel = await unmd.GetListarUnidades();
            if (ModelState.IsValid)
            {
                if (await unmd.GetByID(maquina.Estoque.UM) == null)
                {
                    maquina.Estoque.Unidades = responseModel.UnidadeCadastros;
                    uncd = new UnidadeCadastro()
                    {
                        Id = maquina.Estoque.UM,
                        Desc = maquina.Estoque.Unidades.Where(x => x.Id == maquina.Estoque.UM).Select(x => x.Desc).FirstOrDefault().ToString()
                    };
                    unmd.Add(uncd);
                    await unmd.Save();
                }
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
            maquina.Estoque.Unidades = responseModel.UnidadeCadastros;
            maquina.Estoque.Fornecedores = await new Fornecedor().GetAll();
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
            maquina.Estoque = new Estoque
            {
                Fornecedores = await new Fornecedor().GetAll()
            };
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
                estoque.Update(maquina.Estoque);
                await estoque.Save();
                return RedirectToAction("Index", "Estoque");
            }
            maquina.Estoque.Fornecedores = await new Fornecedor().GetAll();
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