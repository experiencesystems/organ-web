using OrganWeb.Models.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Funcionarios
{
    [Table("tbUsuarioFunc")]
    public class UsuarioFunc
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Funcionario")]
        public int IdFunc { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Usuario")]
        public string IdUsuario { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}