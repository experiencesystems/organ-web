using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbLocalizacao")]
    public class Localizacao : Repository<Localizacao>
    {
        [Key, Column(Order = 0)]
        public char CEP { get; set; }
        [Key, Column(Order = 1)]
        public int Numero { get; set; }

        public string Endereco { get; set; }

        public string Bairro { get; set; }

        public string Complemento { get; set; }

        public string Cidade { get; set; } 

        public string UF { get; set; }

        public virtual List<Funcionario> Funcionarios { get; set; }
        public virtual List<Fazenda> Fazendas { get; set; }
    }
}