using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa
{
    [Table("vwFornecedor")]
    public class VwFornecedor : Repository<VwFornecedor>
    {
        [Key]
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Telefones { get; set; }
    }
}