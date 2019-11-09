using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Endereco
{
    [Table("tbBairro")]
    public class Bairro : Repository<Bairro>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Nome { get; set; }

        [Required]
        [ForeignKey("Cidade")]
        public int IdCidade { get; set; }

        public virtual Cidade Cidade { get; set; }
    }
}