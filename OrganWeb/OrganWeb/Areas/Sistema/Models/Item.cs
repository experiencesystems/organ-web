using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Item : Repository<Item>
    {
        public int IDItem { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double ValorUnit { get; set; }



        //FK
        public int IDEstoque { get; set; }
        public int IDCategoria { get; set; }
    }
}

