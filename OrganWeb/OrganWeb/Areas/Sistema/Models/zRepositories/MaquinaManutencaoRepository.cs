using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Ferramentas;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class MaquinaManutencaoRepository : Repository<ManutencaoMaquina>
    {
        public ManutencaoMaquina GetByID(int? man, int? maq)
        {
            return DbSet.Find(man, maq);
        }

        public void Delete(int id, int id2)
        {
            DbSet.Remove(DbSet.Find(id, id2));
        }
    }
}