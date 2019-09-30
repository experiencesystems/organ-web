using System.Data.Entity;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models.Ferramentas;
using OrganWeb.Areas.Sistema.Models.Financas;
using Microsoft.AspNet.Identity.EntityFramework;
using OrganWeb.Models.Endereco;
using OrganWeb.Models.Telefone;
using OrganWeb.Models.Pessoa;
using OrganWeb.Models.Usuario;
using OrganWeb.Models.Financeiro;

namespace OrganWeb.Models.Banco
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class BancoContext : IdentityDbContext<ApplicationUser>
    {
        public BancoContext() : base("name=BancoContext", throwIfV1Schema: false) { }

        // USUÁRIO
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        // ENDEREÇO
        public DbSet<Endereco.Endereco> Enderecos { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }

        // TELEFONE
        public DbSet<Telefone.Telefone> Telefones { get; set; }
        public DbSet<TipoTel> TipoTels { get; set; }
        public DbSet<DDD> DDDs { get; set; }

        // PESSOA
        public DbSet<Pessoa.Pessoa> Pessoas { get; set; }
        public DbSet<PessoaFisica> PessoaFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoaJuridicas { get; set; }
        public DbSet<PessoaUsuario> PessoaUsuarios { get; set; }
        public DbSet<TelefonePessoa> TelefonePessoas { get; set; }

        // FINANCEIRO
        public DbSet<DadosBancario> DadosBancarios { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<ItensComprados> ItensComprados { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItensVendidos> ItensVendidos { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<DespesaAdm> DespesaAdms { get; set; }
        public DbSet<DespesaFunc> DespesaFuncs { get; set; }

        // ARMAZENAMENTO
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<HistoricoEstoque> HistoricoEstoques { get; set; }
        public DbSet<Semente> Sementes { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fornecedor> Fornecedors { get; set; }

        // ADMINISTRATIVO
        public DbSet<Solo> Solos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        // FERRAMENTAS
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Manutencao> Manutencaos { get; set; }
        public DbSet<MaquinaManutencao> MaquinaManutencaos { get; set; }

        // FUNCIONÁRIO
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<FuncEquipe> FuncEquipes { get; set; }

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

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.CliOrFunc)
                .HasColumnName("CLI/FUNC");

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

            // RELAÇÕES

            //modelBuilder.Entity<Funcionario>()
            //.HasRequired(u => u.User)
            //.WithRequiredDependent(u => u.Funcionario)
            //.Map(u => u.MapKey("IdUsuario"));

            /*modelBuilder.Entity<Funcionario>()
            .HasRequired(f => f.Cargo)
            .WithMany(c => c.Funcionarios)
            .HasForeignKey(f => f.IdCargo);
            
            modelBuilder.Entity<Fazenda>()
            .HasRequired(f => f.Localizacao)
            .WithRequiredDependent(l => l.Fazenda);*/
        }

        public static BancoContext Create()
        {
            return new BancoContext();
        }
    }
}