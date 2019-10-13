using OrganWeb.Areas.Sistema.Models.Financas;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class DespesaFuncRepository : Repository<DespesaFunc>
    {
        public DespesaFunc GetByID(int? f, int? d)
        {
            return DbSet.Include(da => da.Funcionario).Include(da => da.Despesa).Where(a => a.IdFunc == f && a.IdDespesa == d).FirstOrDefault();
        }

        public void Delete(int id, int id2)
        {
            DbSet.Remove(GetByID(id, id2));
        }
    }
}