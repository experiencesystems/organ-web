using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Models.Financeiro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Financas
{
    [Table("tbVenda")]
    public class Venda
    {
        [Key]
        public int Id { get; set; }
        
        [Range(0.01, 999.99, ErrorMessage = "Esse campo deve conter valores até 999,99")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "Digite um valor válido com até duas casas decimais, como 99,99")]
        public double? Desconto { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [ForeignKey("Pagamento")]
        public int IdPagamento { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Pagamento Pagamento { get; set; }
    }
}