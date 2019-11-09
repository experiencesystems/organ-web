using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa
{
    [Table("vwFuncionario")]
    public class VwFuncionario : Repository<VwFuncionario>
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Telefones { get; set; }
        public string Email { get; set; }
    }
}