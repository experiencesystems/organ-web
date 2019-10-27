using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Pessoa
{
    [Table("tbTelefonePessoa")]
    public class TelefonePessoa : Repository<TelefonePessoa>
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Telefone")]
        public int IdTelefone { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual Telefone.Telefone Telefone { get; set; }
    }
}