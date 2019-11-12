using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity.Owin;

namespace OrganWeb.Areas.Ecommerce.Models.zRepositories
{
    public class CarrinhoRepository : EcommerceRepository<Carrinho>
    {
        public async Task<List<Carrinho>> GetCarrinho()
        {
            string id = "";
            if (HttpContext.Current != null && HttpContext.Current.User != null
                  && HttpContext.Current.User.Identity.Name != null)
            {
                id = HttpContext.Current.User.Identity.GetUserId();
            }

            return await DbSet.Include(a => a.Anuncio).Include(u => u.Usuario).Where(x => x.IdUsuario == id && x.Status == 1).ToListAsync();
        }

        public async Task<Carrinho> GetItemCarrinho(Anuncio anuncio)
        {
            string id = "";
            if (HttpContext.Current != null && HttpContext.Current.User != null
                  && HttpContext.Current.User.Identity.Name != null)
            {
                id = HttpContext.Current.User.Identity.GetUserId();
            }

            return await DbSet.Include(a => a.Anuncio).Include(u => u.Usuario).Where(x => x.IdAnuncio == anuncio.Id && x.IdUsuario == "" && x.Status == 1).FirstOrDefaultAsync();
        }

        public async Task<int> AddAoCarrinho(Anuncio anuncio, int qtd = 1)
        {
            return await AddOuRemoverCarrinho(anuncio, qtd);
        }

        public async Task<int> RemoverDoCarrinho(Anuncio anuncio)
        {
            return await AddOuRemoverCarrinho(anuncio, -1);
        }

        public async Task LimparCarrinho()
        {
            var carrinho = await GetCarrinho();
            foreach (var item in carrinho)
            {
                item.Status = 3;
                item.Update(item);
                await Save();
            }
            carrinho = null;
        }
        
        public async Task<(int QtdItens, double ValorTotal)> GetQtdETotalCarrinho()
        {
            var carrinho = await GetCarrinho();
            var subTotal = carrinho?
                .Select(c => c.Anuncio.Produto.ValorUnit * c.Qtd)
                ??
                await carrinho.AsQueryable().Select(c => c.Anuncio.Produto.ValorUnit * c.Qtd).ToListAsync();

            return (subTotal.Count(), subTotal.Sum());
        }

        private async Task<int> AddOuRemoverCarrinho(Anuncio anuncio, int qtd)
        {
            var carrinho = await GetItemCarrinho(anuncio);

            if (carrinho == null)
            {
                carrinho = new Carrinho
                {
                    IdAnuncio = anuncio.Id,
                    IdUsuario = HttpContext.Current.User.Identity.GetUserId(),
                    Qtd = qtd
                };
                Add(carrinho);
            }

            carrinho.Qtd += qtd;

            if (carrinho.Qtd <= 0)
            {
                carrinho.Qtd = 0;
                DbSet.Remove(carrinho);
            }
            await Save();
            carrinho = null;
            return await Task.FromResult(carrinho.Qtd);
        }
    }
}