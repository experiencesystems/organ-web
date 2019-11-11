using OrganWeb.Areas.Sistema.Models.Safras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;
using OrganWeb.Areas.Sistema.Models.zBanco;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class ColheitaRepository : OrganRepository<Colheita>
    {
        public async Task<Colheita> GetByID(int? pl, int? pr)
        {
            return await DbSet.Include(p => p.Plantio).Include(pro => pro.Produto).Where(a => a.IdPlantio == pl && a.IdProd == pr).FirstOrDefaultAsync();
        }

        public async void Delete(int id, int id2)
        {
            DbSet.Remove(await GetByID(id, id2));
        }
    }
}