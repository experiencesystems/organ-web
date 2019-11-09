using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models
{
    [Table("tbEntrega")]
    public class Entrega
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Venda")]
        public int IdVenda{ get; set; }

        [Required]
        [Display(Name = "Valor do frete")]
        public double ValorFrete { get; set; }

        [Required]
        [Display(Name = "Desconto do frete")]
        public double DescFrete { get; set; }

        public virtual Venda Venda { get; set; }
    }
}