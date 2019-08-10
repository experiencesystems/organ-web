using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Fazenda : Repository<Fazenda>
    {
        [Key]
        public int FazendaID { get; set; }
        public decimal Area { get; set; }
        public decimal Perimetro { get; set; }

        [ForeignKey("Localizacao")]
        public int LocalizacaoID { get; set; }

        [ForeignKey("Coordenada")]
        public int CoordenadasID { get; set; }

        //public Localizacao Localizacao { get; set; }
        //public Coordenada Coordenada { get; set; }
        //Localização - 1-1
        //Coordenadas - 1-1
    }
}