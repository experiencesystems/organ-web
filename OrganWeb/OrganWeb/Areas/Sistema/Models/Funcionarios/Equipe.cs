using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Funcionarios
{
    [Table("tbEquipe")]
    public class Equipe : Repository<Equipe>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }
    }

    [Table("tbEquipeFuncionario")]
    public class EquipeFuncionario
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Equipe")]
        public int IdEquipe { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Funcionario")]
        public int IdFunc { get; set; }

        [Required]
        public bool LiderOrNao { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Equipe Equipe { get; set; }
    }
}