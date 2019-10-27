using OrganWeb.Areas.Sistema.Models.Funcionarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Safras
{
    [Table("tbFuncPlantio")]
    public class FuncPlantio
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Funcionario")]
        public int IdFunc { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Plantio")]
        public int IdPlantio { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Plantio Plantio { get; set; }
    }
}