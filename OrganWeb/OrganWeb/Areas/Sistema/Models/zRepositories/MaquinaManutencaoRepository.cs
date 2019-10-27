using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Ferramentas;
using OrganWeb.Models.Banco;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class MaquinaManutencaoRepository : Repository<MaquinaManutencao>
    {
        public async Task<MaquinaManutencao> GetByID(int? man, int? maq)
        {
            return await DbSet.Include("Maquina").Include("Manutencao").Where(a => a.IdManutencao == man && a.IdMaquina == maq).FirstOrDefaultAsync();
        }

        public async void Delete(int id, int id2)
        {
            DbSet.Remove(await GetByID(id,id2));
        }
    }
}