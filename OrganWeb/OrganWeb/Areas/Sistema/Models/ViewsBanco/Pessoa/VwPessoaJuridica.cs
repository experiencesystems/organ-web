using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa
{
    [Table("vwPessoaJuridica")]
    public class VwPessoaJuridica
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string IE { get; set; }
        public string Telefones { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
    }
}