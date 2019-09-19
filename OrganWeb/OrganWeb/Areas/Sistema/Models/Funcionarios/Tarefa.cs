using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Funcionarios
{
    [Table("tbTarefa")]
    public class Tarefa : Repository<Tarefa>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Título")]
        [StringLength(100, MinimumLength = 3)]
        public string Titulo { get; set; }
        
        [Display(Name = "Descrição")]
        [StringLength(500, MinimumLength = 10)]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Status")]
        public bool StatusTarefa { get; set; }

        [Required]
        public int Prioridade { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFim { get; set; }
        //Plantio = n-n
        //Item = n-n
        //Funcionário = n-n
        //Monitoramento = n-n
    }

    [Table("tbTarefaFuncionario")]
    public class TarefaFuncionario
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Tarefa")]
        public int IdTarefa { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Funcionario")]
        public int IdFunc { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Tarefa Tarefa { get; set; }
    }
}