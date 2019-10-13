using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class CreateSementeViewModel
    {
        [Key]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Solo ideal")]
        public string Solo { get; set; }

        [Display(Name = "Incidência solar ideal")]
        [Range(0.01, 999.99)]
        public decimal? IncSol { get; set; }

        [Display(Name = "Incidência vento ideal")]
        [Range(0.01, 999.99)]
        public decimal? IncVento { get; set; }

        [Range(0.01, 999.99)]
        public decimal? Acidez { get; set; }


        [Display(Name = "Fornecedor")]
        [Required]
        public string NomeFantasia { get; set; }

        [Display(Name = "Quantidade")]
        [Required]
        public double Qtd { get; set; }
        //n sei se vai precisar



        [Required]
        public int UM { get; set; }
        //tb n sei o display name desse mals

        [Display(Name = "Valor Unitário")]
        [Required]
        public double ValorUnit { get; set; }





        public IEnumerable<Semente> Semente { get; set; }
        public IEnumerable<Estoque> Estoques { get; set; }
        public IEnumerable<Fornecedor> FornecedorSementess { get; set; }
        public IEnumerable<VwFornecedor> FornecedorSementes { get; set; }
    }
}