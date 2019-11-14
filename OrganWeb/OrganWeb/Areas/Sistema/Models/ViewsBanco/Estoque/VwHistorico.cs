using OrganWeb.Areas.Sistema.Models.zBanco;
using OrganWeb.Areas.Sistema.Models.zRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque
{
    [Table("vwHistorico")]
    public class VwHistorico : HistoricoRepository
    {
        [Key]
        public int Id { get; set; }
        public int IdItem { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Fornecedor { get; set; }
        public string Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Descricao { get; set; }
    }
}