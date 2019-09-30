using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Armazenamento;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewFornecedor
    {
 
        public IEnumerable<Fornecedor> Fornecedors { get; set; }
    }
}