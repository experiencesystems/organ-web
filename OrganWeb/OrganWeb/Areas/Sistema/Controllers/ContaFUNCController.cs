using OrganWeb.Areas.Sistema.Models.Financas;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using OrganWeb.Models.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class ContaFUNCController : Controller
    {
        private DespesaFunc despesafunc = new DespesaFunc();
        private Despesa despesa = new Despesa();
        private VwFuncionario vwfuncionario = new VwFuncionario();

        public ActionResult Index()
        {
            return Redirect("~/Sistema/Financeiro/Index");
        }

        public ActionResult Create()
        {
            var select = new DespesaFunc
            {
                Funcionarios = vwfuncionario.GetAll()
            };
            return View(select);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DespesaFunc despesafunc)
        {
            if (ModelState.IsValid)
            {
                var despesa = new Despesa
                {
                    Data = despesafunc.Despesa.Data,
                    ValorPago = despesafunc.Despesa.ValorPago
                };
                despesa.Add(despesa);
                despesa.Save();

                var despesaadm2 = new DespesaFunc
                {
                    IdDespesa = despesa.Id,
                    IdFunc = despesafunc.IdFunc
                };
                despesaadm2.Add(despesafunc);
                despesaadm2.Save();

                return RedirectToAction("Index");
            }
            return View(despesafunc);
        }

        public ActionResult Editar(int? id, int? id2)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            despesafunc = despesafunc.GetByID(id, id2);
            if (despesafunc == null)
            {
                return HttpNotFound();
            }
            despesafunc.Funcionarios = vwfuncionario.GetAll();
            return View(despesafunc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(DespesaFunc despesafunc)
        {
            if (ModelState.IsValid)
            {
                despesafunc.Update(despesafunc);
                despesafunc.Save();
                despesa.Update(despesafunc.Despesa);
                despesa.Save();
                return RedirectToAction("Index");
            }
            return View(despesafunc);
        }

        public ActionResult Detalhes(int? id, int? id2)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            despesafunc = despesafunc.GetByID(id, id2);
            if (despesafunc == null)
            {
                return HttpNotFound();
            }
            return View(despesafunc);
        }

        public ActionResult Excluir(int? id, int? id2)
        {
            if (id == null | id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            despesafunc = despesafunc.GetByID(id, id2);
            if (despesafunc == null)
            {
                return HttpNotFound();
            }
            return View(despesafunc);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(DespesaFunc despesafunc)
        {
            despesafunc.Delete(despesafunc.IdFunc, despesafunc.IdDespesa);
            despesafunc.Save();
            despesa.Delete(despesafunc.IdDespesa);
            despesa.Save();
            return RedirectToAction("Index");
        }
    }
}