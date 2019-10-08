using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Financeiro
{
    [Table("vwSaida")]
    public class VwSaida
    {
        public string Data { get; set; }
        public double Saida { get; set; }
    }
}