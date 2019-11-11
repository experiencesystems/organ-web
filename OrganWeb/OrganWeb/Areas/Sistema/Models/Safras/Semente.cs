using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.zBanco;

namespace OrganWeb.Areas.Sistema.Models.Safras
{
    [Table("tbSemente")]
    public class Semente : OrganRepository<Semente>
    {
        [Key]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 1)]
        public string Nome { get; set; }
        
        [Display(Name = "Descrição")]
        [StringLength(100, MinimumLength = 3)]
        public string Desc { get; set; }

        public virtual Estoque Estoque { get; set; }

        //http://www.macoratti.net/18/03/mvc5_cadprod1.htm
    }
}