using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;


namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewSementes
    {
       
        public IEnumerable<Semente>Semente{ get; set; }
        public IEnumerable<Estoque>Estoques { get; set; }
        public IEnumerable<Fornecedor> FornecedorSementess { get; set; }
        public IEnumerable<VwFornecedor> FornecedorSementes { get; set; }

    }
}