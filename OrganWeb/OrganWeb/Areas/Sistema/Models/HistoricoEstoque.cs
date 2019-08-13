using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class HistoricoEstoque : Repository<HistoricoEstoque>
    {
        [Key]
        public int IDEstoque { get; set; } //fk talvez

        public DateTime DataAlteracao { get; set; }

        public double QtdAlterada { get; set; }

        public string DescAlteracao { get; set; }

        public double QtdAntiga { get; set; }

        public string UnidadeMedida { get; set; }
    }
}