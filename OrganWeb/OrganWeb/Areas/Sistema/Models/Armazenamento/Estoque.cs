using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models.Banco;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.zRepositories;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbEstoque")]
    public class Estoque : Repository<Estoque>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public double Qtd { get; set; }

        [Required]
        [Display(Name = "Unidade de medida")]
        public int UM { get; set; }

        [Required]
        [Display(Name = "Valor unitário")]
        public double ValorUnit { get; set; }

        [NotMapped]
        public readonly List<SelectListItem> UnidadesDeMedida = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Litros", Value = "1" },
            new SelectListItem() { Text = "Kilos", Value = "2" },
            new SelectListItem() { Text = "Gramas", Value = "3" },
            new SelectListItem() { Text = "Unidades", Value = "4" },
            new SelectListItem() { Text = "Mililitros", Value = "5" },
            new SelectListItem() { Text = "Balde", Value = "6" },
            new SelectListItem() { Text = "Bandeja", Value = "7" },
            new SelectListItem() { Text = "Gramas", Value = "8" },
            new SelectListItem() { Text = "Caixa", Value = "9" },
            new SelectListItem() { Text = "Caixa (c/ 2 unidades)", Value = "10" },
            new SelectListItem() { Text = "Caixa (c/ 3 unidades)", Value = "11" },
            new SelectListItem() { Text = "Caixa (c/ 5 unidades)", Value = "12" },
            new SelectListItem() { Text = "Caixa (c/ 10 unidades)", Value = "13" },
            new SelectListItem() { Text = "Caixa (c/ 15 unidades)", Value = "14" },
            new SelectListItem() { Text = "Caixa (c/ 20 unidades)", Value = "15" },
            new SelectListItem() { Text = "Caixa (c/ 25 unidades)", Value = "16" },
            new SelectListItem() { Text = "Caixa (c/ 50 unidades)", Value = "17" },
            new SelectListItem() { Text = "Caixa (c/ 100 unidades)", Value = "18" },
            new SelectListItem() { Text = "Duzia", Value = "19" },
            new SelectListItem() { Text = "Embalagem", Value = "20" },
            new SelectListItem() { Text = "Cartela", Value = "21" },
            new SelectListItem() { Text = "Conjunto", Value = "22" },
            new SelectListItem() { Text = "Fardo", Value = "23" },
            new SelectListItem() { Text = "Galão", Value = "24" },
            new SelectListItem() { Text = "Garrafa", Value = "25" },
            new SelectListItem() { Text = "Folha", Value = "26" },
            new SelectListItem() { Text = "Peça", Value = "27" },
            new SelectListItem() { Text = "Rolo", Value = "28" },
            new SelectListItem() { Text = "Tanque", Value = "29" },
            new SelectListItem() { Text = "Tambor", Value = "30" },
            new SelectListItem() { Text = "Lata", Value = "31" },
            new SelectListItem() { Text = "Milheiro", Value = "32" },
            new SelectListItem() { Text = "Saco", Value = "33" },
            new SelectListItem() { Text = "Resma", Value = "34" },
            new SelectListItem() { Text = "Palete", Value = "35" },
            new SelectListItem() { Text = "Sacola", Value = "36" },
            new SelectListItem() { Text = "Metro Quadrado", Value = "37" },
            new SelectListItem() { Text = "Pacote", Value = "38" },
            new SelectListItem() { Text = "Pacote 500g", Value = "39" },
            new SelectListItem() { Text = "Pacote 1kg", Value = "40" }/*,
            new SelectListItem() { Text = "Pacote 1,5kg", Value = "38" },
            new SelectListItem() { Text = "Pacote 2kg", Value = "38" },
            new SelectListItem() { Text = "Pacote 3kg", Value = "38" },
            new SelectListItem() { Text = "Pacote 5kg", Value = "38" },
            new SelectListItem() { Text = "Kit", Value = "38" },
            new SelectListItem() { Text = "Muda", Value = "38" },
            new SelectListItem() { Text = "Capsula", Value = "38" }*/
            };
    }

}