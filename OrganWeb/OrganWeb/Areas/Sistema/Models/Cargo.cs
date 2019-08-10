using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Cargo : Repository<Cargo>
    {
        [Key]
        public int IDCargo { get; set; }

        [Display(Name = "Nível")]
        public int Nivel { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        public virtual List<Funcionario> Funcionarios { get; set; }
    }
}