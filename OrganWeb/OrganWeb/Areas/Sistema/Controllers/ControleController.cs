using OrganWeb.Areas.Sistema.Models.Controles;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
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
    public class ControleController : Controller
    {
        private Controle controle = new Controle();
        private ItensControle itenscontrole = new ItensControle();
        private FuncControle funccontrole = new FuncControle();
        private ControlePD controlepd = new ControlePD();
        private VwItems vwItems = new VwItems();
        private PragaOrDoenca pd = new PragaOrDoenca();
        private Funcionario func = new Funcionario();
        private List<int> ItensEstoque = new List<int>();
        private List<double> Quantidades = new List<double>();
        private Dictionary<int, double> ItensQuantidade = new Dictionary<int, double>();

        public async Task<ActionResult> Index()
        {
            return View(await controle.GetAll());
        }
         /* TODO: testar controller controle
          * TODO: CPF tem que ser único
          * TODO: inserir praga com área
          * TODO: RadioButton status controle
         */
        public async Task<ActionResult> Create()
        {
            var itens = await vwItems.GetAll();
            var model = new CreateControleViewModel
            {
                VwItems = itens.Where(x => x.Tipo != "Semente" && x.Tipo != "Produto").ToList(),
                Funcionarios = await func.GetAll(),
                PragaOrDoencas = await pd.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateControleViewModel model)
            //TODO: ver se não precisa mandar os int[] nos parâmetros
        {
            if (ModelState.IsValid)
            {
                controle = new Controle
                {
                    Desc = model.Desc,
                    Status = model.Status,
                    Efic = model.Efic,
                    NumLiberacoes = model.NumLiberacoes
                };
                controle.Add(controle);
                await controle.Save();

                foreach (var item in model.IdPD)
                {
                    controlepd.Add(new ControlePD { IdControle = controle.Id, IdPD = item });
                    await controlepd.Save();
                }

                foreach (var item in model.IdFunc)
                {
                    funccontrole.Add(new FuncControle { IdControle = controle.Id, IdFunc = item });
                    await funccontrole.Save();
                }

                //Primeiro adiciona os dois valores de array em uma lista
                foreach (var item in model.IdEstoque)
                {
                    ItensEstoque.Add(item);
                }
                foreach (var item in model.QtdUsada)
                {
                    Quantidades.Add(item);
                }

                //https://stackoverflow.com/questions/2434593/create-a-dictionary-using-2-lists-using-linq
                //Então eu crio um dicionário com as duas listas
                ItensQuantidade = ItensEstoque.Zip(Quantidades, (i, q) => new { Key = i, Value = q })
                     .ToDictionary(x => x.Key, x => x.Value);

                //https://stackoverflow.com/questions/141088/what-is-the-best-way-to-iterate-over-a-dictionary
                //Agora eu faço um foreach no dicionário para inserir os dois valores ao mesmo tempo (já que vão juntos na tabela)
                foreach (KeyValuePair<int, double> entry in ItensQuantidade)
                {
                    itenscontrole.Add(new ItensControle { IdControle = controle.Id, IdEstoque = entry.Key, QtdUsada = entry.Value });
                    await itenscontrole.Save();
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        //TODO: views detalhes editar excluir controle
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            controle = await controle.GetByID(id);
            if (controle == null)
            {
                return HttpNotFound();
            }
            return View(controle);
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            controle = await controle.GetByID(id);
            if (controle == null)
            {
                return HttpNotFound();
            }
            return View(controle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Controle controle)
        {
            //TODO: testar editar controle
            if (ModelState.IsValid) {
                controle.Update(controle);
                await controle.Save();
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            controle = await controle.GetByID(id);
            if (controle == null)
            {
                return HttpNotFound();
            }
            return View(controle);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Excluir")]
        public async Task<ActionResult> ExcluirConfirmado(Controle controle)
        {
            //TODO: testar excluir controle
            controle.Delete(controle.Id);
            await controle.Save();
            return RedirectToAction("Index");
        }
    }
}