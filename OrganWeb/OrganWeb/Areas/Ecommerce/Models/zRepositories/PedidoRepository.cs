using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using PagedList.EntityFramework;
using Microsoft.AspNet.Identity;
using PagedList;

namespace OrganWeb.Areas.Ecommerce.Models.zRepositories
{
    public class PedidoAnuncioRepository : EcommerceRepository<PedidoAnuncio>
    {
        public async Task<IPagedList<PedidoAnuncio>> GetPedidosAnunciante(int page)
        {
            string id = HttpContext.Current.User.Identity.GetUserId();
            return await DbSet.Include(e => e.Anuncio).Include(o => o.Pedido).Where(x => x.Anuncio.IdAnunciante == id).OrderBy(p => p.Anuncio.Id).ToPagedListAsync(page, 10);
        }

        public async Task<IPagedList<PedidoAnuncio>> GetPedidosCliente(int page)
        {
            string id = HttpContext.Current.User.Identity.GetUserId();
            return await DbSet.Include(e => e.Anuncio).Include(o => o.Pedido).Where(x => x.Pedido.IdUsuario == id).OrderBy(p => p.Anuncio.Id).ToPagedListAsync(page, 10);
        }
    }
}