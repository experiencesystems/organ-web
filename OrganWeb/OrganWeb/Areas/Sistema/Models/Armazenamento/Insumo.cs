using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbInsumo")]
    public class Insumo : Repository<Insumo>
    {
        [Key]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }
        
        [Display(Name = "Descrição")]
        [StringLength(300, MinimumLength = 10)]
        public string Desc { get; set; }

        [Required]
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Estoque Estoque { get; set; }

        public IEnumerable<Categoria> Categorias { get; set; }
    }
}