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
        
        [Display(Name = "Quantidade de parcelas")] //TODO: ver se esse int? não da problema TESTA ISSO MILENA
        public int? QtdParcelas { get; set; }

        [Required]//TODO: tirar valor parcela?
        [Display(Name = "Valor da parcela (se for somente uma, coloque o valor total)")]
        public double VlParcela { get; set; }

        [Required]
        [Display(Name = "Tipo de pagamento")]
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