using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Especificacao : Repository<Especificacao>
    {
        [Key]
        public int EspecificacaoID { get; set; }
        public string Descricao { get; set; }

        //Estadio = n
        //Cultura = n
    }
}