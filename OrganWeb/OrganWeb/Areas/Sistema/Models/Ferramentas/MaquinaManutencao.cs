using OrganWeb.Areas.Sistema.Models.zRepositories;
using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Ferramentas
{
    [Table("tbMaquinaManutencao")]
    public class MaquinaManutencao : MaquinaManutencaoRepository
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Maquina")]
        public int IdMaquina { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Manutencao")]
        public int IdManutencao { get; set; }

        public virtual Maquina Maquina { get; set; }
        public virtual Manutencao Manutencao { get; set; }

       
        public IEnumerable<Maquina> Maquinas { get; set; }
    }
}