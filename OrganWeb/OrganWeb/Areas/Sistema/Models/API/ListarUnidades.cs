using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OrganWeb.Areas.Sistema.Models.API
{
    public partial class ListarUnidades
    {
        [JsonProperty("unidade_cadastro")]
        public UnidadeCadastro[] UnidadeCadastro { get; set; }
    }

    public partial class UnidadeCadastro
    {
        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }
    }

    public partial class ListarUnidades
    {
        public static ListarUnidades FromJson(string json) => JsonConvert.DeserializeObject<ListarUnidades>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ListarUnidades self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}