using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbFuncionario")]
    public class Funcionario : Repository<Funcionario>
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        //TODO: Colocar verificação de CPF
        public Int64 CPF { get; set; }
        public string RG { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public decimal Salario { get; set; }
        public string GrauInstrucao { get; set; }
        public DateTime DataContratacao { get; set; }
        public string TipoContratacao { get; set; }
        public int PeriodoContratacao { get; set; }

        [ForeignKey("Cargo")]
        public int IdCargo { get; set; }

        [ForeignKey("Localizacao"), Column(Order = 0)]
        public char CEP { get; set; }
        [ForeignKey("Localizacao"), Column(Order = 1)]
        public int Numero { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        public virtual List<Tarefa> Tarefas { get; set; }
        public virtual List<Equipe> Equipes { get; set; }
        public virtual List<Telefone> Telefones { get; set; }

        public virtual Cargo Cargo { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Localizacao Localizacao { get; set; }
    }
}