using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class ColheitaRepository : Repository<Colheita>
    {
        public Colheita GetByID(int? pl, int? pr)
        {
            return DbSet.Include(p => p.Plantio).Include(pro => pro.Produto).Where(a => a.IdPlantio == pl && a.IdProd == pr).FirstOrDefault();
        }

        public void Delete(int id, int id2)
        {
            DbSet.Remove(GetByID(id, id2));
        }
    }
}