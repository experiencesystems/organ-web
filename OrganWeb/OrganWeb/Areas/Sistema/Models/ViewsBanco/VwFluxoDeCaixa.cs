using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco
{
    [Table("vwFluxoDeCaixa")]
    public class VwFluxoDeCaixa : Repository<VwFluxoDeCaixa>
    {
        [Key]
        public double Saída { get; set; }
        public double Entrada { get; set; }
        public double Saldo { get; set; }
        public string Mês { get; set; }
    }
}