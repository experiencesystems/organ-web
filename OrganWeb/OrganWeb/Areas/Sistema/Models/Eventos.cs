using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Eventos : Repository<Eventos>
    {
        public int IDEvento { get; set; }

        public DateTime Data { get; set; }

        public string Tipo { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        //FK

        public int IDCategoria { get; set; }
    }
}