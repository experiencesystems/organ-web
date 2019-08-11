using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Monitoramento : Repository<Monitoramento>
    {
        [Key]
        public int IDMonitoramento { get; set; }

        public string Nome { get; set; }

        public string Observacao { get; set; }

        public string Status { get; set; }

        public string Resultado { get; set; }




    }
}