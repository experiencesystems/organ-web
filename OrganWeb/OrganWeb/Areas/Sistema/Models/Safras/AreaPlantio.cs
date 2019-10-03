using OrganWeb.Areas.Sistema.Models.Administrativo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Safras
{
    [Table("tbAreaPlantio")]
    public class AreaPlantio
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Plantio")]
        public int IdPlantio { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Area")]
        public Guid IdArea { get; set; }

        [Required]
        public int Densidade { get; set; }

        public virtual Plantio Plantio { get; set; }
        public virtual Area Area { get; set; }

        public IEnumerable<Area> Areas { get; set; }
    }
}