using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Models
{
    public class Telefone : Repository<Telefone>
    {
        [Key]
        public int TelefoneID { get; set; }
        public int DDD { get; set; }
        public Int64 Numero { get; set; }
        public string Tipo { get; set; }
    }
}