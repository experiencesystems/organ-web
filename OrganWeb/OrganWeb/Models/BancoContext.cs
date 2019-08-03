using System;
using System.Collections.Generic;
using System.Data.Entity;
using OrganWeb.Areas.Sistema.Models._Praga;
using OrganWeb.Areas.Sistema.Models._Semente;
using OrganWeb.Areas.Sistema.Models;
using System.Linq;
using System.Web;
using System.Configuration;
using OrganWeb.Areas.Sistema.Models._Doenca;

namespace OrganWeb.Models
{
    public class BancoContext : DbContext
    {
        public BancoContext() : base("name=BancoContext") {
        }

        public virtual DbSet<Semente> Sementes { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Praga> Pragas { get; set; }
        public virtual DbSet<Doenca> Doencas { get; set; }
    }
}