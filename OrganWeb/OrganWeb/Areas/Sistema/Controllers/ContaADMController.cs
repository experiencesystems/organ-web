using OrganWeb.Areas.Sistema.Models.Financas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class ContaADMController : Controller
    {
        private DespesaAdm despesaadm = new DespesaAdm();
        private Despesa despesa = new Despesa();
        private Conta conta = new Conta();

        public ActionResult Index()
        {
            return Redirect("~/Sistema/Financeiro/Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DespesaAdm despesaadm)
        {
            if (ModelState.IsValid)
            {
                var conta = new Conta
                {
                    Nome = despesaadm.Conta.Nome
                };
                conta.Add(conta);
                conta.Save();

                var despesa = new Despesa
                {
                    Data = despesaadm.Despesa.Data,
                    ValorPago = despesaadm.Despesa.ValorPago
                };
                despesa.Add(despesa);
                despesa.Save();

                var despesaadm2 = new DespesaAdm
                {
                    IdDespesa = despesa.Id,
                    IdConta = conta.Id
                };
                despesaadm2.Add(despesaadm);
                despesaadm2.Save();

                return RedirectToAction("Index");
            }
            return View(despesaadm);
        }

        public ActionResult Editar(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            despesaadm = despesaadm.GetByID(id, id2);
            if (despesaadm == null)
            {
                return HttpNotFound();
            }
            return View(despesaadm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(DespesaAdm despesaadm)
        {
            if (ModelState.IsValid)
            {
                conta.Update(despesaadm.Conta);
                conta.Save();
                despesa.Update(despesaadm.Despesa);
                despesa.Save();
                return RedirectToAction("Index");
            }
            return View(despesaadm);
        }

        public ActionResult Detalhes(int? id, int? id2)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            despesaadm = despesaadm.GetByID(id, id2);
            if (despesaadm == null)
            {
                return HttpNotFound();
            }
            return View(despesaadm);
        }

        public ActionResult Excluir(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            despesaadm = despesaadm.GetByID(id, id2);
            if (despesaadm == null)
            {
                return HttpNotFound();
            }
            return View(despesaadm);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(DespesaAdm despesaadm)
        {
            despesaadm.Delete(despesaadm.IdConta, despesaadm.IdDespesa);
            despesaadm.Save();
            conta.Delete(despesaadm.IdConta);
            conta.Save();
            despesa.Delete(despesaadm.IdDespesa);
            despesa.Save();
            return RedirectToAction("Index");
        }
    }
}