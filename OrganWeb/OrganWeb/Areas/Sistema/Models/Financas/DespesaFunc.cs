using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using OrganWeb.Areas.Sistema.Models.zRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Financas
{
    [Table("tbDespesaFunc")]
    public class DespesaFunc : DespesaFuncRepository
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Despesa")]
        public int IdDespesa { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Funcionario")]
        [Display(Name = "Funcionário")]
        public int IdFunc { get; set; }

        public virtual Despesa Despesa { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        public IEnumerable<VwFuncionario> Funcionarios { get; set; }
    }
}