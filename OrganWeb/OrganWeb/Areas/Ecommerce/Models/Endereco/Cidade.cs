using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Endereco
{
    [Table("tbCidade")]
    public class Cidade : EcommerceRepository<Cidade>
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Cidade")]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [ForeignKey("Estado")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        public virtual Estado Estado { get; set; }

        [NotMapped]
        public List<Estado> Estados { get; set; }
    }
}