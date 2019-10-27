using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class AreaRepository : Repository<Area>
    {
        public async Task<List<Area>> AreasDisponiveis()
        {
            return await DbSet.Where(a => a.Disp == 1).ToListAsync();
        }
    }
}