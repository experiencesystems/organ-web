using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Tarefa : Repository<Tarefa>
    {
        [Key]
        public int TarefaID { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        //TODO: Verificar se Status é FK
        public int Status { get; set; }

        public DateTime DataEmissao { get; set; }
        public int Prioridade { get; set; }
        public int FuncionarioID { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }

        //Plantio = n-n
        //Item = n-n
        //Funcionário = n-n
        //Monitoramento = n-n
    }
}