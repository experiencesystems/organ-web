using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Praga
    {
        [Key]
        [Display(Name = "ID")]
        public int id_praga { get; set; }

        [Display(Name = "Nome popular")]
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        public String nomepopular { get; set; }

        [Display(Name = "Nome científico")]
        [Required(ErrorMessage = "O nome é obrigatório.", AllowEmptyStrings = false)]
        public String nomecientifico { get; set; }

        [Display(Name = "Descrição")]
        public String descricao { get; set; }
    }
}