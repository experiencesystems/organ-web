using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbEvento")]
    public class Evento : Repository<Evento>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [StringLength(300, MinimumLength = 10)]
        public string Descricao { get; set; }
        
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}