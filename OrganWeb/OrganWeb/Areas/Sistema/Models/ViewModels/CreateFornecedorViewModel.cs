using System;
using System.Collections.Generic;
using System.Linq;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class CreateFornecedorViewModel
    {

        [Key]
        public int Id { get; set; }

        public bool Status { get; set; }

        [Required]
        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }
        [Required]
        public string NomeFantasia { get; set; }
        [Required]
        public string RazaoSocial { get; set; }
        [Required]
        public string CNPJ { get; set; }
        [Required]
        //n sei o name display disso dsahduajhsd
        public string IE { get; set; }
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















        public IEnumerable<VwFornecedor> VwFornecedors { get; set; }
        public IEnumerable<VwPessoaJuridica> PessoaJuridicas { get; set; }
        public IEnumerable<VwEndereco> Enderecos { get; set; }
        public IEnumerable<Fornecedor> Fornecedors { get; set; }
    }
}