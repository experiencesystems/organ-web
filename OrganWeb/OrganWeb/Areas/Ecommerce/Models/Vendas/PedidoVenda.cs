using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Vendas
{
    [Table("tbPedidoVenda")]
    public class PedidoVenda
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Pedido")]
        public int IdPedido { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Venda")]
        public int IdVenda { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Venda Venda { get; set; }
    }
}