using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Administrativo
{
    [Table("tbArea")]
    public class Area : Repository<Area>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        public int Disp { get; set; }
        
        [Required]
        public int Tamanho { get; set; }

        [Required]
        [ForeignKey("Solo")]
        public int IdSolo { get; set; }

        public virtual Solo Solo { get; set; }
    }
}