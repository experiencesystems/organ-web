using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Safras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class CreatePlantioViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        public int Sistema { get; set; }

        [Required]
        public int Tipo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data da colheita")]
        public DateTime Colheita { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de início")]
        public DateTime Inicio { get; set; }

        [Required]
        [Display(Name = "Áreas utilizadas")]
        public int[] IdArea { get; set; }

        [Required]
        [Display(Name = "Semente")]
        public int IdEstoque { get; set; }

        [Required]
        public double Quantidade { get; set; }

        public IEnumerable<Area> Areas { get; set; }
        public IEnumerable<Semente> Sementes { get; set; }
        public IEnumerable<SelectListItem> Periodos { get; set; }
        public IEnumerable<SelectListItem> Sistemas { get; set; }
    }
}