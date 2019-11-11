using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
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
    }
}