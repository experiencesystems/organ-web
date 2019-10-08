using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.Administrativo;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewPlantio
    {
        public IEnumerable<Plantio> Plantios { get; set; }
        public IEnumerable<Semente> Sementes { get; set; }
        public IEnumerable<AreaPlantio> AreaPlantios { get; set; }
        public IEnumerable<ItensPlantio> ItensPlantios { get; set; }
        public IEnumerable<Area> Area { get; set; }
    }
}