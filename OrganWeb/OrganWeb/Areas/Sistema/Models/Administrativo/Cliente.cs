using OrganWeb.Models.Banco;
using OrganWeb.Models.Pessoa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Administrativo
{
    [Table("tbCliente")]
    public class Cliente : Repository<Cliente>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}