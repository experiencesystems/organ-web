using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Models.Financeiro
{
    [Table("tbPagamento")]
    public class Pagamento : Repository<Pagamento>
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Quantidade de parcelas")]
        public int? QtdParcelas { get; set; }

        [Required]
        [Display(Name = "Valor da parcela")]
        public double VlParcela { get; set; }

        [Required]
        public int Tipo { get; set; }

        [NotMapped]
        public readonly List<SelectListItem> Tipos = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Boleto bancário", Value = "1" },
            new SelectListItem() { Text = "Cartão de crédito", Value = "2" },
            new SelectListItem() { Text = "Cartão de débito", Value = "3" },
            new SelectListItem() { Text = "Cheque", Value = "4" },
            new SelectListItem() { Text = "Dinheiro", Value = "5" }
            };
    }
}