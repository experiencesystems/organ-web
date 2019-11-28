using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque
{
    [Table("vwColheita")]
    public class VwColheita : OrganRepository<VwColheita>
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Data")]
        public string DataColheita { get; set; }
        [Display(Name = "Situação")]
        public string Situacao { get; set; }
        public int IdProd { get; set; }
        public string Produto { get; set; }
        [Display(Name = "Qtd colhida")]
        public int QtdColhida { get; set; }
        [Display(Name = "Qtd perdida")]
        public int QtdPerdida { get; set; }
        [Display(Name = "Qtd total")]
        public int QtdTotal { get; set; }
        public int IdPlantio { get; set; }
        public string Plantio { get; set; }
    }
}