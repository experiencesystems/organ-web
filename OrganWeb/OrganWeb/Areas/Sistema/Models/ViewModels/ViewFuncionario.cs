using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Funcionarios;


namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewFuncionario
    {
        public IEnumerable<Cargo> Cargo { get; set; }
        public IEnumerable<Funcionario> Funcionario { get; set; }
    }
}