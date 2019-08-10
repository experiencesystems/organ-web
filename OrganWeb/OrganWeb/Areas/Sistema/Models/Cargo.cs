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
        public int CargoID { get; set; }
        public int Nivel { get; set; }
        public string Nome { get; set; }

        public virtual List<Funcionario> Funcionarios { get; set; }
    }
}