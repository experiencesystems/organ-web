using OrganWeb.Areas.Sistema.Models.zRepositories;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque
{
    [Table("vwItems")]
    public class VwItems : EstoqueRepository
    {
        [Key]
        public int Id { get; set; }
        public string Item { get; set; }
        public int Quantidade { get; set; }
        //TODO: mapear nomes um e descrição vwitems
        public string UnidadeMedida { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Fornecedor { get; set; }
        public string Tipo { get; set; }
    }
}