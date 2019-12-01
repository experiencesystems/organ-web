using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.ViewsBanco;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewVendas
    {
        public IPagedList<VwVenda> Vendas { get; set; }
        public IPagedList<VwPedido> Pedidos { get; set; }
    }
}