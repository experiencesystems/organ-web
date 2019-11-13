using OrganWeb.Areas.Ecommerce.Models.Usuarios;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using OrganWeb.Areas.Ecommerce.Models.zRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Ecommerce.Models.Vendas
{
    [Table("tbPedido")]
    public class Pedido : PedidoRepository
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

        [NotMapped]
        public readonly List<SelectListItem> StatusPedido = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Aguardando confirmação", Value = "1" },
            new SelectListItem() { Text = "Aguardando dados do cliente", Value = "2" },
            new SelectListItem() { Text = "Dados enviados", Value = "3" },
            new SelectListItem() { Text = "Venda confirmada", Value = "4" },
            new SelectListItem() { Text = "Enviado para a transportadora", Value = "5" },
            new SelectListItem() { Text = "Entregue", Value = "6" },
            new SelectListItem() { Text = "Entrega confirmada", Value = "7" },
            new SelectListItem() { Text = "Cancelado", Value = "8" },
            new SelectListItem() { Text = "Rejeitado", Value = "9" }
            };
    }
}