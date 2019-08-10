using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Despesa : Repository<Despesa>
    {
        [Key]
        public int IDDespesa { get; set; }

        public DateTime Data { get; set; }

        public string Categoria { get; set; }

        public string Descricao { get; set; }

        public int IDPagamento { get; set; }




    }
}