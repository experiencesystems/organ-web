using OrganWeb.Areas.Sistema.Models.Financas;
using OrganWeb.Models.Banco;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class DespesaAdmRepository : Repository<DespesaAdm>
    {
        public DespesaAdm GetByID(int? c, int? d)
        {
            return DbSet.Include(da => da.Conta).Include(da => da.Despesa).Where(a => a.IdConta == c && a.IdDespesa == d).FirstOrDefault();
        }

        public void Delete(int id, int id2)
        {
            DbSet.Remove(GetByID(id, id2));
        }
    }
}