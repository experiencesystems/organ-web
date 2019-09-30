using OrganWeb.Models.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Pessoa
{
    [Table("tbPessoaUsuario")]
    public class PessoaUsuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public string IdUser { get; set; }

        [Required]
        [ForeignKey("Pessoa")]
        public string IdPessoa { get; set; }

        public virtual ApplicationUser Usuario { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}