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

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Sobrenome { get; set; }

        [Required]
        //[RegularExpression("/^\\d{3}\\d{3}\\d{3}\\d{2}$/", ErrorMessage = "Verifique o CPF.")]
        public Int64 CPF { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9)]
        public string RG { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(0.01, 99999.99)]
        [Display(Name = "Salário")]
        public decimal Salario { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Grau de instrução")]
        public string GrauInstrucao { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de contratação")]
        public DateTime DataContratacao { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Tipo de contratação")]
        public string TipoContratacao { get; set; }

        [Required]
        [Display(Name = "Período de contratação")]
        public int PeriodoContratacao { get; set; }

        //TODO: checkbox p mes ou ano e dependendo do q estiver checado registra
        [Required]
        public bool MesAno { get; set; }

        [Required]
        [Display(Name = "Cargo")]
        public int IdCargo { get; set; }

        [Required]
        [ForeignKey("Localizacao")]
        [Column(Order = 1)]
        public string CEP { get; set; }

        [Required]
        [ForeignKey("Localizacao")]
        [Column(Order = 2)]
        public int Numero { get; set; }

        //**************** DEFINIDO VIA FLUENT API ****************
        //https://stackoverflow.com/questions/55617432/multiplicity-conflict-while-setting-up-entity-framework?noredirect=1&lq=1
        [Required]
        [ForeignKey("User")]
        [Display(Name = "Usuário")]
        public int IdUsuario { get; set; }

        //public virtual List<Tarefa> Tarefas { get; set; }
        //public virtual List<Equipe> Equipes { get; set; }
        
        public virtual Cargo Cargo { get; set; }
        public virtual User User { get; set; }
        public virtual Localizacao Localizacao { get; set; }
    }

    [Table("tbFuncionarioTelefone")]
    public class FuncionarioTelefone
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Telefone")]
        public int IdTel { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Funcionario")]
        public int IdFunc { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Telefone Telefone { get; set; }
    }
}