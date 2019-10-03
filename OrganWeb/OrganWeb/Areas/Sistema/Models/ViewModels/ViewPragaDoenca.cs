using OrganWeb.Areas.Sistema.Models.Controles;
using OrganWeb.Areas.Sistema.Models.Praga_e_Doenca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewPragaDoenca
    {
        public IEnumerable<Controle> Controles { get; set; }
        public IEnumerable<ControlePD> ControlePDs { get; set; }
        public IEnumerable<PragaOrDoenca> PragaOrDoencas { get; set; }
    }
}