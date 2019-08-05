using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Categoria")]
        public String Nome { get; set; }

        [Display(Name = "Cor")]
        public String Cor { get; set; }
        
        public List<Semente> Sementes { get; set; }
    }
}