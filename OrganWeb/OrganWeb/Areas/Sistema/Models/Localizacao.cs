using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Localizacao: Repository<Localizacao>
    {
        [Key]
        public int CEP { get; set; }

        public int Numero { get; set; }

        public string Rua { get; set; }

        public string Bairro { get; set; }

        public int Complemento { get; set; }

        public string Cidade { get; set; } 

        public string UF { get; set; }

        public string Coordenada { get; set; } //?????????????



    }
}