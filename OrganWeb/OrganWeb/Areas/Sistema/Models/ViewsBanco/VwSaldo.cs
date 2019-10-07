using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco
{
    [Table("vwSaldo")]
    public class VwSaldo : Repository<VwSaldo>
    {
        [Key]
        public double Saldo { get; set; }
    }
}