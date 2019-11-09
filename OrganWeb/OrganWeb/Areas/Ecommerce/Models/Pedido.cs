using OrganWeb.Models.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models
{
    [Table("tbPedido")]
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Anuncio")]
        public int IdAnuncio { get; set; }

        [ForeignKey("Usuario")]
        public string IdUsuario { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Required]
        public int Status { get; set; }
        //TODO: lista de status pedido
        public virtual Anuncio Anuncio { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}