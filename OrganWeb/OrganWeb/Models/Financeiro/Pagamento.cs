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
        
        [Display(Name = "Quantidade de parcelas")]
        public int QtdParcelas { get; set; }

        [Required]
        public double VlParcela { get; set; }

        [Required]
        public int Tipo { get; set; }
    }
}