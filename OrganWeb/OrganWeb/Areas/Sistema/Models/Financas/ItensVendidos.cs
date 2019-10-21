using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Financas
{
    [Table("tbItensVendidos")]
    public class ItensVendidos : Repository<ItensVendidos>
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Venda")]
        public int IdVenda { get; set; }
        
        [Key, Column(Order = 2)]
        [ForeignKey("Estoque")]
        [Display(Name = "Itens")]
        public int IdEstoque { get; set; }

        [Display(Name = "Desconto do produto")]
        [Range(0.01, 999.99, ErrorMessage = "Esse campo deve conter valores até 999,99")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "Digite um valor válido com até duas casas decimais, como 99,99")]
        public double? DescontoProd { get; set; }

        [Required]
        public double QtdVendida { get; set; }

        public virtual Venda Venda { get; set; }
        public virtual Estoque Estoque { get; set; }
    }
}