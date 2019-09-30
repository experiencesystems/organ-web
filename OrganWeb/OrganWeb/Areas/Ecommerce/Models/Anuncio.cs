using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models
{
    public class Anuncio
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public string Foto { get; set; }
        public double Desconto { get; set; }
    }
}