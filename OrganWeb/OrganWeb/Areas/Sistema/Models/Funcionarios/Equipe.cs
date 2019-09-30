using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Models.Banco;

namespace OrganWeb.Areas.Sistema.Models.Funcionarios
{
    [Table("tbEquipe")]
    public class Equipe : Repository<Equipe>
    {
        [Key]
        public int Id { get; set; }

        [StringLength(300, MinimumLength = 5)]
        public string Desc { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Nome { get; set; }
    }
}