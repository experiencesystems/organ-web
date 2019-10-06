using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models.ViewsBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FinanceiroController : Controller
    {
        private VwCompra vwcompra = new VwCompra();
        private VwSaldo vwsaldo = new VwSaldo();
        private VwFluxoDeCaixa vwfluxo = new VwFluxoDeCaixa();

        // GET: Sistema/Financeiro
        public ActionResult Index()
        {
            var select = new ViewFinanceiro
            {
                VwCompras = vwcompra.GetFew(),
                VwFluxoDeCaixas = vwfluxo.GetFew(),
                VwSaldos = vwsaldo.GetAll()
            };
            return View(select);
        }
    }
}