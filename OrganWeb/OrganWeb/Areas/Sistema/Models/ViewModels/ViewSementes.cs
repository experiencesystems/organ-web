using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Estoque;
using OrganWeb.Areas.Sistema.Models.Safras;


namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewSementes
    {
       
        public IEnumerable<Semente>Semente{ get; set; }
        public IEnumerable<Estoque.Estoque>Estoquesemente { get; set; }
        public IEnumerable<Fornecedor> FornecedorSemente { get; set; }

    }
}