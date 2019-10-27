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
        [StringLength(100, MinimumLength = 5)]
        public string NomeTitular { get; set; }

        [Required]
        [Range(1, 9999)]
        public int CVV { get; set; }

        [Required]
        public int Banco { get; set; }

        [Required]
        [CreditCard]
        [Display(Name = "Número do cartão")]
        public Int64 NumCartao { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Validade { get; set; }

        [Required]
        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }

        public virtual Pessoa.Pessoa Pessoa { get; set; }
    }
}