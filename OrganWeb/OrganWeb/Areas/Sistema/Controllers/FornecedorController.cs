using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using System.Threading.Tasks;
using OrganWeb.Areas.Ecommerce.Models.Endereco;
using OrganWeb.Areas.Sistema.Models.Telefone;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FornecedorController : Controller
    {
        private Cidade cidade = new Cidade();
        private Bairro bairro = new Bairro();
        private Logradouro logradouro = new Logradouro();
        private Endereco endereco = new Endereco();
        private TipoTel tipotel = new TipoTel();
        private Telefone telefone = new Telefone();
        private Fornecedor fornecedor = new Fornecedor();
        private Estado estado = new Estado();
        private DDD ddd = new DDD();
        private VwFornecedor vwfornecedor = new VwFornecedor();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Estoque");
        }
        
        public async Task<ActionResult> Create()
        {
            var select = new CreateFornecedorViewModel
            {
                Estados = await estado.GetAll(),
                DDDs = await ddd.GetAll()
            };
            // aqui ele tá enviando os estados e ddds pro usuário selecionar qnd ele for criar um fornecedor
            return View(select);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateFornecedorViewModel model)
        {
            if (ModelState.IsValid)
            {
                cidade = new Cidade
                {
                    Nome = model.Cidade,
                    IdEstado = model.Estado
                };

                cidade.Add(cidade);
                await cidade.Save();

                bairro = new Bairro
                {
                    Nome = model.Bairro,
                    IdCidade = cidade.Id
                };

                bairro.Add(bairro);
                await bairro.Save();

                logradouro = new Logradouro
                {
                    Nome = model.Rua,
                    IdBairro = bairro.Id
                };

                logradouro.Add(logradouro);
                await logradouro.Save();

                endereco = new Endereco
                {
                    CEP = model.CEP,
                    IdRua = logradouro.Id
                };

                endereco.Add(endereco);
                await endereco.Save();

                tipotel = new TipoTel
                {
                    Tipo = model.TipoTelefone
                };

                tipotel.Add(tipotel);
                await tipotel.Save();

                telefone = new Telefone
                {
                    Numero = model.Numero,
                    IdDDD = model.DDD,
                    IdTipo = tipotel.Id
                };

                telefone.Add(telefone);
                await telefone.Save();

                fornecedor = new Fornecedor
                {
                    Status = true
                };
                fornecedor.Add(fornecedor);
                await fornecedor.Save();
            }
            return View(fornecedor);
        }
        
        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwfornecedor = await vwfornecedor.GetByID(id);
            if (vwfornecedor == null)
            {
                return HttpNotFound();
            }
            return View(vwfornecedor);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmado(VwFornecedor vwfornecedor)
        {
            fornecedor = await fornecedor.GetByID(vwfornecedor.Id);
            fornecedor.Status = false;
            fornecedor.Update(fornecedor);
            await fornecedor.Save();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornecedor = await fornecedor.GetByID(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Fornecedor fornecedor)
        {//TODO: ver se é createfornecedorviewmodel
            if (ModelState.IsValid)
            {
                fornecedor.Update(fornecedor);
                await fornecedor.Save();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwfornecedor = await vwfornecedor.GetByID(id);
            if (vwfornecedor == null)
            {
                return HttpNotFound();
            }
            return View(vwfornecedor);
        }
    }
}
