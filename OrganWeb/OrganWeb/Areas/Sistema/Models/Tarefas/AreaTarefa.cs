using OrganWeb.Areas.Sistema.Models.Administrativo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Tarefas
{
    [Table("tbAreaTarefa")]
    public class AreaTarefa
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Tarefa")]
        public int IdTarefa { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Area")]
        public int IdArea { get; set; }

        public virtual Tarefa Tarefa { get; set; }
        public virtual Area Area { get; set; }
    }
}