using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.API;
using OrganWeb.Areas.Sistema.Models.zBanco;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbMaquina")]
    public class Maquina : OrganRepository<Maquina>
    {
        [Key]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Nome { get; set; }

        [Required]
        public int Tipo { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Montadora { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(100, MinimumLength = 3)]
        public string Desc { get; set; }
        
        public virtual Estoque Estoque { get; set; }

        [NotMapped]
        public UnidadeCadastro Unini { get; set; }

        [NotMapped]
        public readonly List<SelectListItem> Tipos = new List<SelectListItem>()
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