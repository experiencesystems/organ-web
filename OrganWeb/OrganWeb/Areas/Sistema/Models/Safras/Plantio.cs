using OrganWeb.Areas.Sistema.Models.Administrativo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Safras
{
    [Table("tbPlantio")]
    public class Plantio : PlantioRepository
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        public int Sistema { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataColheita { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataInicio { get; set; }

        [Required]
        public int TipoPlantio { get; set; }

        [NotMapped]
        public double Porcentagem { get; set; }
    }
}