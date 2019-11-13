using OrganWeb.Areas.Ecommerce.Models.Usuarios;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Vendas
{
    [Table("tbPedido")]
    public class Pedido : EcommerceRepository<Pedido>
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Carrinho")]
        public int IdAnuncio { get; set; }

        [ForeignKey("Carrinho")]
        public string IdUsuario { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [NotMapped]
        public double Subtotal { get; set; }

        [Required]
        public int Status { get; set; }
        //TODO: lista de status pedido
        [NotMapped]
        public virtual Anuncio Anuncio { get; set; }
        public virtual Carrinho Carrinho { get; set; }
        [NotMapped]
        public virtual ApplicationUser Usuario { get; set; }
        [NotMapped]
        public List<Carrinho> Carrinhos { get; set; }
    }
}