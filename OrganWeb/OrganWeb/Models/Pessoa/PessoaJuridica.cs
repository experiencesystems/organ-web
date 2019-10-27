using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Pessoa
{
    [Table("tbPessoaJuridica")]
    public class PessoaJuridica : Repository<PessoaJuridica>
    {
        [Key]
        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }

        [Required]
        [Display(Name = "Razão Social")]
        [StringLength(100, MinimumLength = 10)]
        public string RazaoSocial { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{2}\.?[0-9]{3}\.?[0-9]{3}\/?[0-9]{4}\-?[0-9]{2}", ErrorMessage = "Digite um CPNJ válido somente com números")]
        public Int64 CNPJ { get; set; }

        [Required]
        [Display(Name = "Inscrição Estadual")]
        public Int64 IE { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}