using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Financas
{
    [Table("tbCompra")]
    public class Compra : Repository<Compra>
    {
        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Desconto { get; set; }
    }
}