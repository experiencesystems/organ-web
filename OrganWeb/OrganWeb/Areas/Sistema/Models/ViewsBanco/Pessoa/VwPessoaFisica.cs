using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa
{
    [Table("vwPessoaFisica")]
    public class VwPessoaFisica
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string DataNascimento { get; set; }
        public string Telefones { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
    }
}