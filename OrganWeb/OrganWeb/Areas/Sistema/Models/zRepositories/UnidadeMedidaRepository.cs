using Newtonsoft.Json;
using OrganWeb.Areas.Sistema.Models.API;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.zRepositories
{
    public class UnidadeMedidaRepository
    {
        public async Task<ListarUnidades> GetListarUnidades()
        {
            var client = new RestClient("https://app.omie.com.br");
            var request = new RestRequest("/api/v1/geral/unidade/?JSON={\"call\":\"ListarUnidades\",\"app_key\":\"1560731700\",\"app_secret\":\"226dcf372489bb45ceede61bfd98f0f1\",\"param\":[{\"codigo\":\"\"}]}", Method.POST);
            var cancellationTokenSource = new CancellationTokenSource();
            var response = await client.ExecuteTaskAsync(request);
            return JsonConvert.DeserializeObject<ListarUnidades>(response.Content);
        }
    }
}