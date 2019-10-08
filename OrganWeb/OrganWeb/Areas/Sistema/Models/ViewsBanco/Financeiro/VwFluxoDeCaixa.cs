using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Financeiro
{
    [Table("vwFluxoDeCaixa")]
    public class VwFluxoDeCaixa
    {
        [Key]
        public double Saida { get; set; }
        public double Entrada { get; set; }
        public double Saldo { get; set; }
        public string Mes { get; set; }
        public string Ano { get; set; }
    }
}