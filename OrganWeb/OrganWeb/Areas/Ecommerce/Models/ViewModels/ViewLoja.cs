using OrganWeb.Areas.Ecommerce.Models.Vendas;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.ViewModels
{
    public class ViewLoja
    {
        public IPagedList<Anuncio> AnunciosRecentes { get; set; }
        public IPagedList<Anuncio> AnunciosComDesconto { get; set; }
    }
}