using OrganWeb.Areas.Sistema.Models.Funcionarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Controles
{
    [Table("tbFuncControle")]
    public class FuncControle
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Funcionario")]
        public int IdFunc { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Controle")]
        public int IdControle { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Controle Controle { get; set; }
    }
}