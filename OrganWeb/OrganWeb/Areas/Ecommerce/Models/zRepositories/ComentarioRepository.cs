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
    public class ComentarioRepository : EcommerceRepository<Comentario>
    {
        public Task<List<Comentario>> GetComentarios(Anuncio anuncio)
        {
            return DbSet.Include(a => a.Anuncio).Include(b => b.Usuario).Where(e => e.IdAnuncio == anuncio.Id).ToListAsync();
        }
    }
}