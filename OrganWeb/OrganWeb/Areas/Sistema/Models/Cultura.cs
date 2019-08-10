using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Cultura : Repository<Cultura>
    {
        [Key]
        public int IDCultura { get; set; }


        [Display(Name = "Nome da Cultura")]
        public string NomeCultura{ get; set; } 
     

        //Funcionario - n
    }
}