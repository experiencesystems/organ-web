using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Endereco
{
    [Table("vwEndereco")]
    public class VwEndereco
    {
        [Key]
        public string CEP { get; set; }
        public string Rua { get; set; }
        public string BCF { get; set; }
    }
}