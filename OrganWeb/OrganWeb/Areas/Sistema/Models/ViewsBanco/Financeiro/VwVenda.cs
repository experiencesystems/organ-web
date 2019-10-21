using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Financeiro
{
    [Table("vwVenda")]
    public class VwVenda : Repository<VwVenda>
    {
        [Key]
        public int Venda { get; set; }
        public string Data { get; set; }
        public int IdEstoque { get; set; }
        public double ValorTotal { get; set; }
    }
}