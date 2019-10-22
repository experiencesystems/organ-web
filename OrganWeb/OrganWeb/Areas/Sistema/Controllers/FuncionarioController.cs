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
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FuncionarioController : Controller
    {
        private Estado estado = new Estado();
        private DDD ddd = new DDD();
        private Funcionario funcionario = new Funcionario();
        private BancoContext db = new BancoContext();
        private VwFuncionario vwfuncionario = new VwFuncionario();

        public ActionResult Index()
        {
            var select = new ViewFuncionario
            {
                Funcionario = funcionario.GetFew()
            };
            return View(select);
        }

        public ActionResult Create()
        {
            var select = new CreateFuncionarioViewModel
            {
                Estados = estado.GetAll(),
                DDDs = ddd.GetAll()
            };
            return View(funcionario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFuncionarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cidade = new Cidade
                {
                    Nome = model.Cidade,
                    IdEstado = model.Estado
                };

                db.Cidades.Add(cidade);
                db.SaveChanges();

                var bairro = new Bairro
                {
                    Nome = model.Bairro,
                    IdCidade = cidade.Id
                };

                db.Bairros.Add(bairro);
                db.SaveChanges();

                var rua = new Logradouro
                {
                    Nome = model.Rua,
                    IdBairro = bairro.Id
                };

                db.Logradouros.Add(rua);
                db.SaveChanges();

                var cep = new Endereco
                {
                    CEP = model.CEP,
                    IdRua = rua.Id
                };

                db.Enderecos.Add(cep);
                db.SaveChanges();

                var pessoa = new Pessoa
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    NumeroEndereco = model.Numero,
                    CompEndereco = model.Complemento,
                    CEP = cep.CEP
                };

                db.Pessoas.Add(pessoa);
                db.SaveChanges();

                var tipotel = new TipoTel
                {
                    Tipo = model.TipoTelefone
                };

                db.TipoTels.Add(tipotel);
                db.SaveChanges();

                var telefone = new Telefone
                {
                    Numero = model.Numero,
                    IdDDD = model.DDD,
                    IdTipo = tipotel.Id
                };

                db.Telefones.Add(telefone);
                db.SaveChanges();

                var telpessoa = new TelefonePessoa
                {
                    IdPessoa = pessoa.Id,
                    IdTelefone = telefone.Id
                };

                db.TelefonePessoas.Add(telpessoa);
                db.SaveChanges();


                var pessoafisica = new PessoaFisica
                {
                    CPF = model.CPF,
                    DataNasc = model.DataNascimento,
                    RG = model.RG,
                    Foto = "",
                    IdPessoa = pessoa.Id
                };
                
                db.PessoaFisicas.Add(pessoafisica);
                db.SaveChanges();


                var cargo = new Cargo {
                    Nome = model.Cargo
                };
                db.Cargos.Add(cargo);
                db.SaveChanges();


                var funcionario = new Funcionario {
                    Salario = model.Salario,
                    IdCargo = cargo.Id,
                    IdPessoa = pessoa.Id,
                    Status = true
               
                };
                db.Funcionarios.Add(funcionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             vwfuncionario= vwfuncionario.GetByID(id);
            if (vwfuncionario == null)
            {
                return HttpNotFound();
            }

            return View(vwfuncionario);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            funcionario = funcionario.GetByID(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }

            return View(funcionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Funcionario funcionario)
        {

            if (ModelState.IsValid)
            {
                funcionario.Update(funcionario);
                funcionario.Save();
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwfuncionario = vwfuncionario.GetByID(id);
            if (vwfuncionario == null)
            {
                return HttpNotFound();
            }

            return View(vwfuncionario);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(VwFuncionario vwfuncionario)
        {
            funcionario.Delete(vwfuncionario.Id);
            funcionario.Save();

            return RedirectToAction("Index");
        }


    }
}