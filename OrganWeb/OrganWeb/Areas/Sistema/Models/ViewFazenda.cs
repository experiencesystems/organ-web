using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models
{
    public class ViewFazenda
    {
        public IEnumerable<Funcionario> Funcionario { get; set; }
        public IEnumerable<Fazenda> Fazenda { get; set; }
        public IEnumerable<Area> Area { get; set; }
    }
}