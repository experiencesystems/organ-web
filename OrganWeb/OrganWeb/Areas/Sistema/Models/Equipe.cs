using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbEquipe")]
    public class Equipe : Repository<Equipe>
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<Funcionario> Funcionarios { get; set; }
        public List<Tarefa> Tarefas { get; set; }
        //Funcionario = n-n
        //Tarefa = n-n
    }
}