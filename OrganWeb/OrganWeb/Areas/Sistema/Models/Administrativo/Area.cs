using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Safras;

namespace OrganWeb.Areas.Sistema.Models.Administrativo
{
    [Table("tbArea")]
    public class Area : Repository<Area>
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(75, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        public bool Disponibilidade { get; set; }

        [Required]
        [ForeignKey("Solo")]
        public int IdSolo { get; set; }

        [Required]
        public DbGeometry Coordenadas { get; set; }

        public virtual Solo Solo { get; set; }
    }

    [Table("tbAreaPlantio")]
    public class AreaPlantio
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Plantio")]
        public int IdPlantio { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Area")]
        public int IdArea { get; set; }

        public virtual Plantio Plantio { get; set; }
        public virtual Area Area { get; set; }
    }
}