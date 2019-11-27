using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque
{
    [Table("vwPlantio")]
    public class VwPlantio : OrganRepository<VwPlantio>
    {
        [Key]
        public int Id { get; set; }
        public string Plantio { get; set; }
        public int Sistema { get; set; }
        public int Tipo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Colheita { get; set; }
        public string Areas { get; set; }
        public string Itens { get; set; }
        public string Funcionarios { get; set; }

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