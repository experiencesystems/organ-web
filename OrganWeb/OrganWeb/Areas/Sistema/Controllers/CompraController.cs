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
            var create = new CreateCompraViewModel
            {
                Fornecedores = vwfornecedor.GetAll(),
                Items = items.GetAll(),
                Tipos = pagamento.Tipos
            };
            ViewBag.Items = new MultiSelectList(create.Items, "Id", "Item");
            return View(create);
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
        public ActionResult Create(CreateCompraViewModel compra, int[] IdItem, int Tipo)
        {
            if (ModelState.IsValid)
            {
                var pag = new Pagamento
                {
                    QtdParcelas = compra.QtdParcelas,
                    Tipo = Tipo,
                    VlParcela = compra.ValorParcela
                };
                pag.Add(pag);
                pag.Save();

                var c = new Compra
                {
                    Data = compra.Data,
                    Desconto = compra.Desconto,
                    IdForn = compra.IdFornecedor,
                    IdPagamento = pag.Id
                };
                c.Add(c);
                c.Save();

                foreach (var item in IdItem)
                {
                    itemscomp.Add(new ItensComprados { IdEstoque = item, IdCompra = c.Id, DescontoProd = compra.Desconto, QtdProd = compra.Quantidade });
                    itemscomp.Save();
                }

                return RedirectToAction("Index", "Financeiro");
            }
            ViewBag.Items = new MultiSelectList(items.GetAll(), "Id", "Item");
            compra.Fornecedores = vwfornecedor.GetAll();
            compra.Tipos = pagamento.Tipos;
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