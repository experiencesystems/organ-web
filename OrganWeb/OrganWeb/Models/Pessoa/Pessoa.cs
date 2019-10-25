using OrganWeb.Models.Banco;
using OrganWeb.Models.Endereco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Pessoa
{
    [Table("tbPessoa")]
    public class Pessoa : Repository<Pessoa>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int NumeroEndereco { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string CompEndereco { get; set; }
     
        [Required]
        [ForeignKey("Endereco")]
        [RegularExpression(@"[0-9]{5}[\d]{3}", ErrorMessage = "Digite um CEP válido somente com números")]
        public string CEP { get; set; }

        public virtual Endereco.Endereco Endereco { get; set; }
    }
}