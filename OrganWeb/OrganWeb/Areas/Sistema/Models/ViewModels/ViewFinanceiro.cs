using System.Collections.Generic;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Financeiro;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewFinanceiro
    {
        public IEnumerable<VwCompra> VwCompras { get; set; }
        public IEnumerable<VwSaldo> VwSaldos { get; set; }
        public IEnumerable<VwFluxoDeCaixa> VwFluxoDeCaixas { get; set; }
    }
}