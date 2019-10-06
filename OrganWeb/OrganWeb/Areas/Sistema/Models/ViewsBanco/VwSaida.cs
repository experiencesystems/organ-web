using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco
{
    [Table("vwSaida")]
    public class VwSaida
    {
        [Key]
        public DateTime Data { get; set; }
        public double Saida { get; set; }
    }
}