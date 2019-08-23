using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using OrganWeb.Areas.Sistema.Models;

namespace OrganWeb.Models
{
    public class Usuario : IdentityUser
    {
        public virtual User User { get; set; }
    }

    [Table("tbDadosUsuario")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }

        [Required]
        public bool Confirmacao { get; set; }

        [Required]
        public bool Assinatura { get; set; }

        [Required]
        public bool CliOrFunc { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string IdUsuario { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Observe que o authenticationType deve corresponder àquele definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Adicionar declarações de usuário personalizado aqui
            return userIdentity;
        }
    }
}