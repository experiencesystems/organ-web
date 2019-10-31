using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OrganWeb.Areas.Sistema.Models.API
{
    public partial class ListarUnidades
    {
        [JsonProperty("unidade_cadastro")]
        public List<UnidadeCadastro> UnidadeCadastros { get; set; }
    }

    public partial class UnidadeCadastro
    {
        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }
    }
}