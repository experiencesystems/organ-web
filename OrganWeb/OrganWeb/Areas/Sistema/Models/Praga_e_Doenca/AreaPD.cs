using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Praga_e_Doenca
{
    [Table("tbAreaPD")]
    public class AreaPD : Repository<AreaPD>
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Area")]
        public int IdArea { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("PragaOrDoenca")]
        public int IdPd { get; set; }

        public virtual Area Area { get; set; }
        public virtual PragaOrDoenca PragaOrDoenca { get; set; }
    }
}