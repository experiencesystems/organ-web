using OrganWeb.Areas.Sistema.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models
{
    [Table("tbTelefone")]
    public class Telefone : Repository<Telefone>
    {
        [Key]
        public int Id { get; set; }
        public int DDD { get; set; }
        public Int64 Numero { get; set; }
        public string Tipo { get; set; }

        public virtual List<Funcionario> Funcionarios { get; set; }
    }
}