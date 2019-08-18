using System;
using System.Collections.Generic;
using System.Data.Entity;
using OrganWeb.Areas.Sistema.Models;
using System.Linq;
using System.Web;
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;

namespace OrganWeb.Models
{
    public class BancoContext : IdentityDbContext<ApplicationUser>
    {
        public BancoContext() : base("name=BancoContext", throwIfV1Schema: false) { }

        public virtual DbSet<Semente> Sementes { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        //public virtual DbSet<Praga> Pragas { get; set; }
        //public virtual DbSet<Doenca> Doencas { get; set; }
        public virtual DbSet<User> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Semente>()
            .HasRequired(c => c.Categoria)
            .WithMany(b => b.Sementes)
            .WillCascadeOnDelete(false);

            //TODO: Properties ApplicationUser
            /*modelBuilder.Entity<ApplicationUser>()
                .ToTable(tableName: "tbUsuario")
                .Property(e => e.EmailConfirmed).HasColumnName("Confirmacao");

            modelConfiguration.Entity<IdentityUserClaim<int>>()
                .Has(e =>
                {
                    e.ToTable("tbUsuarioClaims");
                    e.Property(en => en.UserId).HasColumnName("IdUsuario");
                    e.Property(en => en.ClaimType).HasColumnName("TipoClaim");
                    e.Property(en => en.ClaimValue).HasColumnName("ValorClaim");
                }
                );
            
            modelConfiguration.Entity<IdentityUserLogin<int>>()
                .Has(e =>
                {
                    e.ToTable("tbLogin");
                    e.Property(en => en.UserId).HasColumnName("IdUsuario");
                }
                );

            modelConfiguration.Entity<IdentityUserRole<int>>()
                .Has(e =>
                {
                    e.ToTable("tbTipoUsuario");
                    e.Property(en => en.UserId).HasColumnName("IdUsuario");
                    e.Property(en => en.RoleId).HasColumnName("IdTipo");
                }
                );

            modelConfiguration.Entity<IdentityRole<int, IdentityUserRole<int>>>()
                .Has(e =>
                {
                    e.ToTable("tbTipo");
                    e.Property(en => en.Id).HasColumnName("Id");
                    e.Property(en => en.Name).HasColumnName("Nome");
                }
                );*/

            base.OnModelCreating(modelBuilder);
        }

        public static BancoContext Create()
        {
            return new BancoContext();
        }
    }
}