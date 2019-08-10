using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Área : Repository<Área>
    {
        [Key]
        public int IDArea { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        public int IDSolo { get; set; } //fk

        public bool Disponibilidade { get; set; } // ???

        public int IDFazenda { get; set; } //fk

        


        //Funcionario - n
    }
}