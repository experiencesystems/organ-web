using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewEquipe
    {
        public IEnumerable<FuncEquipe> FunciEquipe { get; set; }
        public IEnumerable<Equipe> Equipes { get; set; }
        public IEnumerable<Funcionario> Funcionarios { get; set; }
    }
}