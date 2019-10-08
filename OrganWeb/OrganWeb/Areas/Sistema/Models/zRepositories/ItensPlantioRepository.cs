using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class ItensPlantioRepository : Repository<ItensPlantio>
    {
        public void DeleteByIdPlantio(Plantio plantio)
        {
            foreach (var item in DbSet.Where(x => x.IdPlantio == plantio.Id).ToList())
            {
                DbSet.Remove(item);
                Save();
            }
        }
    }
}