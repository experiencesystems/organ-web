using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbFazenda")]
    public class Fazenda : Repository<Fazenda>
    {
        [Key]
        public int Id { get; set; }
        public decimal Area { get; set; }
        public decimal Perimetro { get; set; }

        [ForeignKey("Localizacao"), Column(Order = 0)]
        public char CEP { get; set; }
        [ForeignKey("Localizacao"), Column(Order = 0)]
        public int Numero { get; set; }
        
        public DbGeometry Coordenadas { get; set; }

        public virtual Localizacao Localizacao { get; set; }
        //Localização - 1-1
    }
}