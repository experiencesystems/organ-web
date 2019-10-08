using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque
{
    [Table("vwQtdMa")]
    public class VwQtdMa
    {
        [Key]
        public int IdMaquina { get; set; }
        public int IdManutencao { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double CustoTotal { get; set; }
    }
}