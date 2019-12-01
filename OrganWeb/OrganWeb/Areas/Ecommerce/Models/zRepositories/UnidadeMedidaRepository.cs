using Newtonsoft.Json;
using OrganWeb.Areas.Ecommerce.Models.API;
using RestSharp;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using OrganWeb.Areas.Ecommerce.Models.zBanco;

namespace OrganWeb.Areas.Ecommerce.Models.zRepositories
{
    public class UnidadeMedidaRepository : EcommerceRepository<UnidadeCadastro>
    {
        public async Task<ListarUnidades> GetListarUnidades()
        {
            var client = new RestClient("https://app.omie.com.br");
            var request = new RestRequest("/api/v1/geral/unidade/?JSON={\"call\":\"ListarUnidades\",\"app_key\":\"1560731700\",\"app_secret\":\"226dcf372489bb45ceede61bfd98f0f1\",\"param\":[{\"codigo\":\"\"}]}", Method.POST);
            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteTaskAsync(request);
            var listar = JsonConvert.DeserializeObject<ListarUnidades>(response.Content);
            var aa = listar.UnidadeCadastros.Where(x => x.Id == "DZ    " || x.Id == "UN    " || x.Id == "G     " || x.Id == "L     " || x.Id == "ML    " || x.Id == "KG    " || x.Id == "T     " || x.Id == "PC    " || x.Id == "CX    " || x.Id == "FL    " || x.Id == "PCT   " || x.Id == "BL    " || x.Id == "MIL   " || x.Id == "RES   " || x.Id == "CJ    " || x.Id == "PR    " || x.Id == "BR    " || x.Id == "FR    " || x.Id == "BD    " || x.Id == "RL    " || x.Id == "TB    " || x.Id == "CNT   " || x.Id == "CH" || x.Id == "SC    " || x.Id == "BN    " || x.Id == "CN    " || x.Id == "GF    " || x.Id == "QT    " || x.Id == "FD    " || x.Id == "LT5   " || x.Id == "EMB   " || x.Id == "CTL   " || x.Id == "CON   " || x.Id == "FA    " || x.Id == "MG    " || x.Id == "POT   " || x.Id == "TBL   " || x.Id == "MC    " || x.Id == "MD    " || x.Id == "KIT   " || x.Id == "PAL   " || x.Id == "CIL   " || x.Id == "TUB   " || x.Id == "BAR   " || x.Id == "CG    " || x.Id == "LOT   " || x.Id == "BRL   " || x.Id == "TON   " || x.Id == "TQN   " || x.Id == "PARES " || x.Id == "LATA  " || x.Id == "1000UN" || x.Id == "TO    " || x.Id == "DUZIA " || x.Id == "PET   " || x.Id == "SCH   " || x.Id == "MAQ   " || x.Id == "FRASCO" || x.Id == "AMPOLA" || x.Id == "BS    " || x.Id == "BIS   " || x.Id == "POR   " || x.Id == "SCA   " || x.Id == "DEZ   " || x.Id == "VARA  " || x.Id == "VIDRO " || x.Id == "CXTE  " || x.Id == "CDA   " || x.Id == "UL    " || x.Id == "BAG   ").ToList();
            listar.UnidadeCadastros = aa;
            return listar;
        }

        public async Task<UnidadeCadastro> GetByID(string id)
        {
            try
            {
                return await DbSet.Where(x => x.Id == id).FirstAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}