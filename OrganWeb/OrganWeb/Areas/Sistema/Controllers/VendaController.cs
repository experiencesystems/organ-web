using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.Financas;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Financeiro;
using OrganWeb.Models.Financeiro;
using OrganWeb.Models.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class VendaController : Controller
    {
        private Venda venda = new Venda();
        private Pessoa pessoa = new Pessoa();
        private VwItems vwitems = new VwItems();
        private VwVenda vwvenda = new VwVenda();
        private Cliente cliente = new Cliente();
        private Pagamento pagamento = new Pagamento();
        private ItensVendidos itensv = new ItensVendidos();

        public ActionResult Index()
        {
            return View(vwvenda.GetAll());
        }

        public ActionResult Create()
        {
            //TODO: create venda
            //TODO: refazer td oq usa viewbag
            //TODO: terminar crud venda
            venda = new Venda()
            {
                Clientes = cliente.GetAll(),
                Pagamento = new Pagamento(),
                Items = vwitems.GetAll()
            };
            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Venda venda, int[] IdEstoque)
        {
            if (ModelState.IsValid)
            {
                pagamento = venda.Pagamento;
                pagamento.Add(pagamento);
                pagamento.Save();

                venda.IdPagamento = pagamento.Id;
                venda.Pagamento = null;
                pagamento = null;
                venda.Add(venda);
                venda.Save();

                foreach (var item in IdEstoque)
                {
                    itensv.Add(new ItensVendidos { IdEstoque = item, IdVenda = venda.Id, DescontoProd = venda.Desconto, QtdVendida = venda.ItensVendidos.QtdVendida });
                    itensv.Save();
                }

                return RedirectToAction("Index");
            }
            return View(venda);
        }
    }
}