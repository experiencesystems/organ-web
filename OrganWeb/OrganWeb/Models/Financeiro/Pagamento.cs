using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Financeiro
{
    [Table("tbPagamento")]
    public class Pagamento : Repository<Pagamento>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0.01, 99999.99)]
        public double Valor { get; set; }

        [Required]
        public int Parcelas { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string TipoPagamento { get; set; }
    }
}