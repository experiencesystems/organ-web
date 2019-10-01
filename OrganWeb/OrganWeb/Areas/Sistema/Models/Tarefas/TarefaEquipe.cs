using OrganWeb.Areas.Sistema.Models.Funcionarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Tarefas
{
    [Table("tbTarefaEquipe")]
    public class TarefaEquipe
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Tarefa")]
        public int IdTarefa { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Equipe")]
        public int IdEquipe { get; set; }

        public virtual Tarefa Tarefa { get; set; }
        public virtual Equipe Equipe { get; set; }
    }
}