using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.ViewsBanco;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class CreateCompraViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Item(s) comprado(s)")]
        public int[] IdItem { get; set; }

        [Required]
        [Display(Name = "Fornecedor")]
        public int IdFornecedor { get; set; }

        //TODO: Mudar o descontoprod na tbItensComprados pra double
        [Required]
        [Range(0.00, 999.99)]
        public double Desconto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        [Display(Name = "Quantidade de parcelas")]
        public int QtdParcelas { get; set; }

        [Required]
        [Display(Name = "Valor da parcela (se for somente uma, coloque o valor total)")]
        public double ValorParcela { get; set; }

        [Required]
        [Display(Name = "Tipo de pagamento")]
        public int Tipo { get; set; }

        public IEnumerable<VwItems> Items { get; set; }
        public IEnumerable<Fornecedor> Fornecedores { get; set; }
        public IEnumerable<SelectListItem> Tipos { get; set; }
    }
}