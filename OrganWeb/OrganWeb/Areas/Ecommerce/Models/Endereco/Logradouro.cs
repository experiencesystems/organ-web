using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Endereco
{
    [Table("tbLogradouro")]
    public class Logradouro : Repository<Logradouro>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 5)]
        public string Nome { get; set; }

        [Required]
        [ForeignKey("Bairro")]
        public int IdBairro { get; set; }

        public virtual Bairro Bairro { get; set; }
    }
}