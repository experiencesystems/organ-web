using OrganWeb.Areas.Sistema.Models.Armazenamento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Tarefas
{
    [Table("tbItensTarefa")]
    public class ItensTarefa
    {
        [Required]
        public double QtdUsada { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Tarefa")]
        public int IdTarefa { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        public virtual Tarefa Tarefa { get; set; }
        public virtual Estoque Estoque { get; set; }
    }
}