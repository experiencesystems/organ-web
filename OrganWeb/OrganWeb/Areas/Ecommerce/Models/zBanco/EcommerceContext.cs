using Microsoft.AspNet.Identity.EntityFramework;
using OrganWeb.Areas.Ecommerce.Models.Endereco;
using OrganWeb.Areas.Ecommerce.Models.Financeiro;
using OrganWeb.Areas.Ecommerce.Models.Usuarios;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.zBanco
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class EcommerceContext : IdentityDbContext<ApplicationUser>
    {
        public EcommerceContext() : base("name=EcommerceContext", throwIfV1Schema: false) { }

        // ECOMMERCE
        // ANUNCIO
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Avaliacao> Avaliacaos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Resposta> Respostas { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Entrega> Entregas { get; set; }
        public DbSet<Pacote> Pacotes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        // FINANCEIRO
        public DbSet<DadosBancario> DadosBancarios { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        // ENDEREÇO
        public DbSet<Endereco.Endereco> Enderecos { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }

        // VIEWS
        public DbSet<VwEndereco> VwEnderecos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Logradouro>()
                .Property(t => t.Nome)
                .HasColumnName("Logradouro");

            modelBuilder.Entity<Bairro>()
                .Property(t => t.Nome)
                .HasColumnName("Bairro");

            modelBuilder.Entity<Cidade>()
                .Property(t => t.Nome)
                .HasColumnName("Cidade");

            modelBuilder.Entity<Estado>()
                .Property(t => t.Nome)
                .HasColumnName("Estado");

            modelBuilder.Entity<Comentario>()
                .Property(t => t.Valor)
                .HasColumnName("Comentario");

            modelBuilder.Entity<Resposta>().HasKey(vf => new { vf.IdComentario, vf.IdResposta });

            modelBuilder.Entity<Resposta>()
                    .HasRequired(m => m.Comentario)
                    .WithMany(t => t.Respostas)
                    .HasForeignKey(m => m.IdComentario)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resposta>()
                    .HasRequired(m => m.Comentario)
                    .WithMany(t => t.Respostas)
                    .HasForeignKey(m => m.IdResposta)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                    .ToTable("tbUsuario")
                    .Ignore(t => t.PhoneNumber)
                    .Ignore(t => t.PhoneNumberConfirmed)
                    .Ignore(t => t.LockoutEndDateUtc)
                    .Ignore(t => t.LockoutEnabled)
                    .Ignore(t => t.AccessFailedCount)
                    .Ignore(t => t.TwoFactorEnabled);

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.SecurityStamp)
                .HasColumnName("CarimboSeguranca");

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.PasswordHash)
                .HasColumnName("SenhaHash");

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.EmailConfirmed)
                .HasColumnName("ConfirmacaoEmail");
        }

        public static EcommerceContext Create()
        {
            return new EcommerceContext();
        }
    }
}