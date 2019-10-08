using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Financas;
using OrganWeb.Areas.Sistema.Models.ViewsBanco;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Models.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewCompra
    {
        public Compra Compra { get; set; }
        public Pagamento Pagamento { get; set; }
        public IEnumerable<Fornecedor> Fornecedores { get; set; }
        public IEnumerable<VwItems> VwItems { get; set; }
        public IEnumerable<ItensComprados> ItensComprados { get; set; }
    }
}