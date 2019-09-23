using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Financas;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewFinanceiro
    {
        public IEnumerable<Compra> Compras { get; set; }
        public IEnumerable<Despesa> Despesas { get; set; }
    }
}