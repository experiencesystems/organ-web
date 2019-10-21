using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Financas;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models.ViewsBanco;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using OrganWeb.Models.Banco;
using OrganWeb.Models.Financeiro;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class CompraController : Controller
    {
        private BancoContext db = new BancoContext();
        private Fornecedor fornecedor = new Fornecedor();
        private VwFornecedor vwfornecedor = new VwFornecedor();
        private Pagamento pagamento = new Pagamento();
        private Compra compra = new Compra();
        private VwItems items = new VwItems();
        private ItensComprados itemscomp = new ItensComprados();

        public ActionResult Index()
        {
            return Redirect("~/Sistema/Financeiro/Index");
        }

        public ActionResult Create()
        {
            compra = new Compra
            {
                //Fornecedores = vwfornecedor.GetAll(),
                Items = items.GetAll(),
                Pagamento = new Pagamento()
            };
            return View(compra);
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compra = compra.GetByID(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            var itemscompr = itemscomp.GetAll().Where(a => a.IdCompra == compra.Id);
            var select = new ViewCompra
            {
                Compra = compra,
                ItensComprados = itemscompr,
                Pagamento = pagamento.GetByID(compra.IdPagamento),
                VwItems = items.GetAll().Where(a => itemscompr.Any(c => c.IdEstoque == a.Id))
            };
            ViewBag.Tipo = pagamento.Tipos.Where(x => x.Value == select.Pagamento.Tipo.ToString()).First().Text;
            return View(select);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Compra compra, int[] IdEstoque)
        {
            if (ModelState.IsValid)
            {
                pagamento = compra.Pagamento;
                pagamento.Add(pagamento);
                pagamento.Save();

                compra.IdPagamento = pagamento.Id;
                compra.Add(compra);
                compra.Save();

                foreach (var item in IdEstoque)
                {
                    itemscomp.Add(new ItensComprados { IdEstoque = item, IdCompra = compra.Id, DescontoProd = compra.Desconto, QtdProd = compra.ItensComprados.QtdProd });
                    itemscomp.Save();
                }

                return RedirectToAction("Index", "Financeiro");
            }
            compra.Fornecedores = vwfornecedor.GetAll();
            compra.Pagamento = new Pagamento();
            compra.Items = items.GetAll();
            return View(compra);
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compra = compra.GetByID(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            var select = new ViewCompra
            {
                Fornecedores = vwfornecedor.GetAll(),
                Compra = compra,
                Pagamento = pagamento.GetAll().Where(a => a.Id == compra.IdPagamento).FirstOrDefault()
            };
            return View(select);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ViewCompra vcompra)
        {
            if (ModelState.IsValid)
            {
                compra = vcompra.Compra;
                compra.Update(vcompra.Compra);
                compra.Save();
                pagamento = vcompra.Pagamento;
                pagamento.Update(vcompra.Pagamento);
                pagamento.Save();
                return RedirectToAction("Index");
            }
            return View(vcompra);
        }

        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compra = compra.GetByID(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            var itemscompr = itemscomp.GetAll().Where(a => a.IdCompra == compra.Id);
            var select = new ViewCompra
            {
                Compra = compra,
                ItensComprados = itemscompr,
                Pagamento = pagamento.GetByID(compra.IdPagamento),
                VwItems = items.GetAll().Where(a => itemscompr.Any(c => c.IdEstoque == a.Id))
            };
            ViewBag.Tipo = pagamento.Tipos.Where(x => x.Value == select.Pagamento.Tipo.ToString()).First().Text;
            return View(select);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(ViewCompra vcompra)
        {
            itemscomp.DeleteByIdCompra(vcompra.Compra);
            compra = vcompra.Compra;
            compra.Delete(vcompra.Compra.Id);
            compra.Save();
            pagamento = vcompra.Pagamento;
            pagamento.Delete(vcompra.Pagamento.Id);
            pagamento.Save();
            return RedirectToAction("Index");
        }
    }
}