using OrganWeb.Areas.Ecommerce.Models.Vendas;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewVendas
    {
        public IPagedList<Venda> Vendas { get; set; }
        public IPagedList<PedidoAnuncio> Pedidos { get; set; }
    }
}