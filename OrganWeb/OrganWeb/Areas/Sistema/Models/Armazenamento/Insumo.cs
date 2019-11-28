using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbInsumo")]
    public class Insumo : OrganRepository<Insumo>
    {
        [Key]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Nome { get; set; }
        
        [Display(Name = "Descrição")]
        [StringLength(100, MinimumLength = 3)]
        public string Desc { get; set; }

        [Required]
        public int Categoria { get; set; }

        public virtual Estoque Estoque { get; set; }
        
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
    }
}