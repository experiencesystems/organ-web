using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Equipe : Repository<Equipe>
    {
        [Key]
        public int EquipeID { get; set; }
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public List<Funcionario> Funcionarios { get; set; }
        public List<Tarefa> Tarefas { get; set; }
        //Funcionario = n-n
        //Tarefa = n-n
    }
}