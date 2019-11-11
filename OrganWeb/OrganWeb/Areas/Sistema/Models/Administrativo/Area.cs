using OrganWeb.Areas.Sistema.Models.zRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.Administrativo
{
    [Table("tbArea")]
    public class Area : AreaRepository
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Disponibilidade")]
        public int Disp { get; set; }
        
        [Required]
        [Display(Name = "Tamanho em ha")]
        public int Tamanho { get; set; }

        [Required]
        [ForeignKey("Solo")]
        [Display(Name = "Solo")]
        public int IdSolo { get; set; }

        public virtual Solo Solo { get; set; }
        public IEnumerable<Solo> Solos { get; set; }

        [NotMapped]
        public readonly List<SelectListItem> Disponibilidades = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Disponível", Value = "1" },
            new SelectListItem() { Text = "Em uso", Value = "2" },
            new SelectListItem() { Text = "Indisponível", Value = "3" }
            };
    }
}