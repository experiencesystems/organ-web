using OrganWeb.Areas.Ecommerce.Models.Financeiro;
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
    public class Pedido : EcommerceRepository<Pedido>
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Usuario")]
        public string IdUsuario { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public double ValFrete { get; set; }

        [Required]
        [ForeignKey("Endereco")]
        [Display(Name = "CEP")]
        [RegularExpression(@"[0-9]{5}[\d]{3}", ErrorMessage = "Digite um CEP válido somente com números")]
        [StringLength(8, MinimumLength = 8)]
        public string CEPEntrega { get; set; }

        [Required]
        [Display(Name = "Número")]
        public int NumEntrega { get; set; }

        [Required]
        [ForeignKey("Pagamento")]
        public int IdPagamento { get; set; }

        [StringLength(50)]
        [Display(Name = "Complemento")]
        public string CompEntrega { get; set; }

        [NotMapped]
        public virtual Anuncio Anuncio { get; set; }
        public virtual Endereco.Endereco Endereco { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public virtual Pagamento Pagamento { get; set; }

        [NotMapped]
        public double Subtotal { get; set; }

        [NotMapped]
        public List<Carrinho> Carrinhos { get; set; }

        [NotMapped]
        public readonly List<SelectListItem> StatusPedido = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Aguardando confirmação", Value = "1" },
            new SelectListItem() { Text = "Venda confirmada", Value = "2" },
            new SelectListItem() { Text = "Enviado para a transportadora", Value = "3" },
            new SelectListItem() { Text = "Entregue", Value = "4" },
            new SelectListItem() { Text = "Entrega confirmada", Value = "5" },
            new SelectListItem() { Text = "Cancelado", Value = "6" },
            new SelectListItem() { Text = "Rejeitado", Value = "7" }
            };
    }
}