using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using PagedList.EntityFramework;
using PagedList;
using System.Web.Http;

namespace OrganWeb.Areas.Ecommerce.Models.zRepositories
{
    public class WishlistRepository : EcommerceRepository<Wishlist>
    {
        public async Task<IPagedList<Wishlist>> GetPagedAll(int page)
        {
            var id = HttpContext.Current.User.Identity.GetUserId();
            return await DbSet.Include(a => a.Anuncio).Include(u => u.Usuario).Where(x => x.IdUsuario == id).OrderBy(p => p.IdAnuncio).ToPagedListAsync(page, 5);
        }

        public async Task<List<Wishlist>> GetWishlist()
        {
            var id = HttpContext.Current.User.Identity.GetUserId();
            return await DbSet.Include(a => a.Anuncio).Include(u => u.Usuario).Where(x => x.IdUsuario == id).AsNoTracking().ToListAsync();
        }

        public async Task<Wishlist> GetWishlist(Anuncio anuncio)
        {
            var id = HttpContext.Current.User.Identity.GetUserId();
            return await DbSet.Include(a => a.Anuncio).Include(u => u.Usuario).Where(x => x.IdUsuario == id && x.IdAnuncio == anuncio.Id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task AddWishlist(Anuncio anuncio)
        {
            if (await GetWishlist(anuncio) == null)
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
        }

        public async Task DeleteItemWishlist(Anuncio anuncio)
        {
            var wishlist = await GetWishlist(anuncio);
            DbSet.Attach(wishlist);
            DbSet.Remove(wishlist);
            await Save();
        }
        
        public async Task LimparWishlist()
        {
            var wishlist = await GetWishlist();
            foreach (var item in wishlist)
            {
                DbSet.Attach(item);
                DbSet.Remove(item);
                await Save();
            }
            wishlist = null;
        }
    }
}