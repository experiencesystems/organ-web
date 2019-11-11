using OrganWeb.Areas.Sistema.Models.Controles;
using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Praga_e_Doenca
{
    [Table("tbControlePD")]
    public class ControlePD : OrganRepository<ControlePD>
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Controle")]
        public int IdControle { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("PragaOrDoenca")]
        public int IdPD { get; set; }

        public virtual Controle Controle { get; set; }
        public virtual PragaOrDoenca PragaOrDoenca { get; set; }
    }
}