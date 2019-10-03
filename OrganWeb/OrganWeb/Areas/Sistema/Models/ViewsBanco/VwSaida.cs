using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco
{
    public class VwSaida
    {
        [Key]
        public DateTime Data { get; set; }
        public double Saida { get; set; }
    }
}