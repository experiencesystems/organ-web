using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Controles
{
    [Table("tbItensControle")]
    public class ItensControle : OrganRepository<ItensControle>
    {
        [Required]
        public double QtdUsada { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Controle")]
        public int IdControle { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        public virtual Controle Controle { get; set; }
        public virtual Estoque Estoque { get; set; }
    }
}