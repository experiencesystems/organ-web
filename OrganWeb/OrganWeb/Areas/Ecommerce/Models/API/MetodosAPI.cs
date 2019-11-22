using OrganWeb.Areas.Ecommerce.Models.API.Classes;
using OrganWeb.Areas.Ecommerce.Models.API.Classes.Bing;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.API
{
    public static class MetodosAPI
    {
        public static async Task<string> GetFreteFromCarrinhoAsync(List<Carrinho> carrinhos, string CEPEntrega)
        {
            var fretes = new List<double>();
            double subtotais = 0;
            foreach (var carrinho in carrinhos)
            {
                var valor = await GetValorDoFrete(carrinho.Anuncio.Anunciante.CEP, CEPEntrega);
                fretes.Add(valor.ValorFrete);
                subtotais += carrinho.Anuncio.Produto.ValorUnit * carrinho.Qtd;
            }
            string mensagem = "";
            if (carrinhos.Count > 1)
                mensagem = "Valores de frete: \n";
            else
                mensagem = "Valor do frete: \n";
            double valores = 0;
            foreach (var valor in fretes)
            {
                valores += valor;
                mensagem += "R$" + Math.Round(valor, 2, MidpointRounding.ToEven) + "\n";
            }
            mensagem += "Total com o frete: R$" + Math.Round(valores + subtotais, 2, MidpointRounding.ToEven);
            return mensagem;
        }

        public static async Task<FreteAntt> GetValorDoFrete(string ceporigem, string cepdestino)
        {
            EnderecoJson origin = await GetAddress(ceporigem);
            EnderecoJson destination = await GetAddress(cepdestino);
            QueryCoordinates coordinatesorigin = await GetCoordinates(GenerateUri(origin));
            QueryCoordinates coordinatesdestination = await GetCoordinates(GenerateUri(destination));
            var distancematrix = await PostDistance(GenerateJsonBody(coordinatesorigin, coordinatesdestination));
            var aa = await ValorDoFrete(GetDistance(distancematrix));
            return aa;
        }

        public static double GetDistance(DistanceMatrix distanceMatrix)
        {
            foreach (var a in distanceMatrix.ResourceSets)
            {
                foreach (var b in a.Resources)
                {
                    foreach (var c in b.Results)
                    {
                        return c.TravelDistance;
                    }
                }
            }
            return 1;
        }

        public static string GenerateJsonBody(QueryCoordinates origin, QueryCoordinates destination)
        {
            var originarray = CoordinatesArray(origin);
            var destarray = CoordinatesArray(destination);

            Classes.Destination[] origins = new Classes.Destination[1];
            origins[0] = new Classes.Destination
            {
                Latitude = originarray[0],
                Longitude = originarray[1]
            };

            Classes.Destination[] destinations = new Classes.Destination[1];
            destinations[0] = new Classes.Destination
            {
                Latitude = destarray[0],
                Longitude = destarray[1]
            };

            BodyDistanceMatrix body = new BodyDistanceMatrix()
            {
                Origins = origins,
                Destinations = destinations,
                TravelMode = "driving"
            };

            return Serialize.ToJson(body);
        }

        public static double[] CoordinatesArray(QueryCoordinates coordinates)
        {
            double[] array = new double[2];
            foreach (var sets in coordinates.ResourceSets)
            {
                foreach (var a in sets.Resources)
                {
                    int i = 0;
                    foreach (var b in a.GeocodePoints)
                    {
                        foreach (var c in b.Coordinates)
                        {
                            array[i] = c;
                            i++;
                        }
                        if (i == 2)
                            break;
                    }
                }
            }
            return array;
        }

        public static string GenerateUri(EnderecoJson x)
        {
            string end = x.Street + ", " + x.District + ", " + x.City + " - " + x.Uf + ", " + x.Cep + ", Brazil";
            return Uri.EscapeDataString(end);
        }

        private static async Task<EnderecoJson> GetAddress(string cep)
        {
            string responseData;
            var baseAddress = new Uri("http://api.frenet.com.br/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.Add("accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("token", "EFB53FBBRF4CAR4EDARB177R7F076B125EE8");

                using (var response = await httpClient.GetAsync("CEP/Address/" + cep))
                {
                    responseData = await response.Content.ReadAsStringAsync();
                }
            }
            return EnderecoJson.FromJson(responseData);
        }

        private static async Task<QueryCoordinates> GetCoordinates(string address)
        {
            string responseData;
            var baseAddress = new Uri("http://dev.virtualearth.net/REST/v1/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                using (var response = await httpClient.GetAsync("Locations/" + address + "?includeNeighborhood=1&maxResults=1&key=Av9IgsvQJRzGVg0lZV6QFzJJrsI9gW8EOq2vxtZCpYoqBvQtJZYowjyCOXg3YXlz"))
                {
                    responseData = await response.Content.ReadAsStringAsync();
                }
            }
            return QueryCoordinates.FromJson(responseData);
        }

        private static async Task<DistanceMatrix> PostDistance(string body)
        {
            var baseAddress = new Uri("http://dev.virtualearth.net/REST/v1/");
            string responseData = "";
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                using (var content = new StringContent(body, System.Text.Encoding.Default, "application/json"))
                {
                    using (var response = await httpClient.PostAsync("Routes/DistanceMatrix?key=Av9IgsvQJRzGVg0lZV6QFzJJrsI9gW8EOq2vxtZCpYoqBvQtJZYowjyCOXg3YXlz", content))
                    {
                        responseData = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return DistanceMatrix.FromJson(responseData);
        }

        public static async Task<FreteAntt> ValorDoFrete(double distancia)
        {
            var baseAddress = new Uri("https://calculafrete.com");
            string responseData = "";
            HttpResponseMessage response = new HttpResponseMessage();
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                using (var content = new StringContent("TipoCargaEnum=4&TotalEixo=2&DistanciaKM=" + Convert.ToInt16(distancia) + "&CargaLotacao=0", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded"))
                {
                    using (response = await httpClient.PostAsync("", content))
                    {
                        responseData = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return FreteAntt.FromJson(responseData);
        }
    }
}