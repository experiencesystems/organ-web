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

        [Required]
        [Range(0.01, 999.99)]
        [Display(Name = "Área")]
        public decimal Area { get; set; }

        [Required]
        [Range(0.01, 999.99)]
        [Display(Name = "Perímetro")]
        public decimal Perimetro { get; set; }

        [Required]
        public DbGeometry Coordenadas { get; set; }

        [Required]
        [ForeignKey("Localizacao")] 
        [Column(Order = 1)]
        public string CEP { get; set; }

        [Required]
        [ForeignKey("Localizacao")]
        [Column(Order = 2)]
        public int Numero { get; set; }
        
        public virtual Localizacao Localizacao { get; set; }
    }
}