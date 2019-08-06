using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Doenca : Repository<Doenca>
    {
        [Key]
        [Display(Name = "ID")]
        public int DoencaID { get; set; }
        public string Nome { get; set; }
        public string Sintomas { get; set; }
        public string Tratamento { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}