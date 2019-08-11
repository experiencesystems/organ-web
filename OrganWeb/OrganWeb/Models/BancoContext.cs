using System;
using System.Collections.Generic;
using System.Data.Entity;
using OrganWeb.Areas.Sistema.Models;
using System.Linq;
using System.Web;
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganWeb.Models
{
    public class BancoContext : DbContext
    {
        public BancoContext() : base("name=BancoContext") { }

        public virtual DbSet<Semente> Sementes { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Praga> Pragas { get; set; }
        public virtual DbSet<Doenca> Doencas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

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