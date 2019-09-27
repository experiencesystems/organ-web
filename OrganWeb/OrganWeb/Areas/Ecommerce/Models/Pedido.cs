using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public bool Status { get; set; }
    }
}