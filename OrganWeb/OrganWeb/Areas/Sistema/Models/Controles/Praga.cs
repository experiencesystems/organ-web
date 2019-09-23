using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Controles
{
    [Table("tbPraga")]
    public class Praga : Repository<Praga>
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome popular")]
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3)]
        public string NomePopular { get; set; }

        [Display(Name = "Nome científico")]
        [StringLength(75, MinimumLength = 5)]
        public string NomeCientifico { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(300, MinimumLength = 10)]
        public string Descricao { get; set; }
    }
}