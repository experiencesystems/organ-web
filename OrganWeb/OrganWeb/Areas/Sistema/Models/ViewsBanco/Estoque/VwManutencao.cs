using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque
{
    [Table("vwManutencao")]
    public class VwManutencao
    {
        [Key]
        public int IdMaquina { get; set; }
        public int IdManutencao { get; set; }
        public string Maquina { get; set; }
        public int Tipo { get; set; }
        public string Manutencao { get; set; }
        public string Data { get; set; }
        public double Valor { get; set; }
    }
}