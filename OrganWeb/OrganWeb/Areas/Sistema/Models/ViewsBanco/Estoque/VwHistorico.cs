using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque
{
    //TODO: dbcontext vwhistorico
    [Table("vwHistorico")]
    public class VwHistorico
    {
        [Key]
        public int Id { get; set; }

        //TODO: mapear atributos vwhistorico
        public int IdItem { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Fornecedor { get; set; }
        public double Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Descricao { get; set; }
    }
}