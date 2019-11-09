using OrganWeb.Models.Telefone;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Funcionarios
{
    [Table("tbTelFunc")]
    public class TelFunc
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Funcionario")]
        public int IdFunc { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Telefone")]
        public int IdTelefone { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Telefone Telefone { get; set; }
    }
}