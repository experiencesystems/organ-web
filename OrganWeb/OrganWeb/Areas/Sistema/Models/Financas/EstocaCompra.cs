using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Financas
{
    [Table("tbEstocaCompra")]
    public class EstocaCompra : Repository<EstocaCompra>
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Estoque")]
        public double Quantidade { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Compra")]
        public double Desconto { get; set; }

        public virtual Compra Compra { get; set; }
        public virtual Estoque.Estoque Estoque { get; set; }
    }
}