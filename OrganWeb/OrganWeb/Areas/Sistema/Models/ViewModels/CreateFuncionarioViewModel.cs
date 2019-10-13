using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class CreateFuncionarioViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cargo { get; set; }
        [Required]
        public double Salario { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string RG { get; set; }
        [Required]
        public string DataNascimento { get; set; }
        [Required]
        public string Telefones { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Rua { get; set; }
        [Required]
        public string BCF { get; set; }

        public IEnumerable<Cargo> Cargos{ get; set; }
        public IEnumerable<Funcionario> Funcionarios { get; set; }
        public IEnumerable<VwPessoa> Pessoas { get; set; }
        public IEnumerable<VwFuncionario> vwFuncionarios { get; set; }
        public IEnumerable<VwEndereco> Enderecos { get; set; }
    }
}