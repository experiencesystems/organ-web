using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrganWeb.Areas.Sistema.Models.Sementec;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Categoria")]
        public String NOME { get; set; }

        [Display(Name = "Cor")]
        public String COR { get; set; }

        public List<Semente> Sementes { get; set; }
    }
}