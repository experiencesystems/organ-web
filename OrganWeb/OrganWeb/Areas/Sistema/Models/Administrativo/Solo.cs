using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Administrativo
{
    [Table("tbSolo")]
    public class Solo : Repository<Solo>
    {
        [Key]
        public int IDSolo{ get; set; }

        [StringLength(75, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Tipo de Solo")]
        [StringLength(50, MinimumLength = 3)]
        public string Tipo { get; set; }
       
        [Required]
        [Range(0.01, 999.99)]
        [Display(Name = "Incidência Solar")]
        public double IncSolar { get; set; }

        [Required]
        [Range(0.01, 999.99)]
        [Display(Name = "Incidência do Vento")]
        public double IncVento { get; set; }

        [Required]
        [Range(0.01, 999.99)]
        public double Acidez { get; set; }
    }
}