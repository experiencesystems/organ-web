using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models
{
    [Table("tbEntrega")]
    public class Entrega
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("VendaAnuncio")]
        public int IdVA { get; set; }

        [Required]
        [Display(Name = "Valor do frete")]
        public double ValorFrete { get; set; }

        [Required] //todo: testar se double valida igual decimal
        [Display(Name = "Desconto do frete")]
        public double DescFrete { get; set; }

        public virtual VendaAnuncio VendaAnuncio { get; set; }
    }
}