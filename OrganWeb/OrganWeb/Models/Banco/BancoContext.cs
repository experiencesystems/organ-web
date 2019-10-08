using System.Data.Entity;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.Ferramentas;
using OrganWeb.Areas.Sistema.Models.Financas;
using Microsoft.AspNet.Identity.EntityFramework;
using OrganWeb.Models.Endereco;
using OrganWeb.Models.Telefone;
using OrganWeb.Models.Pessoa;
using OrganWeb.Models.Usuario;
using OrganWeb.Models.Financeiro;
using OrganWeb.Areas.Sistema.Models.Controles;
using OrganWeb.Areas.Sistema.Models.Praga_e_Doenca;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Financeiro;

namespace OrganWeb.Models.Banco
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class BancoContext : IdentityDbContext<ApplicationUser>
    {
        public BancoContext() : base("name=BancoContext", throwIfV1Schema: false) { }

        // ADMINISTRATIVO
        public DbSet<Area> Areas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Solo> Solos { get; set; }

        // ARMAZENAMENTO
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Fornecedor> Fornecedors { get; set; }
        public DbSet<HistoricoEstoque> HistoricoEstoques { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        // CONTROLES
        public DbSet<Controle> Controles { get; set; }
        public DbSet<ItensControle> ItensControles { get; set; }

        // FERRAMENTAS
        public DbSet<Manutencao> Manutencaos { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<MaquinaManutencao> MaquinaManutencaos { get; set; }

        // FINANCEIRO
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<DadosBancario> DadosBancarios { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<DespesaAdm> DespesaAdms { get; set; }
        public DbSet<DespesaFunc> DespesaFuncs { get; set; }
        public DbSet<ItensComprados> ItensComprados { get; set; }
        public DbSet<ItensVendidos> ItensVendidos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        // FUNCIONÁRIO
        public DbSet<Cargo> Cargos { get; set; }
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
        public DbSet<Semente> Sementes { get; set; }

        // VIEWS
        public DbSet<VwEndereco> VwEnderecos { get; set; }
        public DbSet<VwTelefone> VwTelefones { get; set; }
        public DbSet<VwPessoa> VwPessoas { get; set; }
        public DbSet<VwPessoaFisica> VwPessoaFisicas { get; set; }
        public DbSet<VwPessoaJuridica> VwPessoaJuridicas { get; set; }
        public DbSet<VwFuncionario> VwFuncionarios { get; set; }
        public DbSet<VwItems> VwItems { get; set; }
        public DbSet<VwCompra> VwCompras { get; set; }
        public DbSet<VwSaida> VwSaidas { get; set; }
        public DbSet<VwVenda> VwVendas { get; set; }
        public DbSet<VwSaldo> VwSaldos { get; set; }
        public DbSet<VwFluxoDeCaixa> VwFluxoDeCaixas { get; set; }
        public DbSet<VwQtdMa> VwQtdMas { get; set; }
        public DbSet<VwManutencao> VwManutencaos { get; set; }
        public DbSet<VwPragaOrDoenca> VwPragaOrDoencas { get; set; }
        public DbSet<VwControle> VwControles { get; set; }

        // ENDEREÇO
        public DbSet<Endereco.Endereco> Enderecos { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estado> Estados { get; set; }

        // PESSOA
        public DbSet<Pessoa.Pessoa> Pessoas { get; set; }
        public DbSet<PessoaFisica> PessoaFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoaJuridicas { get; set; }
        public DbSet<TelefonePessoa> TelefonePessoas { get; set; }

        // TELEFONE
        public DbSet<Telefone.Telefone> Telefones { get; set; }
        public DbSet<TipoTel> TipoTels { get; set; }
        public DbSet<DDD> DDDs { get; set; }

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

            modelBuilder.Entity<VwPessoa>()
                .Property(t => t.Endereco)
                .HasColumnName("Endereço");

            modelBuilder.Entity<VwPessoaFisica>()
                .Property(t => t.Endereco)
                .HasColumnName("Endereço");

            modelBuilder.Entity<VwPessoaFisica>()
                .Property(t => t.DataNascimento)
                .HasColumnName("Data de Nascimento");

            modelBuilder.Entity<VwPessoaJuridica>()
                .Property(t => t.Endereco)
                .HasColumnName("Endereço");

            modelBuilder.Entity<VwFuncionario>()
                .Property(t => t.Salario)
                .HasColumnName("Salário");

            modelBuilder.Entity<VwFuncionario>()
                .Property(t => t.DataNascimento)
                .HasColumnName("Data de Nascimento");

            modelBuilder.Entity<VwFuncionario>()
                .Property(t => t.Endereco)
                .HasColumnName("Endereço");

            modelBuilder.Entity<VwFornecedor>()
                .Property(t => t.NomeFantasia)
                .HasColumnName("Nome Fantasia");

            modelBuilder.Entity<VwFornecedor>()
                .Property(t => t.RazaoSocial)
                .HasColumnName("Razão Social");

            modelBuilder.Entity<VwFornecedor>()
                .Property(t => t.Endereco)
                .HasColumnName("Endereço");

            modelBuilder.Entity<VwItems>()
                .Property(t => t.UnidadeMedida)
                .HasColumnName("Unidade de Medida");

            modelBuilder.Entity<VwItems>()
                .Property(t => t.ValorTotal)
                .HasColumnName("Valor Total (p/Produto)");

            modelBuilder.Entity<VwItems>()
                .Property(t => t.ValorUnitario)
                .HasColumnName("Valor Unitário (R$)");

            modelBuilder.Entity<VwCompra>()
                .Property(t => t.ItensQtd)
                .HasColumnName("Itens - Quantidade Comprada");

            modelBuilder.Entity<VwCompra>()
                .Property(t => t.ValorTotal)
                .HasColumnName("Valor Total");

            modelBuilder.Entity<VwSaida>()
                .Property(t => t.Saida)
                .HasColumnName("Saída");

            modelBuilder.Entity<VwFluxoDeCaixa>()
                .Property(t => t.Saida)
                .HasColumnName("Saída");

            modelBuilder.Entity<VwFluxoDeCaixa>()
                .Property(t => t.Mes)
                .HasColumnName("MÊS");

            modelBuilder.Entity<VwFluxoDeCaixa>()
                .Property(t => t.Ano)
                .HasColumnName("ANO");

            modelBuilder.Entity<VwQtdMa>()
                .Property(t => t.Quantidade)
                .HasColumnName("Quantidade de Manutenções");

            modelBuilder.Entity<VwQtdMa>()
                .Property(t => t.CustoTotal)
                .HasColumnName("Custo Total");

            modelBuilder.Entity<VwManutencao>()
                .Property(t => t.Maquina)
                .HasColumnName("Máquina");

            modelBuilder.Entity<VwManutencao>()
                .Property(t => t.Tipo)
                .HasColumnName("Tipo de Máquina");

            modelBuilder.Entity<VwManutencao>()
                .Property(t => t.Manutencao)
                .HasColumnName("Manutenção");

            modelBuilder.Entity<VwManutencao>()
                .Property(t => t.Data)
                .HasColumnName("Data de Manutenção");

            modelBuilder.Entity<VwManutencao>()
                .Property(t => t.Valor)
                .HasColumnName("Valor da Manutenção");

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

            modelBuilder.Entity<VwControle>()
                .Property(t => t.Itens)
                .HasColumnName("Itens Usados - Quantidade");

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("tbUsuario")
                .Ignore(t => t.PhoneNumber)
                .Ignore(t => t.PhoneNumberConfirmed)
                .Ignore(t => t.LockoutEndDateUtc)
                .Ignore(t => t.LockoutEnabled)
                .Ignore(t => t.AccessFailedCount)
                .Ignore(t => t.TwoFactorEnabled);
        }

        public static BancoContext Create()
        {
            return new BancoContext();
        }
    }
}