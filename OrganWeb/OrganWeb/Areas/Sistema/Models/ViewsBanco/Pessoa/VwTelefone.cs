using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa
{
    [Table("vwTelefone")]
    public class VwTelefone
    {
        [Key]
        public int Id { get; set; }
        public string Telefone { get; set; }
    }
}