using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewEstoque
    {
        public IEnumerable<VwItems> VwItems { get; set; }
        public IEnumerable<HistoricoEstoque> HistoricoEstoques { get; set; }
        public IEnumerable<Estoque> Estoques { get; set; }
        public IEnumerable<Semente> Sementes { get; set; }
        //public IEnumerable<Item> Items { get; set; }
    }
}