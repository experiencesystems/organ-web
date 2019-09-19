using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Estoque
{
    [Table("tbFornecedor")]
    public class Fornecedor : Repository<Fornecedor>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 3)]
        [Display(Name = "Fornecedor")]
        public string Nome { get; set; }

        [Required]
        //TODO: Verificação CNPJ
        public string CNPJ { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string RazaoSocial { get; set; }

        [StringLength(100, MinimumLength = 10)]
        public string Site { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        [ForeignKey("Localizacao")]
        [Column(Order = 1)]
        public string CEP { get; set; }

        [Required]
        [ForeignKey("Localizacao")]
        [Column(Order = 2)]
        public int Numero { get; set; }

        public virtual Localizacao Localizacao { get; set; }
    }

    [Table("tbFornecedorTelefone")]
    public class FornecedorTelefone
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Telefone")]
        public int IdTel { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Fornecedor")]
        public int IdForn { get; set; }

        public virtual Telefone Telefone { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}