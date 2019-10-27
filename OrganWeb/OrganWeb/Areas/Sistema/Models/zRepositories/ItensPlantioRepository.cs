using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class ItensPlantioRepository : Repository<ItensPlantio>
    {
        public async void DeleteByIdPlantio(Plantio plantio)
        {
            foreach (var item in await DbSet.Where(x => x.IdPlantio == plantio.Id).ToListAsync())
            {
                DbSet.Remove(item);
                await Save();
            }
        }
    }
}