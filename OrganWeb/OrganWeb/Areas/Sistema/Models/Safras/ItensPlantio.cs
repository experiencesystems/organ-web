using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.zRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Safras
{
    [Table("tbItensPlantio")]
    public class ItensPlantio : ItensPlantioRepository
    {
        [Required] 
        public double QtdUsada { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Plantio")]
        public int IdPlantio { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        public virtual Plantio Plantio { get; set; }
        public virtual Estoque Estoque { get; set; }
    }
}