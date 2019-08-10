using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        //TODO: Colocar verificação de CPF
        public Int64 CPF { get; set; }
        public string RG { get; set; }

        //TODO: Verificar se Status é FK
        public int Status { get; set; }
        public DateTime DataNascimento { get; set; }

        [ForeignKey("Telefones")]
        public int TelefoneID { get; set; }
        public string Email { get; set; }

        [ForeignKey("Cargo")]
        public int CargoID { get; set; }

        public decimal Salario { get; set; }

        //TODO: Tirar comentário quando tiver classe Localizacao
        //[ForeignKey("Localizacao")]
        public int LocalizacaoID { get; set; }
        public string GrauInstrucao { get; set; }
        public DateTime DataContratacao { get; set; }
        public string TipoContratacao { get; set; }
        public string PeriodoContratacao { get; set; }

        //[ForeignKey("Usuario")]
        public int UsuarioID { get; set; }

        public List<Tarefa> Tarefas { get; set; }
        public List<Telefone> Telefones { get; set; }
        public Cargo Cargo { get; set; }
        public List<Equipe> Equipes { get; set; }
        //public Usuario Usuario { get; set; }
        //public Localizacao Localizacao { get; set; }

        //Tarefa = n-n
        //Telefone = n-n
        //Cargo = 1-n
        //Equipe = n-n
    }
}