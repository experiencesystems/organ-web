using OrganWeb.Areas.Ecommerce.Models.Endereco;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.Telefone;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FuncionarioController : Controller
    {
        private TipoTel tipotel = new TipoTel();
        private Telefone telefone = new Telefone();
        private DDD ddd = new DDD();
        private Funcionario funcionario = new Funcionario();
        private VwFuncionario vwfuncionario = new VwFuncionario();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Fazenda");
        }

        public async Task<ActionResult> Create()
        {
            return View(new Funcionario
            {
                Telefone = new Telefone
                {
                    DDDs = await ddd.GetAll()
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Funcionario model)
        {
            if (ModelState.IsValid)
            {
                tipotel = new TipoTel
                {
                    Tipo = model.Telefone.TipoTel.Tipo
                };
                tipotel.Add(tipotel);
                await tipotel.Save();

                telefone = new Telefone
                {
                    Numero = model.Telefone.Numero,
                    IdDDD = model.Telefone.IdDDD,
                    IdTipo = tipotel.Id
                };
                telefone.Add(telefone);
                await telefone.Save();

                model.Status = true;
                model.Add(model);
                await model.Save();

                var telfunc = new TelFunc
                {
                    IdFunc = funcionario.Id,
                    IdTelefone = telefone.Id
                };
                telfunc.Add(telfunc);
                await telfunc.Save();

                funcionario.Add(funcionario);
                await funcionario.Save();

                return RedirectToAction("Index");
            }
            funcionario.Telefone.DDDs = await ddd.GetAll();
            return View(funcionario);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwfuncionario = await vwfuncionario.GetByID(id);
            if (vwfuncionario == null)
            {
                return HttpNotFound();
            }
            return View(vwfuncionario);
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            funcionario = await funcionario.GetByID(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                funcionario.Update(funcionario);
                await funcionario.Save();
                return RedirectToAction("Index");
            }
            funcionario.Telefone.DDDs = await ddd.GetAll();
            return View(funcionario);
        }

        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwfuncionario = await vwfuncionario.GetByID(id);
            if (vwfuncionario == null)
            {
                return HttpNotFound();
            }
            return View(vwfuncionario);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmado(VwFuncionario vwfuncionario)
        {
            funcionario.Delete(vwfuncionario.Id);
            await funcionario.Save();
            return RedirectToAction("Index");
        }
    }
}