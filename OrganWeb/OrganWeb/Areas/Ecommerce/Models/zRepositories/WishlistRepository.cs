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
    public class WishlistRepository : EcommerceRepository<Wishlist>
    {
        public async Task<List<Wishlist>> GetWishlist()
        {
            return await DbSet.Include(a => a.Anuncio).Include(u => u.Usuario).Where(x => x.IdUsuario == HttpContext.Current.User.Identity.GetUserId()).ToListAsync();
        }

        public async Task<Wishlist> GetWishlist(Anuncio anuncio)
        {
            return await DbSet.Include(a => a.Anuncio).Include(u => u.Usuario).Where(x => x.IdUsuario == HttpContext.Current.User.Identity.GetUserId() && x.IdAnuncio == anuncio.Id).FirstAsync();
        }

        public async Task AddWishlist(Anuncio anuncio)
        {
            var wishlist = new Wishlist
            {
                IdAnuncio = anuncio.Id,
                IdUsuario = HttpContext.Current.User.Identity.GetUserId()
            };
            Add(wishlist);
            await Save();
            wishlist = null;
        }

        public async Task DeleteItemWishlist(Anuncio anuncio)
        {
            DbSet.Remove(await GetWishlist(anuncio));
            await Save();
        }

        public async Task LimparWishlist()
        {
            var wishlist = await GetWishlist();
            DbSet.RemoveRange(wishlist);
            wishlist = null;
            await Save();
        }
    }
}