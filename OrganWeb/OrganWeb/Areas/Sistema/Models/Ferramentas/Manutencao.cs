using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.zRepositories;
using OrganWeb.Models.Banco;

namespace OrganWeb.Areas.Sistema.Models.Ferramentas
{
    [Table("tbManutencao")]
    public class Manutencao : Repository<Manutencao>
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(30, MinimumLength = 3)]
        public string Nome { get; set; }

        [StringLength(300, MinimumLength = 10)]
        public string Detalhes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de manutenção")]
        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Valor pago")]
        public double ValorPago { get; set; }
    }
}