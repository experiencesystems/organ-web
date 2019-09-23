using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Financas
{
    [Table("tbDespesa")]
    public class Despesa : Repository<Despesa>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        [Range(0.01, 99999.99)]
        public double Valor { get; set; }
    }
}