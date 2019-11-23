using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewFazenda
    {
        public IEnumerable<Area> Areas { get; set; }
        public IEnumerable<Solo> Solos { get; set; }
        public IEnumerable<VwFuncionario> Funcionarios { get; set; }
    }
}