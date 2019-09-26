using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Ferramentas;


namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewMaquina
    {

        public IEnumerable<Manutencao> Manutencao { get; set; }
        public IEnumerable<Maquina> Maquina { get; set; }
     
    }
}