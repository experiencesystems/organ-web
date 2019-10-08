using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Financeiro
{
    [Table("vwCompra")]
    public class VwCompra : Repository<VwCompra>
    {
        [Key]
        public int Compra { get; set; }
        public string Data { get; set; }
        public string ItensQtd { get; set; }
        public int Estoque { get; set; }
        public double ValorTotal { get; set; }
    }
}