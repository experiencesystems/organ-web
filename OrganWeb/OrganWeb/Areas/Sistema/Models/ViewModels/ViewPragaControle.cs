using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using PagedList;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class ViewPragaControle
    {
        public IPagedList<VwPragaOrDoenca> PragaOrDoencas { get; set; }
        public IPagedList<VwControle> Controles { get; set; }
    }
}