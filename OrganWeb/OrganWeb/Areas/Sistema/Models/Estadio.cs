using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Estadio : Repository<Estadio>
    {
        [Key]
        public int EstadioID { get; set; }
        public string Nome { get; set; }
        public double Tempo { get; set; }
        public string Descricao { get; set; }

        public List<Especificacao> Especificacoes { get; set; }
        public List<Controle> Controles { get; set; }
        //Especificacao - n
        //Controle - n
    }
}