using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Estoque;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewFornecedor
    {
 
        public IEnumerable<Fornecedor> Fornecedors { get; set; }
        public IEnumerable<FornecedorTelefone> FornecedorTelefone { get; set; }
    }
}