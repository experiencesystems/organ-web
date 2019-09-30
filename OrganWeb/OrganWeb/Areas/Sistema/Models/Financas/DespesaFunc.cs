using OrganWeb.Areas.Sistema.Models.Funcionarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Financas
{
    [Table("tbDespesaFunc")]
    public class DespesaFunc
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Despesa")]
        public int IdDespesa { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Funcionario")]
        public int IdFunc { get; set; }

        public virtual Despesa Despesa { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}