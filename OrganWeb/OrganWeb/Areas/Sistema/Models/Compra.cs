using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("Compra")]
    public class Compra : Repository<Compra>
    {
        [Key]
        public int CompraID { get; set; }
        [Required]
        [ForeignKey("Produto")]
        public int ProdutoID { get; set; }
        [Required]
        [ForeignKey("Fornecedor")]
        public int FornecedorID { get; set; }
        [Display(Name = "Descrição")]
        [MaxLength(500, ErrorMessage = "Esse campo deve ter 500 caracteres ou menos"), MinLength(10, ErrorMessage = "Esse campo deve ter no mínimo 10 caracteres")]
        public string Descricao { get; set; }
        [Required]
        [ForeignKey("Pagamento")]
        public int PagamentoID { get; set; }

        //public Movimentacao Movimentacao { get; set; }
        //public List<Produto> Produtos { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Pagamento Pagamento { get; set; }
    }
}