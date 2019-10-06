using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class AreaRepository : Repository<Area>
    {
        public List<Area> AreasDisponiveis()
        {
            return DbSet.Where(a => a.Disp == 1).ToList();
        }
    }
}