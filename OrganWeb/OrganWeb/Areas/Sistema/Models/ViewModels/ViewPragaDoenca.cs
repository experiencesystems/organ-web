using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Controles;


namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewPragaDoenca
    {
        public IEnumerable<Doenca> Doencas { get; set; }
        public IEnumerable<Controle> Controles { get; set; }
        public IEnumerable<Praga> Pragas { get; set; }
    }
}