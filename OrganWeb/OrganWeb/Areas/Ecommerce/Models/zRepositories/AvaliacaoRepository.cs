using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace OrganWeb.Areas.Ecommerce.Models.zRepositories
{
    public class AvaliacaoRepository : EcommerceRepository<Avaliacao>
    {
        public async Task<List<Avaliacao>> GetAvaliacoes(Anuncio anuncio)
        {
            return await DbSet.Include(a => a.Anuncio).Include(u => u.Usuario).Where(x => x.IdAnuncio == anuncio.Id).ToListAsync();
        }
    }
}