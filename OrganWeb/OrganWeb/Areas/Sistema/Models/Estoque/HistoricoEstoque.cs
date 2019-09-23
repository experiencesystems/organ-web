using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Estoque
{
    [Table("tbHistEstoque")]
    public class HistoricoEstoque : Repository<HistoricoEstoque>
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }
        [Key]
        [Column(Order = 2)]
        public DateTime DtAlteracao { get; set; }

        [Required]
        [Range(0.01, 99999.99)]
        public double QtdAlterada { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string DescAlteracao { get; set; }

        [Required]
        [Range(0.01, 99999.99)]
        public double QtdAntiga { get; set; }

        public virtual Estoque Estoque { get; set; }
    }
}