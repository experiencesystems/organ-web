using Newtonsoft.Json;
using OrganWeb.Areas.Sistema.Models.API;
using OrganWeb.Areas.Sistema.Models.zBanco;
using RestSharp;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class UnidadeMedidaRepository : OrganRepository<UnidadeCadastro>
    {
        public async Task<ListarUnidades> GetListarUnidades()
        {
            var client = new RestClient("https://app.omie.com.br");
            var request = new RestRequest("/api/v1/geral/unidade/?JSON={\"call\":\"ListarUnidades\",\"app_key\":\"1560731700\",\"app_secret\":\"226dcf372489bb45ceede61bfd98f0f1\",\"param\":[{\"codigo\":\"\"}]}", Method.POST);
            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteTaskAsync(request);
            var listar = JsonConvert.DeserializeObject<ListarUnidades>(response.Content);
            listar.UnidadeCadastros.Where(x => x.Id == "DZ" && x.Id == "UN");
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