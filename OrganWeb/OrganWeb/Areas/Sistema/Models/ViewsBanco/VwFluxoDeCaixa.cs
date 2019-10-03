using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco
{
    public class VwFluxoDeCaixa : Repository<VwFluxoDeCaixa>
    {
        [Key]
        public double Saída { get; set; }
        public double Entrada { get; set; }
        public double Saldo { get; set; }
        public string Mês { get; set; }
    }
}