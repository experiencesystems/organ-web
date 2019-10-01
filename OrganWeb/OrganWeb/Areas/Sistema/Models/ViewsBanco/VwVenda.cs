using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco
{
    public class VwVenda
    {
        public int IdVenda { get; set; }
        public DateTime D { get; set; }
        public DateTime Data { get; set; }
        public int IdEstoque { get; set; }
        public double ValorTotal { get; set; }
    }
}