using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco
{
    public class VwCompra : Repository<VwCompra>
    {
        [Key]
        public int Compra { get; set; }
        public DateTime Data { get; set; }
        public int Estoque { get; set; }
        public double ValorTotal { get; set; }
    }
}