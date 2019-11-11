using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using PagedList;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewEstoque
    {
        public IPagedList<VwItems> VwItems { get; set; }
        public IEnumerable<VwHistorico> VwHistoricos { get; set; }
        public IEnumerable<Estoque> Estoques { get; set; }
        public IEnumerable<Semente> Sementes { get; set; }
        public IEnumerable<VwFornecedor> Fornecedors { get; set; }
    }
}