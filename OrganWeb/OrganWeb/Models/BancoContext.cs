using System;
using System.Collections.Generic;
using System.Data.Entity;
using OrganWeb.Areas.Sistema.Models;
using System.Linq;
using System.Web;
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

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

            base.OnModelCreating(modelBuilder);
        }

        public static BancoContext Create()
        {
            return new BancoContext();
        }
    }
}