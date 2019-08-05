using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Doenca
    {
        [Key]
        [Display(Name = "ID")]
        public int id_doenca { get; set; }
    }
}