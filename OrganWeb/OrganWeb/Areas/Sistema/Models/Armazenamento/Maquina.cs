using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrganWeb.Areas.Sistema.Models.API;
using OrganWeb.Areas.Sistema.Models.zBanco;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbMaquina")]
    public class Maquina : OrganRepository<Maquina>
    {
        [Key]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Nome { get; set; }

        [Required]
        public int Tipo { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Montadora { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(100, MinimumLength = 3)]
        public string Desc { get; set; }
        
        public virtual Estoque Estoque { get; set; }

        [NotMapped]
        public UnidadeCadastro Unini { get; set; }
    }
}