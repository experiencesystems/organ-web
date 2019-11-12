using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.zRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.ViewModels
{
    public class CarrinhoViewModel
    {
        public List<Carrinho> Carrinhos { get; set; }
        public CarrinhoRepository Carrinho { get; set; }
        public double ValorTotal { get; set; }
        public int ItensTotal { get; set; }
    }
}