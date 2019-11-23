using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa
{
    [Table("vwFuncionario")]
    public class VwFuncionario : OrganRepository<VwFuncionario>
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Funcao { get; set; }
        public string Telefones { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public readonly List<SelectListItem> Cargos = new List<SelectListItem>()
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