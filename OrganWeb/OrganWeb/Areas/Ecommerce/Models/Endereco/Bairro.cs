using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Endereco
{
    [Table("tbBairro")]
    public class Bairro : EcommerceRepository<Bairro>
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Bairro")]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [ForeignKey("Cidade")]
        public int IdCidade { get; set; }

        public virtual Cidade Cidade { get; set; }
    }
}