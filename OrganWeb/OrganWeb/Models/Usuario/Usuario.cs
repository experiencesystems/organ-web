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

namespace OrganWeb.Models.Usuario
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public DateTime DataCadastro { get; set; }
        [Required]
        public bool Confirmacao { get; set; }
        [Required]
        public bool Assinatura { get; set; }
        [Required]
        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }

        /*
        public virtual bool PhoneNumberConfirmed { get; set; }
        public virtual bool TwoFactorEnabled { get; set; }
        public virtual DateTime? LockoutEndDateUtc { get; set; }
        public virtual bool LockoutEnabled { get; set; }
        public virtual int AccessFailedCount { get; set; }
        public virtual ICollection<TRole> Roles { get; }
        public virtual ICollection<TClaim> Claims { get; }
        public virtual ICollection<TLogin> Logins { get; }*/

        public virtual Pessoa.Pessoa Pessoa { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Observe que o authenticationType deve corresponder àquele definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Adicionar declarações de usuário personalizado aqui
            return userIdentity;
        }
    }
}