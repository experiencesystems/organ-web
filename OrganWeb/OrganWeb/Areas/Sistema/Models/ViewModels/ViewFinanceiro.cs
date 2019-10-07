using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Financas;
using OrganWeb.Areas.Sistema.Models.ViewsBanco;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewFinanceiro
    {
        public IEnumerable<VwCompra> VwCompras { get; set; }
        public IEnumerable<VwSaldo> VwSaldos { get; set; }
        public IEnumerable<VwFluxoDeCaixa> VwFluxoDeCaixas { get; set; }
    }
}