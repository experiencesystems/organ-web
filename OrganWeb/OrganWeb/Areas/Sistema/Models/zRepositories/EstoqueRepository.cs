using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Models.Banco;
using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class EstoqueRepository : Repository<VwItems>
    {
        public async Task<IPagedList<VwItems>> GetPagedAll(int page)
        {
            return await DbSet.OrderBy(p => p.Id).ToPagedListAsync(page, 5);
        }
    }
}