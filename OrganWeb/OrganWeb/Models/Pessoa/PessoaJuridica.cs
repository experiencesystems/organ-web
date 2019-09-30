using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Pessoa
{
    [Table("tbPessoaJuridica")]
    public class PessoaJuridica
    {
        [Key]
        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string RazaoSocial { get; set; }

        [Required]
        public Int64 CNPJ { get; set; }

        [Required]
        public Int64 IE { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}