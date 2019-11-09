using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Models;
using OrganWeb.Models.Banco;

namespace OrganWeb.Areas.Sistema.Models.Administrativo
{
    [Table("tbSolo")]
    public class Solo : Repository<Solo>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Tipo de Solo")]
        public int Tipo { get; set; }
       
        [Required]
        [Display(Name = "Incidência Solar")]
        [Range(0, 999.99, ErrorMessage = "Esse campo deve conter valores até 999,99")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "Digite um valor válido com até duas casas decimais, como 99,99")]
        public decimal IncSolar { get; set; }
        
        [Required]
        [Display(Name = "Incidência do Vento")]
        [Range(0, 999.99, ErrorMessage = "Esse campo deve conter valores até 999,99")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "Digite um valor válido com até duas casas decimais, como 99,99")]
        public decimal IncVento { get; set; }

        [NotMapped]
        public readonly List<SelectListItem> Tipos = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Argiloso", Value = "1" },
            new SelectListItem() { Text = "Arenoso", Value = "2" },
            new SelectListItem() { Text = "Humoso", Value = "3" },
            new SelectListItem() { Text = "Calcário", Value = "4" },
            };
    }
}