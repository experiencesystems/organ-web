using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.zRepositories;
using OrganWeb.Models;
using OrganWeb.Models.Banco;
using OrganWeb.Models.Pessoa;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbFornecedor")]
    public class Fornecedor : Repository<Fornecedor>
    {
        [Key]
        public int Id { get; set; }

        public bool Status { get; set; }

        [Required]
        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}