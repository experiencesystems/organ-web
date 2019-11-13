using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace OrganWeb.Areas.Ecommerce.Models.zRepositories
{
    public class PedidoRepository : EcommerceRepository<Pedido>
    {
        public async Task<List<Pedido>> GetPedidosAnunciante()
        {
            return await DbSet.Include(a => a.Carrinho).Where(x => x.Anuncio.IdUsuario == HttpContext.Current.User.Identity.GetUserId()).ToListAsync();
        }

        public async Task<List<Pedido>> GetPedidosCliente()
        {
            return await DbSet.Include(a => a.Carrinho).Where(x => x.Carrinho.IdUsuario == HttpContext.Current.User.Identity.GetUserId()).ToListAsync();
        }
    }
}