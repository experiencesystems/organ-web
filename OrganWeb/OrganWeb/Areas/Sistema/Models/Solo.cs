using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Solo : Repository<Solo>
    {
        [Key]
        public int IDSolo{ get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        public string Nome { get; set; }


        [Display(Name = "Tipo do Solo")]
        public string TipoSolo { get; set; }



        [Display(Name = "Incidência Solar")]
        public double InciSol { get; set; }


        [Display(Name = "Incidência do Vento")]
        public double InciVen { get; set; }
    }
}