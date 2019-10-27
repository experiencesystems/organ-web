using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models
{
    [Table("tbAnuncio")]
    public class Anuncio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 10)]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Desc { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public byte[] Foto { get; set; }

        [Range(0.00, 100.00)]
        public decimal Desconto { get; set; }
    }
}