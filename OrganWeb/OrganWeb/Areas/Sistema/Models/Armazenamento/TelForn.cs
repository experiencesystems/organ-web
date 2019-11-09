using OrganWeb.Models.Telefone;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbTelForn")]
    public class TelForn
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Fornecedor")]
        public int IdForn { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Telefone")]
        public int IdTelefone { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Telefone Telefone { get; set; }
    }
}