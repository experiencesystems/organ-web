using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Models.Banco;

namespace OrganWeb.Areas.Sistema.Models.Financas
{
    [Table("tbDespesa")]
    public class Despesa : Repository<Despesa>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Valor pago")]
        public double ValorPago { get; set; }

        //TODO: [Required] Data despesa
        public DateTime Data { get; set; }
    }
}