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

        //Funcionario = n-n
        //Tarefa = n-n
    }
}