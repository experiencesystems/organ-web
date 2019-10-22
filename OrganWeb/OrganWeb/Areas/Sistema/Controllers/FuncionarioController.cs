using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Models.Endereco;
using OrganWeb.Models.Telefone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FuncionarioController : Controller
    {
        private Estado estado = new Estado();
        private DDD ddd = new DDD();
        private Funcionario funcionario = new Funcionario();

        public ActionResult Funcionario()
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
        public ActionResult Create(CreateFornecedorViewModel fornecedor)
        {
            if (ModelState.IsValid)
            {
                var pess = new Funcionario
                {
                    Status = fornecedor.Status,
                    IdPessoa = fornecedor.IdPessoa
                };
                pess.Add(pess);
                pess.Save();

              

            }
            return View(funcionario);
        }



        }
}