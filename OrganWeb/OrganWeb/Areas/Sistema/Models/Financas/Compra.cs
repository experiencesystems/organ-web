using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Models;
using OrganWeb.Models.Banco;
using OrganWeb.Models.Financeiro;
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

        [Required]
        public DateTime Data { get; set; }

        [Range(0.01, 999.99)]
        public double Desconto { get; set; }

        [Required]
        [ForeignKey("Fornecedor")]
        public int IdForn { get; set; }

        [Required]
        [ForeignKey("Pagamento")]
        public int IdPagmento { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Pagamento Pagamento { get; set; }
    }
}