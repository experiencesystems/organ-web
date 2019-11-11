using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Controles
{
    [Table("tbControle")]
    public class Controle : OrganRepository<Controle>
    {
        [Key]
        public int Id { get; set; }

        public bool Status { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(300, MinimumLength = 10)]
        public string Desc { get; set; }

        [Required]
        [Display(Name = "Eficiência")]
        [Range(0, 999.99, ErrorMessage = "Esse campo deve conter valores até 999,99")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "Digite um valor válido com até duas casas decimais, como 99,99")]
        public double Efic { get; set; }

        [Required]
        [Display(Name = "Número de liberações")]
        public int NumLiberacoes { get; set; }
    }
}