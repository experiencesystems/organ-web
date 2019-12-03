using Microsoft.AspNet.Identity.EntityFramework;
using OrganWeb.Areas.Ecommerce.Models.Endereco;
using OrganWeb.Areas.Ecommerce.Models.Financeiro;
using OrganWeb.Areas.Ecommerce.Models.Usuarios;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.ViewsBanco;
using OrganWeb.Areas.Ecommerce.Models.API;
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
        public EcommerceContext() : base("Server=SG-Organ-1627-master.servers.mongodirector.com;Database=dbEcommerce;Uid=sgroot;Password=&qqRvKycZGFH3s6i;", throwIfV1Schema: false) { }

        // ECOMMERCE
        // ANUNCIO
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Anunciante> Anunciantes { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Avaliacao> Avaliacaos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<PedidoAnuncio> PedidoAnuncios { get; set; }
        public DbSet<PedidoVenda> PedidoVendas { get; set; }
        public DbSet<HistCarrinho> HistCarrinhos { get; set; }
        public DbSet<VwPedido> VwPedidos { get; set; }
        public DbSet<UnidadeCadastro> UnidadeCadastros { get; set; }

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

            modelBuilder.Entity<ApplicationUser>()
                    .ToTable("tbUsuario")
                    .Ignore(t => t.PhoneNumber)
                    .Ignore(t => t.PhoneNumberConfirmed)
                    .Ignore(t => t.LockoutEndDateUtc)
                    .Ignore(t => t.LockoutEnabled)
                    .Ignore(t => t.AccessFailedCount)
                    .Ignore(t => t.TwoFactorEnabled);

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.UserName)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.SecurityStamp)
                .HasColumnName("CarimboSeguranca");

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.PasswordHash)
                .HasColumnName("SenhaHash");

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.EmailConfirmed)
                .HasColumnName("ConfirmacaoEmail");

            modelBuilder.Entity<IdentityUserClaim>()
                .Property(t => t.ClaimType)
                .HasColumnName("TipoClaim");

            modelBuilder.Entity<IdentityUserClaim>()
                .Property(t => t.ClaimValue)
                .HasColumnName("ValorClaim");

            modelBuilder.Entity<VwPedido>()
                .Property(t => t.Anuncio)
                .HasColumnName("Anúncio");

            modelBuilder.Entity<VwPedido>()
                .Property(t => t.NomeAnuncioQtd)
                .HasColumnName("Nome do Anúncio - Quantidade Pedida");

            modelBuilder.Entity<VwPedido>()
                .Property(t => t.ValorTotalsDesconto)
                .HasColumnName("Valor Total s/Desconto");

            modelBuilder.Entity<VwPedido>()
                .Property(t => t.ValorTotalcDesconto)
                .HasColumnName("Valor Total c/Desconto");

            modelBuilder.Entity<VwPedido>()
                .Property(t => t.Comprador)
                .HasColumnName("Comprador - CPF");

            modelBuilder.Entity<VwPedido>()
                .Property(t => t.Endereco)
                .HasColumnName("Endereço de Entrega");

            modelBuilder.Entity<VwPedido>()
                .Property(t => t.ValorFrete)
                .HasColumnName("Valor do Frete");

            modelBuilder.Entity<VwPedido>()
                .Property(t => t.Situacao)
                .HasColumnName("Situação do Pedido");

            modelBuilder.Entity<VwPedido>()
                .Property(t => t.Data)
                .HasColumnName("Data do Pedido");

            modelBuilder.Entity<VwVenda>()
                .Property(t => t.Data)
                .HasColumnName("Data da Venda");

            modelBuilder.Entity<VwVenda>()
                .Property(t => t.NomeAnuncio)
                .HasColumnName("Nome do Anúncio - Quantidade Pedida");

            modelBuilder.Entity<VwVenda>()
                .Property(t => t.ValorTotal)
                .HasColumnName("Valor Total");

            modelBuilder.Entity<VwVenda>()
                .Property(t => t.Endereco)
                .HasColumnName("Endereço de Entrega");

            modelBuilder.Entity<VwVenda>()
                .Property(t => t.Situacao)
                .HasColumnName("Situação da Venda");
        }

        public static EcommerceContext Create()
        {
            return new EcommerceContext();
        }
    }
}