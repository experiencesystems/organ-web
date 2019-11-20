using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.API.Classes
{
    public partial class EnderecoJson
    {
        [JsonProperty("CEP")]
        public string Cep { get; set; }

        [JsonProperty("UF")]
        public string Uf { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("District")]
        public string District { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }

    public partial class EnderecoJson
    {
        public static EnderecoJson FromJson(string json) => JsonConvert.DeserializeObject<EnderecoJson>(json, Converter.Settings);
    }
}