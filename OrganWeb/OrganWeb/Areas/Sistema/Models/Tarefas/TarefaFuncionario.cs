using OrganWeb.Areas.Sistema.Models.Funcionarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Tarefas
{
    [Table("tbTarefaFuncionario")]
    public class TarefaFuncionario
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Funcionario")]
        public int IdFunc { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Tarefa")]
        public int IdTarefa { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Tarefa Tarefa { get; set; }
    }
}