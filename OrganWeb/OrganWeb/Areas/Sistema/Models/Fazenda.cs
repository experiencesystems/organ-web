using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int LocalizacaoID { get; set; }
        public int CoordenadasID { get; set; }

        //Localização - 1-1
        //Coordenadas - 1-1
    }
}