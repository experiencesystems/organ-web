using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Controles
{
    [Table("tbControle")]
    public class Controle : Repository<Controle>
    {
        [Key]
        public int Id { get; set; }

        public bool Status { get; set; }

        [StringLength(300, MinimumLength = 10)]
        public string Desc { get; set; }

        [Required]
        [Range(0.01, 999.99)]
        public double Efic { get; set; }

        [Required]
        public int NumLiberacoes { get; set; }
    }
}