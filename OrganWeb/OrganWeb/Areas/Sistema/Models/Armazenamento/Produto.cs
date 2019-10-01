using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbProduto")]
    public class Produto
    {
        [Key]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }
        
        [StringLength(300, MinimumLength = 2)]
        public string Desc { get; set; }

        public virtual Estoque Estoque { get; set; }
    }
}