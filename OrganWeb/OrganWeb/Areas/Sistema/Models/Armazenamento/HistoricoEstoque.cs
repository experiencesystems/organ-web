using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Models.Banco;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbHistEstoque")]
    public class HistoricoEstoque : Repository<HistoricoEstoque>
    {
        [Key]
        public int Id { get; set; }
        public double QtdAntiga { get; set; }
        public string NomeAntigo { get; set; }
        public string CategoriaAntiga { get; set; }
        public string UMAntiga { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Desc { get; set; }
                
        [Required]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        public string FornAntigo { get; set; }

        public virtual Estoque Estoque { get; set; }
    }
}