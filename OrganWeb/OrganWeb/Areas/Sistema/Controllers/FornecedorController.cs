using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Models.Banco;
using OrganWeb.Models.Endereco;
using OrganWeb.Models.Telefone;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using OrganWeb.Models.Pessoa;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FornecedorController : Controller
    {
        private Fornecedor fornecedor = new Fornecedor();
        private Estado estado = new Estado();
        private DDD ddd = new DDD();
        private BancoContext db = new BancoContext();
        private VwFornecedor vwfornecedor = new VwFornecedor();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Financeiro");
        }
        
        public ActionResult Fornecedores()
        {
            return View();
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornecedor = fornecedor.GetByID(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }
        
        public ActionResult Create()
        {
            var select = new CreateFornecedorViewModel
            {
                Estados = estado.GetAll(),
                DDDs = ddd.GetAll()
            };
            // aqui ele tá enviando os estados e ddds pro usuário selecionar qnd ele for criar um fornecedor
            return View(select);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFornecedorViewModel model)
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
                    Nome = model.NomeFantasia,
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


                var pessoajuridica = new PessoaJuridica
                {
                    RazaoSocial = model.RazaoSocial,
                    CNPJ = model.CNPJ,
                    IE = model.IE,
                    IdPessoa = pessoa.Id
                };

                db.PessoaJuridicas.Add(pessoajuridica);
                db.SaveChanges();

                var fornecedor = new Fornecedor
                {
                    Status = true,
                    IdPessoa = pessoa.Id
                };
                db.Fornecedors.Add(fornecedor);
                db.SaveChanges();
            }
            return View(fornecedor);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornecedor = fornecedor.GetByID(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(fornecedor).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }
        
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornecedor = fornecedor.GetByID(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(Fornecedor fornecedor)
        {

            fornecedor = fornecedor.GetByID(fornecedor.Id);
            fornecedor.Delete(fornecedor.Id);
            fornecedor.Save();
            return RedirectToAction("Index");
        }




        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornecedor = fornecedor.GetByID(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Fornecedor fornecedor)
        {

            if (ModelState.IsValid)
            {
                fornecedor.Update(fornecedor);
                fornecedor.Save();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwfornecedor = vwfornecedor.GetByID(id);
            if (vwfornecedor == null)
            {
                return HttpNotFound();
            }

            return View(vwfornecedor);
        }

       
        
        

    }
}
