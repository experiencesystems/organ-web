using OrganWeb.Models;
using OrganWeb.Models.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models
{
    [Table("tbWishlist")]
    public class Wishlist
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Usuario")]
        public string IdUsuario { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Anuncio")]
        public int IdAnuncio { get; set; }

        public virtual Anuncio Anuncio { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}