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
        [RegularExpression (@"[0-9]{3}[0-9]{3}[0-9]{3}[0-9]{2}", ErrorMessage = "CPF Inválido!")]
        public Int64 CPF { get; set; }
        //TODO: arrumar o datetime
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