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
    [Table("vwPragaOrDoenca")]
    public class VwPragaOrDoenca : PragaOrDoencaRepository
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Areas { get; set; }
        public string Status { get; set; }
    }
}