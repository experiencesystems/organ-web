using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrganWeb.Models.Endereco;
using OrganWeb.Models.Telefone;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class CreateFuncionarioViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cargo { get; set; }
        [Required]
        public double Salario { get; set; }
        [Required]
        public long CPF { get; set; }
        [Required]
        public string RG { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Telefone { get; set; }

        [Required]
        [Display(Name = "Tipo do telefone (Casa, Trabalho, etc.)")]
        [StringLength(20, MinimumLength = 2)]
        public string TipoTelefone { get; set; }

        public int DDD { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)]
        [RegularExpression(@"[0-9]{5}[\d]{3}", ErrorMessage = "Digite um CEP válido somente com números")]
        public string CEP { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Rua { get; set; }

        [Required]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string Complemento { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Cidade { get; set; }

        [Required]
        public int Estado { get; set; }
        
        public IEnumerable<Estado> Estados { get; set; }
        public IEnumerable<Funcionario> Funcionarios { get; set; }
        public IEnumerable<Cargo> Cargos { get; set; }
        public IEnumerable<DDD> DDDs { get; set; }
    }
}