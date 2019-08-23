using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbEstoque")]
    public class Estoque : Repository<Estoque>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0.01, 99999.99)]
        public double Quantidade { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 1)]
        public string UnidadeMedida { get; set; }
    }
}