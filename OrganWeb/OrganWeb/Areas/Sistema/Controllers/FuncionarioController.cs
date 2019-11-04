using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using OrganWeb.Models.Banco;
using OrganWeb.Models.Endereco;
using OrganWeb.Models.Pessoa;
using OrganWeb.Models.Telefone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FuncionarioController : Controller
    {
        private Cidade cidade = new Cidade();
        private Bairro bairro = new Bairro();
        private Logradouro logradouro = new Logradouro();
        private Endereco endereco = new Endereco();
        private Pessoa pessoa = new Pessoa();
        private TipoTel tipotel = new TipoTel();
        private Telefone telefone = new Telefone();
        private TelefonePessoa telefonePessoa = new TelefonePessoa();
        private PessoaFisica pessoaFisica = new PessoaFisica();
        private Cargo cargo = new Cargo();
        private Estado estado = new Estado();
        private DDD ddd = new DDD();
        private Funcionario funcionario = new Funcionario();
        private VwFuncionario vwfuncionario = new VwFuncionario();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Fazenda");
        }

        public async Task<ActionResult> Create()
        {
            var select = new CreateFuncionarioViewModel
            {
                Estados = await estado.GetAll(),
                DDDs = await ddd.GetAll()
            };
            return View(funcionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateFuncionarioViewModel model)
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

                pessoa = new Pessoa
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    NumeroEndereco = model.Numero,
                    CompEndereco = model.Complemento,
                    CEP = endereco.CEP
                };

                pessoa.Add(pessoa);
                await pessoa.Save();

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

                telefonePessoa = new TelefonePessoa
                {
                    IdPessoa = pessoa.Id,
                    IdTelefone = telefone.Id
                };

                telefonePessoa.Add(telefonePessoa);
                await telefonePessoa.Save();

                pessoaFisica = new PessoaFisica
                {
                    CPF = model.CPF,
                    DataNasc = model.DataNascimento,
                    RG = model.RG,
                    Foto = "",
                    IdPessoa = pessoa.Id
                };

                pessoaFisica.Add(pessoaFisica);
                await pessoaFisica.Save();

                cargo = new Cargo
                {
                    Nome = model.Cargo
                };

                cargo.Add(cargo);
                await cargo.Save();

                funcionario = new Funcionario
                {
                    Salario = model.Salario,
                    IdCargo = cargo.Id,
                    IdPessoa = pessoa.Id,
                    Status = true
                };

                funcionario.Add(funcionario);
                await funcionario.Save();

                return RedirectToAction("Index");
            }
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