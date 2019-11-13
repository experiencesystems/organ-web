using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using OrganWeb.Areas.Sistema.Models.zRepositories;

namespace OrganWeb.Areas.Sistema.Models.API
{
    public partial class ListarUnidades : UnidadeMedidaRepository
    {
        [JsonProperty("unidade_cadastro")]
        public List<UnidadeCadastro> UnidadeCadastros { get; set; }
    }

    [Table("tbUM")]
    public partial class UnidadeCadastro : UnidadeMedidaRepository
    {
        [Key]
        [JsonProperty("codigo")]
        public string Id { get; set; }

        [JsonProperty("descricao")]
        public string Desc { get; set; }
    }
}