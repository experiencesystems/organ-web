using OrganWeb.Areas.Ecommerce.Models.zBanco;
using OrganWeb.Areas.Ecommerce.Models.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [NotMapped]
        public readonly List<SelectListItem> Categorias = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Frutas", Value = "1" },
            new SelectListItem() { Text = "Verduras", Value = "2" },
            new SelectListItem() { Text = "Grãos", Value = "3" },
            new SelectListItem() { Text = "Oleaginosas", Value = "4" },
            new SelectListItem() { Text = "Hortaliças", Value = "5" },
            new SelectListItem() { Text = "Legumes", Value = "6" },
            new SelectListItem() { Text = "Orgânicos", Value = "7" },
            new SelectListItem() { Text = "Fertilizantes", Value = "8" },
            new SelectListItem() { Text = "Transgênicos", Value = "9" },
            new SelectListItem() { Text = "Sementes", Value = "10" },
            new SelectListItem() { Text = "Mudas/Ramas", Value = "11" },
            new SelectListItem() { Text = "Tratores Agrícolas", Value = "12" },
            new SelectListItem() { Text = "Carrocerias", Value = "13" },
            new SelectListItem() { Text = "Caminhões", Value = "14" },
            new SelectListItem() { Text = "Colheitadeiras", Value = "15" },
            new SelectListItem() { Text = "Equipamentos", Value = "16" },
            new SelectListItem() { Text = "Utensílios Agropecuários", Value = "17" },
            new SelectListItem() { Text = "Químicos", Value = "18" }
            };
    }
}