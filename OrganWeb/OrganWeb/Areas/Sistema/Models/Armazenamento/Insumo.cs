using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbInsumo")]
    public class Insumo : Repository<Categoria>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string Desc { get; set; }

        [Required]
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}