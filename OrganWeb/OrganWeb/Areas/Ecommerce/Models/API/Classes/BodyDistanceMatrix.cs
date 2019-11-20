using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.API.Classes
{
    public partial class BodyDistanceMatrix
    {
        [JsonProperty("origins")]
        public Destination[] Origins { get; set; }

        [JsonProperty("destinations")]
        public Destination[] Destinations { get; set; }

        [JsonProperty("travelMode")]
        public string TravelMode { get; set; }
    }

    public partial class Destination
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public partial class BodyDistanceMatrix
    {
        public static BodyDistanceMatrix FromJson(string json) => JsonConvert.DeserializeObject<BodyDistanceMatrix>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this BodyDistanceMatrix self) => JsonConvert.SerializeObject(self, Converter.Settings);
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