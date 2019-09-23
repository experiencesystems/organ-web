using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Controles
{
    [Table("tbDoenca")]
    public class Doenca : Repository<Doenca>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Nome { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string Sintomas { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string Tratamento { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        [StringLength(300, MinimumLength = 10)]
        public string Descricao { get; set; }
    }
}