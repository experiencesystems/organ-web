using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Telefone
{
    [Table("tbDDD")]
    public class DDD : OrganRepository<DDD>
    {
        [Key]
        public int Valor { get; set; }
    }
}