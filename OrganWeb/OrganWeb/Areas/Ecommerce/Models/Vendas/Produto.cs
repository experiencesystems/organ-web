using OrganWeb.Areas.Ecommerce.Models.zBanco;
using OrganWeb.Areas.Sistema.Models.API;
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
        [ForeignKey("UnidadeMedida")]
        [Display(Name = "Unidade de medida")]
        public string UM { get; set; }

        [Required]
        [Display(Name = "Nome do produto")]
        [StringLength(30, MinimumLength = 2)]
        public string Nome { get; set; }

        public int Categoria { get; set; }

        public virtual UnidadeCadastro UnidadeMedida { get; set; }

        [NotMapped]
        public List<UnidadeCadastro> Unidades { get; set; }
    }
}