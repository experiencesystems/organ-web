using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbTarefa")]
    public class Tarefa : Repository<Tarefa>
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Título")]
        public string Titulo { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public bool StatusTarefa { get; set; }
        public int Prioridade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public virtual List<Funcionario> Funcionarios { get; set; }
        //Plantio = n-n
        //Item = n-n
        //Funcionário = n-n
        //Monitoramento = n-n
    }
}