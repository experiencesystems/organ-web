using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Models.Banco;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco
{
    [Table("VwItems")]
    public class VwItems : Repository<VwItems>
    {
        [Key]
        public int Id { get; set; }
        public string Item { get; set; }
        public double Quantidade { get; set; }
        [Display(Name = "Unidade de medida")]
        public string UnidadeMedida { get; set; }
        [Display(Name = "Valor unitário")]
        public double ValorUnit { get; set; }
        [Display(Name = "Valor total")]
        public double ValorTotal { get; set; }
        public string Categoria { get; set; }
    }
}