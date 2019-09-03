using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbSemente")]
    public class Semente : Repository<Semente>
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 5)]
        [Display(Name = "Solo ideal")]
        public string SoloIdeal { get; set; }

        [Required]
        [Display(Name = "Incidência solar ideal")]
        [Range(0.01, 999.99)]
        public double IncSolar { get; set; }

        [Required]
        [Display(Name = "Incidência vento ideal")]
        [Range(0.01, 999.99)]
        public double IncVento { get; set; }

        [Required]
        [Range(0.01, 999.99)]
        public double Acidez { get; set; }

        [Required]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        [Required]
        [ForeignKey("Fornecedor")]
        public int IdFornecedor { get; set; }

        [Required]
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        public virtual Estoque Estoque { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Categoria Categoria { get; set; }

        //http://www.macoratti.net/18/03/mvc5_cadprod1.htm
    }
}