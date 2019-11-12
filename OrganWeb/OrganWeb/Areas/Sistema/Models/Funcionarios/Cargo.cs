using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.zBanco;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Funcionarios
{
    [Table("tbCargo")]
    public class Cargo : OrganRepository<Cargo>
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Nível")]
        public int Nivel { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 1)]
        public string Nome { get; set; }

        [NotMapped]
        public readonly List<SelectListItem> Niveis = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Gerência", Value = "1" },
            new SelectListItem() { Text = "Coordenação", Value = "2" },
            new SelectListItem() { Text = "Supervisão", Value = "3" },
            new SelectListItem() { Text = "Analista", Value = "4" },
            new SelectListItem() { Text = "Assistente", Value = "5" },
            new SelectListItem() { Text = "Auxiliar", Value = "6" },
            new SelectListItem() { Text = "Estagiário", Value = "7" },
            new SelectListItem() { Text = "Aprendiz", Value = "8" }
            };
    }
}