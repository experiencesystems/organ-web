﻿using System.Data.Entity;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.Controles;
using OrganWeb.Areas.Sistema.Models.Praga_e_Doenca;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.Telefone;

namespace OrganWeb.Areas.Sistema.Models.zBanco
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class OrganContext : DbContext
    {
        public OrganContext() : base("name=OrganContext") { }

        // SISTEMA
        // ADMINISTRATIVO
        public DbSet<Area> Areas { get; set; }
        public DbSet<Solo> Solos { get; set; }

        // ARMAZENAMENTO
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

        // FUNCIONÁRIO
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
        public DbSet<VwTelefone> VwTelefones { get; set; }
        public DbSet<VwFuncionario> VwFuncionarios { get; set; }
        public DbSet<VwItems> VwItems { get; set; }
        public DbSet<VwPragaOrDoenca> VwPragaOrDoencas { get; set; }
        public DbSet<VwHistorico> VwHistoricos { get; set; }
        public DbSet<VwControle> VwControles { get; set; }
        public DbSet<VwPlantio> VwPlantios { get; set; }
        public DbSet<VwColheita> VwColheitas { get; set; }

        // TELEFONE
        public DbSet<Telefone.Telefone> Telefones { get; set; }
        public DbSet<TipoTel> TipoTels { get; set; }
        public DbSet<DDD> DDDs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MAPEAMENTO DOS NOMES
            /*Dataa date,
            QntPerdas double,
            QntTot double,
            UnM varchar(6),
            Nome1 varchar(30),
            `Desc1` varchar(100),
            IdPlant int,
            Stats int,
            IdCol int
            modelBuilder.Entity<Colheita>()
                .MapToStoredProcedures(s => s
                .Insert(u => u.HasName("spInsertColheita")
                    .Parameter(b => b.Data, "Dataa")
                    .Parameter(b => b.QtdPerdas, "QntPerdas")
                    .Parameter(b => b.QtdTotal, "QntTot")
                    .Parameter(b => b.Produto.Estoque.UM, "UnM")
                    .Parameter(b => b.Produto.Nome, "Nome1")
                    .Parameter(b => b.Produto.Desc, "Desc1")
                    .Parameter(b => b.IdPlantio, "IdPlant")
                    .Parameter(b => b.Status, "Stats")
                    .Parameter(b => b.Id, "IdCol")
                ));*/

            modelBuilder.Entity<DDD>()
                .Property(t => t.Valor)
                .HasColumnName("DDD");

            modelBuilder.Entity<PragaOrDoenca>()
                .Property(t => t.PD)
                .HasColumnName("P/D");

            modelBuilder.Entity<VwFornecedor>()
                .Property(t => t.RazaoSocial)
                .HasColumnName("Razão Social");

            modelBuilder.Entity<VwFuncionario>()
                .Property(t => t.Funcao)
                .HasColumnName("Função");

            modelBuilder.Entity<VwFuncionario>()
                .Property(t => t.Situacao)
                .HasColumnName("Situação");

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

            modelBuilder.Entity<VwItems>()
                .Property(t => t.UnidadeMedida)
                .HasColumnName("Unidade de Medida");

            modelBuilder.Entity<VwItems>()
                .Property(t => t.Descricao)
                .HasColumnName("Descrição");

            modelBuilder.Entity<VwHistorico>()
                .Property(t => t.IdItem)
                .HasColumnName("Id do Item");

            modelBuilder.Entity<VwHistorico>()
                .Property(t => t.Nome)
                .HasColumnName("Nome do Item");

            modelBuilder.Entity<VwHistorico>()
                .Property(t => t.DataAlteracao)
                .HasColumnName("Data de Alteração");

            modelBuilder.Entity<VwHistorico>()
                .Property(t => t.QtdAntiga)
                .HasColumnName("Quantidade Antiga");

            modelBuilder.Entity<VwHistorico>()
                .Property(t => t.QtdAtual)
                .HasColumnName("Quantidade Atual");

            modelBuilder.Entity<VwHistorico>()
                .Property(t => t.Descricao)
                .HasColumnName("Descrição de Alteração");

            modelBuilder.Entity<VwPlantio>()
                .Property(t => t.Inicio)
                .HasColumnName("Data de Início");

            modelBuilder.Entity<VwPlantio>()
                .Property(t => t.Colheita)
                .HasColumnName("Data Prevista pra Colheita");

            modelBuilder.Entity<VwPlantio>()
                .Property(t => t.Areas)
                .HasColumnName("Áreas");

            modelBuilder.Entity<VwPlantio>()
                .Property(t => t.Itens)
                .HasColumnName("Itens Usados");

            modelBuilder.Entity<VwPlantio>()
                .Property(t => t.Funcionarios)
                .HasColumnName("Funcionários Participantes");

            modelBuilder.Entity<VwColheita>()
                .Property(t => t.DataColheita)
                .HasColumnName("Data da Colheita");

            modelBuilder.Entity<VwColheita>()
                .Property(t => t.Situacao)
                .HasColumnName("Situação da Colheita");

            modelBuilder.Entity<VwColheita>()
                .Property(t => t.QtdColhida)
                .HasColumnName("Quantidade Colhida");

            modelBuilder.Entity<VwColheita>()
                .Property(t => t.QtdPerdida)
                .HasColumnName("Quantidade Perdida");

            modelBuilder.Entity<VwColheita>()
                .Property(t => t.QtdTotal)
                .HasColumnName("Quantidade Total");
        }

        public static OrganContext Create()
        {
            return new OrganContext();
        }
    }
}