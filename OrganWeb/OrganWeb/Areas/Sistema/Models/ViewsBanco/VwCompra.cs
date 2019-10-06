using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco
{
    [Table("vwCompra")]
    public class VwCompra : Repository<VwCompra>
    {
        [Key]
        public int Compra { get; set; }
        public string Data { get; set; }
        public int Estoque { get; set; }
        public string Item { get; set; }
        public double ValorTotal { get; set; }
    }
}