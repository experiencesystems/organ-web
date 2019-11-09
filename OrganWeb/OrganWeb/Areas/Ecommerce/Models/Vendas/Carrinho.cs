using OrganWeb.Areas.Sistema.Models.Usuario;
using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Vendas
{
    [Table("tbCarrinho")]
    public class Carrinho
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Usuario")]
        public string IdUsuario { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Anuncio")]
        public int IdAnuncio { get; set; }
        
        [Required]
        [Display(Name = "Quantidade")]
        public int Qtd { get; set; }

        [Required]
        public int Status { get; set; }
        //TODO: lista de status para anúncio
        public virtual Anuncio Anuncio { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}