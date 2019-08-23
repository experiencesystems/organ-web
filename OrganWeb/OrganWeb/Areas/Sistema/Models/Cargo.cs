using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbCargo")]
    public class Cargo : Repository<Cargo>
    {
        [Key]
        public int Id { get; set; }

        //TODO: Nível varchar
        [Display(Name = "Nível")]
        public int Nivel { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        public virtual List<Funcionario> Funcionarios { get; set; }
    }
}