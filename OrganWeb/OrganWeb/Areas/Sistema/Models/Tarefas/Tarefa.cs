using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Tarefas
{
    [Table("tbTarefa")]
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [StringLength(300, MinimumLength = 10)]
        [Display(Name = "Descrição")]
        public string Desc { get; set; }

        public bool Status { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataEmissao { get; set; }

        [Required]
        public int Prioridade { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataFim { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataInicio { get; set; }

        public string Relatorio { get; set; }
    }
}