using OrganWeb.Areas.Sistema.Models.Financas;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class ItensCompradosRepository : Repository<ItensComprados>
    {
        public void DeleteByIdCompra(Compra compra)
        {
            foreach (var item in DbSet.Where(x => x.IdCompra == compra.Id).ToList())
            {
                DbSet.Remove(item);
                Save();
            }
        }
    }
}