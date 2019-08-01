using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Semente
{
    [Table("Semente")]
    public class Semente
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public String NOME { get; set; }

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

        [Display(Name = "Descrição")]
        public String DESCRICAO { get; set; }

        //public int CategoriaId { get; set; }
        //public virtual Categoria Categoria { get; set; }
        //http://www.macoratti.net/18/03/mvc5_cadprod1.htm
    }
}