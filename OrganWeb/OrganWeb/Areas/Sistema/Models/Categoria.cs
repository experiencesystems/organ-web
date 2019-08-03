using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrganWeb.Areas.Sistema.Models._Semente;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int id_praga { get; set; }

        [Display(Name = "Categoria")]
        public String nome { get; set; }

        [Display(Name = "Cor")]
        public String cor { get; set; }
        
        public List<Semente> Sementes { get; set; }
    }
}