using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using OrganWeb.Models;
using OrganWeb.Models.Banco;
using OrganWeb.Models.Financeiro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Financas
{
    [Table("tbCompra")]
    public class Compra : Repository<Compra>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Required]
        [Range(0.00, 999.99)]
        public double Desconto { get; set; }

        [Required]
        [ForeignKey("Fornecedor")]
        [Display(Name = "Fornecedor")]
        public int IdForn { get; set; }

        [Required]
        [ForeignKey("Pagamento")]
        public int IdPagamento { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Pagamento Pagamento { get; set; }

        public IEnumerable<VwFornecedor> Fornecedores { get; set; }
        public IEnumerable<VwItems> Items { get; set; }

        [NotMapped]
        public ItensComprados ItensComprados { get; set; }
    }
}