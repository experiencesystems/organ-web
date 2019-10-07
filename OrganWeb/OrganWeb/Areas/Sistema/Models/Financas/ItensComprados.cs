using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.zRepositories;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Financas
{
    [Table("tbItensComprados")]
    public class ItensComprados : ItensCompradosRepository
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Compra")]
        public int IdCompra { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        [Required]
        [Range(0.00, 999.99)]
        [Display(Name = "Desconto do produto")]
        public double DescontoProd { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public double QtdProd { get; set; }

        public virtual Compra Compra { get; set; }
        public virtual Estoque Estoque { get; set; }
    }
}