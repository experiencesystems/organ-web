using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.zRepositories;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Safras
{
    [Table("tbColheita")]
    public class Colheita : ColheitaRepository
    {
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        
        [Required]
        [Display(Name = "Quantidade de perdas")]
        public decimal QtdPerdas { get; set; }

        [Required]
        [Display(Name = "Quantidade total")]
        public decimal QtdTotal { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Plantio")]
        public int IdPlantio { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Produto")]
        public int IdProd { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Plantio Plantio { get; set; }
    }
}