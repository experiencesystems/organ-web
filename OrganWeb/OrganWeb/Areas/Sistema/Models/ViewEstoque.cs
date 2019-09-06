using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models
{
    public class ViewEstoque
    {
        public IEnumerable<VwItems> VwItems { get; set; }
        public IEnumerable<HistoricoEstoque> HistoricoEstoques { get; set; }
        public IEnumerable<Estoque> Estoques { get; set; }
        public IEnumerable<Semente> Sementes { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}