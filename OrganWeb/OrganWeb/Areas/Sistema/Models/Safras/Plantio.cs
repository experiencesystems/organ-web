using OrganWeb.Areas.Sistema.Models.Administrativo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.Safras
{
    [Table("tbPlantio")]
    public class Plantio : PlantioRepository
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        public int Sistema { get; set; }

        [Required]
        [Display(Name = "Data da colheita")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataColheita { get; set; }

        [Required]
        [Display(Name = "Data de início")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        [Required]
        [Display(Name = "Tipo do plantio")]
        public int TipoPlantio { get; set; }

        [NotMapped]
        public double Porcentagem { get; set; }

        [NotMapped]
        public string NomeAreas { get; set; }

        public bool Status { get; set; }

        [NotMapped]
        public List<AreaPlantio> AreaPlantios { get; set; }

        [NotMapped]
        public readonly List<SelectListItem> Sistemas = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Convencional", Value = "1" },
            new SelectListItem() { Text = "Mínimo", Value = "2" },
            new SelectListItem() { Text = "Plantio direto", Value = "3" },
            new SelectListItem() { Text = "Sobre-semeadura", Value = "4" }
            };
        [NotMapped]
        public readonly List<SelectListItem> Periodos = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Safra", Value = "1" },
            new SelectListItem() { Text = "Entressafra (safrinha)", Value = "2" }
            };
    }
}