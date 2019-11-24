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
    public class PedidoRepository : EcommerceRepository<Pedido>
    {
        public async Task<IPagedList<Pedido>> GetPedidosAnunciante(int page)
        {
            return await DbSet.Where(x => x.Anuncio.IdAnunciante == HttpContext.Current.User.Identity.GetUserId()).OrderBy(p => p.Id).ToPagedListAsync(page, 10);
        }

        public async Task<IPagedList<Pedido>> GetPedidosCliente(int page)
        {
            return await DbSet.Where(x => x.IdUsuario == HttpContext.Current.User.Identity.GetUserId()).OrderBy(p => p.Id).ToPagedListAsync(page, 10);
        }
    }
}