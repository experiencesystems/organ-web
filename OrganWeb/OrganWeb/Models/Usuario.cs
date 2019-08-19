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

    [Table("tbUsuario")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public bool Confirmacao { get; set; }
        public bool Assinatura { get; set; }
        public bool CliOrFunc { get; set; }
        public DateTime DataCadastro { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Funcionario Funcionario { get; set; }
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

    /*[Table("tbUsuario")]
    public partial class User : IdentityUser
    {
        [Key]
        public IKey ID { get; set; }
        public string Senha { get; set; }
        public bool Confirmacao { get; set; }
        public bool Assinatura { get; set; }
        public bool CliOrFunc { get; set; }
        public DateTime DataCadastro { get; set; }
        public System.Guid CodigoAtivacao { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Observe que o authenticationType deve corresponder àquele definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Adicionar declarações de usuário personalizado aqui
            return userIdentity;
        }
    }*/

    /*public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public int AtivacaoUsuario { get; set; }
        public string Email { get; set; }

        //Talvez seja FK
        public int Assinatura { get; set; }

        public int TipoUsuario { get; set; }
    }*/
}