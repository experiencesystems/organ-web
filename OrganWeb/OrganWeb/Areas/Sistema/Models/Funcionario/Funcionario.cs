using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrganWeb.Models.Banco;
using OrganWeb.Models.Pessoa;

namespace OrganWeb.Areas.Sistema.Models.Funcionarios
{
    [Table("tbFuncionario")]
    public class Funcionario : Repository<Funcionario>
    {
        [Key]
        public int Id { get; set; }
        public bool Status { get; set; }

        [Required]
        public double Salario { get; set; }

        [Required]
        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }

        [Required]
        [ForeignKey("Cargo")]
        public int IdCargo { get; set; }

        //https://stackoverflow.com/questions/13208349/how-to-insert-blob-datatype
        public byte[] Foto { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual Cargo Cargo { get; set; }
    }
}