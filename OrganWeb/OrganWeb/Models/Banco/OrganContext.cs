using System.Data.Entity;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Administrativo;
using Microsoft.AspNet.Identity.EntityFramework;
using OrganWeb.Areas.Sistema.Models.Controles;
using OrganWeb.Areas.Sistema.Models.Praga_e_Doenca;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Ecommerce.Models.Endereco;
using OrganWeb.Areas.Sistema.Models.Telefone;
using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Sistema.Models.Usuario;
using OrganWeb.Areas.Ecommerce.Models.Financeiro;

namespace OrganWeb.Models.Banco
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class OrganContext : IdentityDbContext<ApplicationUser>
    {
        public OrganContext() : base("name=OrganContext", throwIfV1Schema: false) { }

        // SISTEMA
        // ADMINISTRATIVO
        public DbSet<Area> Areas { get; set; }
        public DbSet<Solo> Solos { get; set; }

        // ARMAZENAMENTO
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<TelForn> TelForns { get; set; }
        public DbSet<Fornecedor> Fornecedors { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<HistoricoEstoque> HistoricoEstoques { get; set; }

        // CONTROLES
        public DbSet<Controle> Controles { get; set; }
        public DbSet<ItensControle> ItensControles { get; set; }
        public DbSet<FuncControle> FuncControles { get; set; }

        // FINANCEIRO
        public DbSet<DadosBancario> DadosBancarios { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        // FUNCIONÁRIO
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<TelFunc> TelFuncs { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        // PRAGA/DOENÇA
        public DbSet<AreaPD> AreaPDs { get; set; }
        public DbSet<ControlePD> ControlePDs { get; set; }
        public DbSet<PragaOrDoenca> PragaOrDoencas { get; set; }

        // SAFRAS
        public DbSet<AreaPlantio> AreaPlantios { get; set; }
        public DbSet<Colheita> Colheitas { get; set; }
        public DbSet<ItensPlantio> ItensPlantios { get; set; }
        public DbSet<Plantio> Plantios { get; set; }
        public DbSet<FuncPlantio> FuncPlantios { get; set; }
        public DbSet<Semente> Sementes { get; set; }

        // VIEWS
        public DbSet<VwEndereco> VwEnderecos { get; set; }
        public DbSet<VwTelefone> VwTelefones { get; set; }
        public DbSet<VwFuncionario> VwFuncionarios { get; set; }
        public DbSet<VwItems> VwItems { get; set; }
        public DbSet<VwPragaOrDoenca> VwPragaOrDoencas { get; set; }
        public DbSet<VwHistorico> VwHistoricos { get; set; }
        public DbSet<VwControle> VwControles { get; set; }

        // ENDEREÇO
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }

        // TELEFONE
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<TipoTel> TipoTels { get; set; }
        public DbSet<DDD> DDDs { get; set; }

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MAPEAMENTO DOS NOMES

            modelBuilder.Entity<Cargo>()
                .Property(t => t.Nome)
                .HasColumnName("Cargo");

            modelBuilder.Entity<Categoria>()
                .Property(t => t.Nome)
                .HasColumnName("Categoria");

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

            modelBuilder.Entity<DDD>()
                .Property(t => t.Valor)
                .HasColumnName("DDD");

            modelBuilder.Entity<Categoria>()
                .Property(t => t.Nome)
                .HasColumnName("Categoria");

            modelBuilder.Entity<PragaOrDoenca>()
                .Property(t => t.PD)
                .HasColumnName("P/D");

            modelBuilder.Entity<VwFornecedor>()
                .Property(t => t.RazaoSocial)
                .HasColumnName("Razão Social");

            modelBuilder.Entity<VwPragaOrDoenca>()
                .Property(t => t.Areas)
                .HasColumnName("Áreas");

            modelBuilder.Entity<VwControle>()
                .Property(t => t.Descricao)
                .HasColumnName("Descrição");

            modelBuilder.Entity<VwControle>()
                .Property(t => t.Eficiencia)
                .HasColumnName("Eficiência(%)");

            modelBuilder.Entity<VwControle>()
                .Property(t => t.Liberacoes)
                .HasColumnName("Número de Liberações");

            modelBuilder.Entity<VwControle>()
                .Property(t => t.Nome)
                .HasColumnName("Pragas/Doenças");

            modelBuilder.Entity<Comentario>()
                .Property(t => t.Valor)
                .HasColumnName("Comentario");

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

            modelBuilder.Entity<Resposta>().HasKey(vf => new { vf.IdComentario, vf.IdResposta });

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
                .Property(t => t.EmailConfirmed)
                .HasColumnName("ConfirmacaoEmail");
        }

        public static OrganContext Create()
        {
            return new OrganContext();
        }
    }
}