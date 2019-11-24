using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbProduto")]
    public class Produto : OrganRepository<Produto>
    {
        [Key]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }
        
        [StringLength(100)]
        [Display(Name = "Descrição")]
        public string Desc { get; set; }

        [Required]
        [Display(Name = "Produto")]
        [StringLength(30, MinimumLength = 1)]
        public string Nome { get; set; }

        public virtual Estoque Estoque { get; set; }
    }
}