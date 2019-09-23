using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Safras
{
    [Table("tbPlantio")]
    public class Plantio : PlantioRepository
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 3)]
        public string Nome { get; set; }
        
        [StringLength(75, MinimumLength = 3)]
        public string Tipo { get; set; }

        [Required]
        [Range(0.01, 99999.99)]
        public double Densidade { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de criação")]
        public DateTime DataCriacao { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de início")]
        public DateTime DataInício { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data da colheita")]
        public DateTime DataColheita { get; set; }
        
        [Required]
        [ForeignKey("Semente")]
        public int IdSemente { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Quantidade de semente usada")]
        public int QtdSemUsada { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Época")]
        public string Epoca { get; set; }

        [Required]
        [Range(0.01, 9999.99)]
        [Display(Name = "KG/HA de Semente")]
        public double QntHectare { get; set; }
        
        public virtual Semente Semente { get; set; }

        public IEnumerable<Semente> Sementes { get; set; }

        [NotMapped]
        public double Porcentagem { get; set; }
    }
}