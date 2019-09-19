using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.zRepositories;

namespace OrganWeb.Areas.Sistema.Models.Ferramentas
{
    [Table("tbManutencao")]
    public class Manutencao : Repository<Manutencao>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de manutenção")]
        public DateTime DataManuntencao { get; set; }

        [StringLength(300, MinimumLength = 10)]
        public string Detalhes { get; set; }
    }

    [Table("tbManutencaoMaquina")]
    public class ManutencaoMaquina : MaquinaManutencaoRepository
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Maquina")]
        public int IdMaquina { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Manutencao")]
        public int IdManuntencao { get; set; }

        public virtual Maquina Maquina { get; set; }
        public virtual Manutencao Manutencao { get; set; }
    }
}