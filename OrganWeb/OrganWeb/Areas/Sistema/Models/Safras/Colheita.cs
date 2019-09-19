using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Safras
{
    public class Colheita : Repository<Colheita>
    {
        [Key]
        public int IDColheita { get; set; }

        public DateTime Data { get; set; }
        public double Quantidade { get; set; }
        public int CodEstoque { get; set; }
        public double QtdPerdas { get; set; }

        //TODO: Verificar FKs Colheita
    }
}