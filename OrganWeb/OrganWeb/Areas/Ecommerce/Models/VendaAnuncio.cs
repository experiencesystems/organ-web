using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models
{
    public class VendaAnuncio
    {
        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Contrato{ get; set; }
        public int StatusEntrega { get; set; }
       
        [ForeignKey("Anuncio")]
        public int IdAnuncio { get; set; }

        [ForeignKey("Pagamento")]
        public int IdPagamento { get; set; }

        public virtual Pagamento Pagamento { get; set; }
    }
}