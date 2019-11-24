using Microsoft.AspNet.Identity;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using PagedList;

namespace OrganWeb.Areas.Ecommerce.Models.zRepositories
{
    public class VendaRepository : EcommerceRepository<Venda>
    {
        public async Task<IPagedList<Venda>> GetVendasDoAnunciante(int page)
        {
            return await DbSet.Include(a => a.Pedido).Include(u => u.Pagamento).Include(e => e.Endereco).Where(x => x.Pedido.Anuncio.IdAnunciante == HttpContext.Current.User.Identity.GetUserId()).OrderBy(p => p.Id).ToPagedListAsync(page, 10);
        }
    }
}