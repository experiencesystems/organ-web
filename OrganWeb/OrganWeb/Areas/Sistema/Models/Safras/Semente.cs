using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Models.Banco;
using OrganWeb.Areas.Sistema.Models.Armazenamento;

namespace OrganWeb.Areas.Sistema.Models.Safras
{
    [Table("tbSemente")]
    public class Semente : Repository<Semente>
    {
        [Key]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }
        
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Solo ideal")]
        public string Solo { get; set; }
        
        [Display(Name = "Incidência solar ideal")]
        [Range(0.01, 999.99)]
        public double IncSol { get; set; }
        
        [Display(Name = "Incidência vento ideal")]
        [Range(0.01, 999.99)]
        public double IncVento { get; set; }
        
        [Range(0.01, 999.99)]
        public double Acidez { get; set; }

        public virtual Estoque Estoque { get; set; }

        //http://www.macoratti.net/18/03/mvc5_cadprod1.htm
    }
}