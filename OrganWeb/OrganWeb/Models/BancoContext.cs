using System;
using System.Collections.Generic;
using System.Data.Entity;
using OrganWeb.Areas.Sistema.Models.Semente;
using System.Linq;
using System.Web;
using System.Configuration;

namespace OrganWeb.Models
{
    public class BancoContext : DbContext
    {
        public BancoContext() : base("name=BancoContext") { }

        public virtual DbSet<Semente> Sementes { get; set; }
    }
}