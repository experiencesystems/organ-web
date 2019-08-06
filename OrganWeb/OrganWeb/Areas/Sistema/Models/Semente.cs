using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("Semente")]
    public class Semente : Repository<Semente>
    {
        [Key]
        public Int32 SementeID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        public String Nome { get; set; }
                
        [Display(Name = "Descrição")]
        public String Descricao { get; set; }

        [Display(Name = "Categoria")]
        public Int32 CategoriaID { get; set; }

        [Display(Name = "Categoria")]
        [ForeignKey("CategoriaID")]
        public virtual Categoria Categoria { get; set; }

        /*
        [Display(Name = "Tipo de solo")]
        public String TipoSolo { get; set; }

        [Display(Name = "Incidência solar")]
        public double IncSolar { get; set; }

        [Display(Name = "Incidência do vento")]
        public double IncVento { get; set; }

        [Display(Name = "Acidez")]
        public double Acidez { get; set; }
        */

        //http://www.macoratti.net/18/03/mvc5_cadprod1.htm
    }
}