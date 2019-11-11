using OrganWeb.Areas.Sistema.Models.zBanco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganWeb.Areas.Sistema.Models.Funcionarios
{
    [Table("tbFuncionario")]
    public class Funcionario : OrganRepository<Funcionario>
    {//TODO: refazer controller funcionário
        [Key]
        public int Id { get; set; }
        public bool Status { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, MinimumLength = 2)]
        public string Email { get; set; }

        [Required]
        [ForeignKey("Cargo")]
        public int IdCargo { get; set; }
        
        public virtual Cargo Cargo { get; set; }
    }
}