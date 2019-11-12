using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Vendas
{
    [Table("tbProduto")]
    public class Produto : EcommerceRepository<Produto>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Valor unitário")]
        public double ValorUnit { get; set; }

        [Required]
        public double Quantidade { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Nome { get; set; }
    }
}