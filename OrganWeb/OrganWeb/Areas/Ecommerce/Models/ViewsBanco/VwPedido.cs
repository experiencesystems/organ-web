using Microsoft.AspNet.Identity;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.ViewsBanco
{
    [Table("vwPedido")]
    public class VwPedido : EcommerceRepository<VwPedido>
    {
        [Key]
        public int Id { get; set; }
        public string Anunciante { get; set; }
        public string IdAnunciante { get; set; }
        public int IdAnuncio { get; set; }
        public string Anuncio { get; set; }
        public string NomeAnuncioQtd { get; set; }
        public double ValorTotalsDesconto { get; set; }
        public double ValorTotalcDesconto { get; set; }
        public string IdCliente { get; set; }
        public string Comprador { get; set; }
        public string Endereco { get; set; }
        public double ValorFrete { get; set; }
        public string Situacao { get; set; }
        public string Data { get; set; }

        public async Task<IPagedList<VwPedido>> GetPedidosAnunciante(int page)
        {
            string id = HttpContext.Current.User.Identity.GetUserId();
            return await DbSet.Where(x => x.IdAnunciante == id).OrderBy(p => p.Id).ToPagedListAsync(page, 10);
        }
    }
}