using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Praga : Repository<Praga>
    {
        [Key]
        [Display(Name = "ID")]
        public int PragaID { get; set; }

        [Display(Name = "Nome popular")]
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        public String NomePopular { get; set; }

        [Display(Name = "Nome científico")]
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        public String NomeCientifico { get; set; }

        [Display(Name = "Descrição")]
        public String Descricao { get; set; }
    }
}