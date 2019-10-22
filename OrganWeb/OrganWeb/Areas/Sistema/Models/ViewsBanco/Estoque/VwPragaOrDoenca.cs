using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque
{
    [Table("vwPragaOrDoenca")]
    public class VwPragaOrDoenca : Repository<VwPragaOrDoenca>
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Areas { get; set; }
        public int Status { get; set; }
    }
}