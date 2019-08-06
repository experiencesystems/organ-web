using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("Categoria")]
    public class Categoria : Repository<Categoria>
    {
        [Key]
        public int CategoriaID { get; set; }

        [Display(Name = "Categoria")]
        public String Nome { get; set; }

        [Display(Name = "Cor")]
        public String Cor { get; set; }
        
        public List<Semente> Sementes { get; set; }
    }
}