using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Models
{
    public class Pagamento : Repository<Pagamento>
    {
        [Key]
        public int PagamentoID { get; set; }
        public decimal Valor { get; set; }
        public int Parcelas { get; set; }
        public int Quantidade { get; set; }
        public string Tipo { get; set; }

        //Compra = n
        //Despesa = n
    }
}