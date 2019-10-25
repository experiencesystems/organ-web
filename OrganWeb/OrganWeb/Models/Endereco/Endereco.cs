using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Endereco
{
    [Table("tbEndereco")]
    public class Endereco
    {
        [Key]
        [RegularExpression(@"[0-9]{5}[\d]{3}", ErrorMessage = "Digite um CEP válido somente com números")]
        [StringLength(8, MinimumLength = 8)]
        public string CEP { get; set; }

        [Required]
        [ForeignKey("Logradouro")]
        public int IdRua { get; set; }

        public virtual Logradouro Logradouro { get; set; }
    }
}