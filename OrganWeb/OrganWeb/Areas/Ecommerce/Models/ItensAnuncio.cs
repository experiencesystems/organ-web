using OrganWeb.Areas.Sistema.Models.Armazenamento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models
{
    public class ItensAnuncio
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Anuncio")]
        public int IdAnuncio { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        public int Quantidade { get; set; }
        public double Desconto { get; set; }

        public virtual Anuncio Anuncio { get; set; }
        public virtual Estoque Estoque { get; set; }
    }
}