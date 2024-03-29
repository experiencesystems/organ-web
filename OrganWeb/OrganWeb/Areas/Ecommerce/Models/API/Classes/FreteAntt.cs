﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OrganWeb.Areas.Ecommerce.Models.API.Classes
{
    public partial class FreteAntt
    {
        [JsonProperty("TipoCargaEnum")]
        public long TipoCargaEnum { get; set; }

        [JsonProperty("TipoCargaStr")]
        public string TipoCargaStr { get; set; }

        [JsonProperty("TotalEixo")]
        public long TotalEixo { get; set; }

        [JsonProperty("DistanciaKM")]
        public long DistanciaKm { get; set; }

        [JsonProperty("ValorFrete")]
        public double ValorFrete { get; set; }

        [JsonProperty("CargaLotacao")]
        public long CargaLotacao { get; set; }

        [JsonProperty("CargaLotacaoStr")]
        public object CargaLotacaoStr { get; set; }
    }

    public partial class FreteAntt
    {
        public static FreteAntt FromJson(string json) => JsonConvert.DeserializeObject<FreteAntt>(json, Converter.Settings);
    }
}