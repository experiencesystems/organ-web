using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Funcionario : Repository<Funcionario>
    {
        [Key]
        public int FuncionarioID { get; set; }
        public string Nome { get; set; }
        public Int64 CPF { get; set; }
        public string RG { get; set; }

        //TODO: Verificar se Status é FK
        public int Status { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }

        //FK
        public int CargoID { get; set; }

        public decimal Salario { get; set; }
        public int LocalizacaoID { get; set; }
        public string GrauInstrucao { get; set; }
        public DateTime DataContratacao { get; set; }
        public string TipoContratacao { get; set; }
        public string PeriodoContratacao { get; set; }

        //FK
        public int UsuarioID { get; set; }

        //Tarefa = n-n
        //Telefone = n-n
        //Cargo = 1-n
        //Equipe = n-n
    }
}