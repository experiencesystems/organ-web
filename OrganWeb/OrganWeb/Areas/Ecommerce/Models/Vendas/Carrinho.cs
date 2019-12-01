using OrganWeb.Areas.Ecommerce.Models.Usuarios;
using OrganWeb.Areas.Ecommerce.Models.zRepositories;
using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Ecommerce.Models.Vendas
{
    [Table("tbCarrinho")]
    public class Carrinho : CarrinhoRepository
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

        public bool Status { get; set; }

        public virtual Anuncio Anuncio { get; set; }
        public virtual ApplicationUser Usuario { get; set; } = new ApplicationUser();

        [NotMapped]
        public readonly List<SelectListItem> StatusList = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Ativo", Value = "1" },
            new SelectListItem() { Text = "Compra realizada", Value = "2" },
            new SelectListItem() { Text = "Inativo", Value = "3" }
            };
    }
}