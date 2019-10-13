using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewFornecedor
    {


        public IEnumerable<VwFornecedor> VwFornecedor { get; set; }
        public IEnumerable<VwPessoaJuridica> PessoaJuridica { get; set; }
        public IEnumerable<VwEndereco> Endereco { get; set; }
        public IEnumerable<Fornecedor> Fornecedor { get; set; }
    }
}