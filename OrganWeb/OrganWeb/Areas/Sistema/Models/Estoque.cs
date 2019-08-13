using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Estoque : Repository<Estoque>
    {
        [Key]
        public int IDEstoque { get; set; }

        public double Quantidade { get; set; }

        public string UnidadeMedida { get; set; }

        //TODO: Verificar FKs Estoque
    }
}