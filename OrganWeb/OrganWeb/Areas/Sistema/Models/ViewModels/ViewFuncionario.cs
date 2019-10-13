using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;


namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewFuncionario
    {
        public IEnumerable<Cargo> Cargo { get; set; }
        public IEnumerable<Funcionario> Funcionario { get; set; }
        public IEnumerable<VwPessoa> Pessoa { get; set; }
        public IEnumerable<VwFuncionario> vwFuncionario { get; set; }
        public IEnumerable<VwEndereco> Enderecos { get; set; }

    }
}