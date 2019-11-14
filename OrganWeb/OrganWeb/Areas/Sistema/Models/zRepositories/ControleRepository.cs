using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Areas.Sistema.Models.zBanco;
using PagedList;
using PagedList.EntityFramework;
using System.Linq;
using System.Threading.Tasks;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class ControleRepository : OrganRepository<VwControle>
    {
        public async Task<IPagedList<VwControle>> GetPagedAll(int page)
        {
            return await DbSet.OrderBy(p => p.Id).ToPagedListAsync(page, 5);
        }
    }
}