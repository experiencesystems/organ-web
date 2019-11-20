using OrganWeb.Areas.Ecommerce.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Vendas
{
    [Table("tbHistCarrinho")]
    public class HistCarrinho
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public string IdUsuario { get; set; }

        [StringLength(30)]
        [Required(AllowEmptyStrings = false)]
        public string NomeAnuncio { get; set; }

        [Required]
        public int Qtd { get; set; }

        public virtual ApplicationUser Usuario { get; set; }
    }
}