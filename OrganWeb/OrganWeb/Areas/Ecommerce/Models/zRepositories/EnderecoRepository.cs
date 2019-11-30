using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace OrganWeb.Areas.Ecommerce.Models.zRepositories
{
    public class EnderecoRepository : EcommerceRepository<Endereco.Endereco>
    {
        public Task<Endereco.Endereco> GetByID(string cep)
        {
            return DbSet.Where(x => x.CEP == cep).FirstOrDefaultAsync();
        }
    }
}