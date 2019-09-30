using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Financeiro
{
    [Table("tbDadosBancarios")]
    public class DadosBancario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 9999)]
        public int CVV { get; set; }

        [Required]
        public int Banco { get; set; }

        [Required]
        public Int64 NumCartao { get; set; }

        [Required]
        public DateTime Validade { get; set; }

        [Required]
        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }

        public virtual Pessoa.Pessoa Pessoa { get; set; }
    }
}