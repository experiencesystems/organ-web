using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Areas.Sistema.Models.zBanco;
using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class HistoricoRepository : OrganRepository<VwHistorico>
    {
        public async Task<IPagedList<VwHistorico>> GetPagedAll(int page)
        {
            try
            {
                return await DbSet.OrderBy(p => p.Id).ToPagedListAsync(page, 5);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}