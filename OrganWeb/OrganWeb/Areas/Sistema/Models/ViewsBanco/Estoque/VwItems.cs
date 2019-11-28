using OrganWeb.Areas.Sistema.Models.zRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque
{
    [Table("vwItems")]
    public class VwItems : EstoqueRepository
    {
        [Key]
        public int Id { get; set; }
        public string Item { get; set; }
        public int Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Fornecedor { get; set; }
        public string Tipo { get; set; }

        [NotMapped]
        public readonly List<SelectListItem> TiposInsumo = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Eimoletos (ex. arado)", Value = "1" },
            new SelectListItem() { Text = "Ferramenta", Value = "2" },
            new SelectListItem() { Text = "Equipamentos (bomba, tubulação, aspersores)", Value = "3" },
            new SelectListItem() { Text = "Insetos de uso agrícola", Value = "4" },
            new SelectListItem() { Text = "Esterco", Value = "5" },
            new SelectListItem() { Text = "Fertilizantes orgânicos", Value = "6" },
            new SelectListItem() { Text = "Microorganismos de uso agrícola", Value = "7" },
            new SelectListItem() { Text = "Adubação verde", Value = "8" },
            new SelectListItem() { Text = "Defensivos", Value = "9" },
            new SelectListItem() { Text = "Fungicidas", Value = "10" },
            new SelectListItem() { Text = "Herbicidas", Value = "11" },
            new SelectListItem() { Text = "Fertilizantes químicos", Value = "12" }
            };

        [NotMapped]
        public readonly List<SelectListItem> TiposMaquina = new List<SelectListItem>()
            {
            new SelectListItem() { Text = "Semeadoras", Value = "1" },
            new SelectListItem() { Text = "Plantadoras", Value = "2" },
            new SelectListItem() { Text = "Transplantadoras", Value = "3" },
            new SelectListItem() { Text = "Adubadoras", Value = "4" },
            new SelectListItem() { Text = "Carretas", Value = "5" },
            new SelectListItem() { Text = "Ceifadeiras", Value = "6" },
            new SelectListItem() { Text = "Roçadoras", Value = "7" },
            new SelectListItem() { Text = "Pulverizadoras", Value = "8" },
            new SelectListItem() { Text = "Polvilhadoras", Value = "9" },
            new SelectListItem() { Text = "Microatomizadoras", Value = "10" },
            new SelectListItem() { Text = "Atomizadoras", Value = "11" },
            new SelectListItem() { Text = "Fumigadoras", Value = "12" },
            new SelectListItem() { Text = "Atomizadoras", Value = "13" },
            new SelectListItem() { Text = "Colhedoras", Value = "14" },
            new SelectListItem() { Text = "Carroças", Value = "15" },
            new SelectListItem() { Text = "Caminhões", Value = "16" },
            new SelectListItem() { Text = "Secadoras", Value = "17" },
            new SelectListItem() { Text = "Classificadoras", Value = "18" },
            new SelectListItem() { Text = "Polidoras", Value = "19" },
            new SelectListItem() { Text = "Tratores florestais", Value = "20" },
            new SelectListItem() { Text = "Tratores industriais", Value = "21" }
            };
    }
}