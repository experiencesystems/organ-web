using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Ferramentas;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewManutencao
    {
        public IEnumerable<Manutencao> Manutencaos { get; set; }
        public IEnumerable<MaquinaManutencao> MaquinaManutencaos { get; set; }
        public IEnumerable<Maquina> Maquinas { get; set; }
    }
}