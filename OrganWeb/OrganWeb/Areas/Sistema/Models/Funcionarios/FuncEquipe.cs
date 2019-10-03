using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Funcionarios
{
    [Table("tbFuncEquipe")]
    public class FuncEquipe : Repository<FuncEquipe>
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Funcionario")]
        public int IdFunc { get; set; }

        [Key,Column(Order = 2)]
        [ForeignKey("Equipe")]
        public int IdEquipe { get; set; }
        
        public bool LIDER { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Equipe Equipe { get; set; }
    }
}