using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models.Banco;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbEstoque")]
    public class Estoque : Repository<Estoque>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        [Range(0.01, 999.99, ErrorMessage = "Esse campo deve conter valores até 999,99")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "Digite um valor válido com até duas casas decimais, como 99,99")]
        public double Qtd { get; set; }

        [Required]
        [Display(Name = "Unidade de medida")]
        public int UM { get; set; }

        [Required]
        [Display(Name = "Valor unitário")]
        [Range(0.01, 999.99, ErrorMessage = "Esse campo deve conter valores até 999,99")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "Digite um valor válido com até duas casas decimais, como 99,99")]
        public double ValorUnit { get; set; }

        [NotMapped]
        public readonly List<SelectListItem> UnidadesDeMedida = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Litros", Value = "1" },
            new SelectListItem() { Text = "Kilos", Value = "2" },
            new SelectListItem() { Text = "Gramas", Value = "3" },
            new SelectListItem() { Text = "Unidades", Value = "4" },
            new SelectListItem() { Text = "Mililitros", Value = "5" }
            };
    }
}