using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbItem")]
    public class Item : Repository<Item>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 3)]
        public string Nome { get; set; }

        [StringLength(300, MinimumLength = 10)]
        public string Descricao { get; set; }

        [Required]
        [Range(0.01, 99999.99)]
        [Display(Name = "Valor unitário")]
        public double ValorUnit { get; set; }
        
        [Required]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        [Required]
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        [Required]
        [ForeignKey("Fornecedor")]
        public int IdFornecedor { get; set; }

        public virtual Estoque Estoque { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}

