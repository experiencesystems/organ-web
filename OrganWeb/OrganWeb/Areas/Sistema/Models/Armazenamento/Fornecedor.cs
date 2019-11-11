using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Areas.Sistema.Models.zBanco;
using OrganWeb.Areas.Sistema.Models.zRepositories;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbFornecedor")]
    public class Fornecedor : OrganRepository<Fornecedor>
    {
        [Key]
        public int Id { get; set; }

        public bool Status { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
}