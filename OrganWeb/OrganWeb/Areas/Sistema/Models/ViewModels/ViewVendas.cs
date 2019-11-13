using OrganWeb.Areas.Ecommerce.Models.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewVendas
    {
        public List<Venda> Vendas { get; set; }
        public List<Pedido> Pedidos { get; set; }
    }
}