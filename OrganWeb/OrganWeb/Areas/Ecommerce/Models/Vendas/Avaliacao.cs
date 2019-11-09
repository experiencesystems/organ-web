using System;
using OrganWeb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Usuario;

namespace OrganWeb.Areas.Ecommerce.Models.Vendas
{
    public class Avaliacao
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Anuncio")]
        public int IdAnuncio { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Usuario")]
        public string IdUsuario { get; set; }

        public int Nota { get; set; }
        public bool Like { get; set; }

        public virtual Anuncio Anuncio { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}