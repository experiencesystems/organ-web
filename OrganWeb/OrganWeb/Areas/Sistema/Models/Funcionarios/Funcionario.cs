using OrganWeb.Areas.Sistema.Models.zBanco;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.Funcionarios
{
    [Table("tbFuncionario")]
    public class Funcionario : OrganRepository<Funcionario>
    {
        [Key]
        public int Id { get; set; }
        public bool Status { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 2)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Função")]
        public int Funcao { get; set; }

        [NotMapped]
        public Telefone.Telefone Telefone { get; set; }

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