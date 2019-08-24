using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbMonitoramento")]
    public class Monitoramento : Repository<Monitoramento>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [StringLength(300, MinimumLength = 10)]
        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Required]
        public bool Status { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Resultado { get; set; }
    }

    [Table("tbMonitoramentoTarefa")]
    public class MonitoramentoTarefa
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Tarefa")]
        public int IdTarefa { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Monitoramento")]
        public int IdMonitoramento { get; set; }

        public virtual Monitoramento Monitoramento { get; set; }
        public virtual Tarefa Tarefa { get; set; }
    }
}