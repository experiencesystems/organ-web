using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Pessoa
{
    [Table("tbPessoaFisica")]
    public class PessoaFisica
    {
        [Key]
        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }

        [Required]
        //TODO: Validação CPF
        public Int64 CPF { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 6)]
        public string RG { get; set; }

        [Required]
        public DateTime DataNasc { get; set; }

        [Required]
        public string Foto { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}