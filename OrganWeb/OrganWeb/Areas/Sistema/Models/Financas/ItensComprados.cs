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
        [Display(Name = "Desconto do produto")]
        [Range(0.00, 999.99, ErrorMessage = "Esse campo deve conter valores até 999,99")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "Digite um valor válido com até duas casas decimais, como 99,99")]
        public double DescontoProd { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public double QtdProd { get; set; }

        public virtual Compra Compra { get; set; }
        public virtual Estoque Estoque { get; set; }
    }
}