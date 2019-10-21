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

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FornecedorController : Controller
    {
        private Fornecedor fornecedor = new Fornecedor();
        private Estado estado = new Estado();
        private DDD ddd = new DDD();
        
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
        public ActionResult Create(CreateFornecedorViewModel fornecedor)
        {
            if (ModelState.IsValid)
            {
                var forn = new Fornecedor
                {
                    Status = fornecedor.Status,
                    IdPessoa = fornecedor.IdPessoa
                };
                forn.Add(forn);
                forn.Save();

                var pess = new VwFornecedor
                {
                    NomeFantasia = fornecedor.NomeFantasia,
                    RazaoSocial = fornecedor.RazaoSocial,
                      CNPJ = fornecedor.CNPJ,
                        IE = fornecedor.IE,
                    Email = fornecedor.RazaoSocial,
                    Telefones = fornecedor.Telefone

                };
                pess.Add(pess);
                pess.Save();
                var ddd = new DDD
                {
                    Valor = fornecedor.DDD,
                    

                };
                ddd.Add(ddd);
                ddd.Save();

                var estado = new Estado
                {
                    Nome = fornecedor.Estado
                    

                };
                estado.Add(estado);
                estado.Save();
                /*var endereco = new VwEndereco
                {

                    Rua = fornecedor.Rua,
                    BCF = fornecedor.BCF

                };
                endereco.Add(endereco);
                endereco.Save() ta dano ruim aki*/


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
    }
}
