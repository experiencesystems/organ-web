using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Financas
{
    [Table("tbDespesaAdm")]
    public class DespesaAdm
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Despesa")]
        public int IdDespesa { get; set; }
        
        [Key, Column(Order = 2)]
        [ForeignKey("Conta")] //TODO: FK tbDespesaAdm IdConta
        public int IdConta { get; set; }

        public virtual Despesa Despesa { get; set; }
        public virtual Conta Conta { get; set; }
    }
}