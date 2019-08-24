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
        public virtual DbSet<Localizacao> Localizacaos { get; set; }
        public virtual DbSet<Fazenda> Fazendas { get; set; }
        public virtual DbSet<Funcionario> Funcionarios { get; set; }
        public virtual DbSet<Cargo> Cargos { get; set; }
        //public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Tarefa> Tarefas { get; set; }
        public virtual DbSet<Equipe> Equipes { get; set; }
        public virtual DbSet<Telefone> Telefones { get; set; }
        public virtual DbSet<FuncionarioTelefone> FuncionarioTelefones { get; set; }
        public virtual DbSet<Monitoramento> Monitoramentos { get; set; }
        public virtual DbSet<MonitoramentoTarefa> MonitoramentoTarefas { get; set; }
        public virtual DbSet<EquipeFuncionario> EquipeFuncionarios { get; set; }
        public virtual DbSet<Estoque> Estoques { get; set; }
        public virtual DbSet<HistoricoEstoque> HistoricoEstoques { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<Fornecedor> Fornecedors { get; set; }
        public virtual DbSet<FornecedorTelefone> FornecedorTelefones { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Cultura> Culturas { get; set; }
        public virtual DbSet<Plantio> Plantios { get; set; }
        public virtual DbSet<Estadio> Estadios { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AreaPlantio> AreaPlantios { get; set; }
        public virtual DbSet<Maquina> Maquinas { get; set; }
        public virtual DbSet<Manutencao> Manutencaos { get; set; }
        public virtual DbSet<ManutencaoMaquina> ManutencaoMaquinas { get; set; }
        public virtual DbSet<Praga> Pragas { get; set; }
        public virtual DbSet<Doenca> Doencas { get; set; }
        public virtual DbSet<Controle> Controles { get; set; }
        public virtual DbSet<ControlePraga> ControlePragas { get; set; }
        public virtual DbSet<ControleDoenca> ControleDoencas { get; set; }
        public virtual DbSet<ControleMaquina> ControleMaquinas { get; set; }
        public virtual DbSet<ControleArea> ControleAreas { get; set; }
        public virtual DbSet<ControleItem> ControleItems { get; set; }
        public virtual DbSet<Pagamento> Pagamentos { get; set; }
        public virtual DbSet<Despesa> Despesas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ****** CHAVES CONCATENADAS ***** //

            modelBuilder.Entity<Localizacao>().HasKey(t => new { t.CEP, t.Numero });

            modelBuilder.Entity<TarefaFuncionario>().HasKey(t => new { t.IdTarefa, t.IdFunc });

            // ****** MAPEAMENTO NOMES ***** //

            modelBuilder.Entity<Cargo>()
                .Property(t => t.Nome)
                .HasColumnName("Cargo");

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.CliOrFunc)
                .HasColumnName("CLI/FUNC");

            modelBuilder.Entity<Funcionario>()
                .Property(t => t.MesAno)
                .HasColumnName("MES/ANO");

            modelBuilder.Entity<Categoria>()
                .Property(t => t.EventoItem)
                .HasColumnName("EVENTO/ITEM");

            modelBuilder.Entity<Evento>()
                .Property(t => t.DataHora)
                .HasColumnName("Data/Hora");

            modelBuilder.Entity<Semente>()
                .Property(t => t.IncSolar)
                .HasColumnName("Incidência Solar Ideal");

            modelBuilder.Entity<Semente>()
                .Property(t => t.IncVento)
                .HasColumnName("Incidência Vento Ideal");

            modelBuilder.Entity<Plantio>()
                .Property(t => t.QntHectare)
                .HasColumnName("KG/HA de Semente");

            // ****** RELAÇÕES ***** //

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