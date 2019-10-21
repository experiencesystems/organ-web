using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
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
    [Table("tbVenda")]
    public class Venda : Repository<Venda>
    {
        [Key]
        public int Id { get; set; }
        
        [Range(0.01, 999.99, ErrorMessage = "Esse campo deve conter valores até 999,99")]
        public double? Desconto { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [ForeignKey("Pagamento")]
        public int IdPagamento { get; set; }
        //TODO: VWCLIENTE
        //TODO: tirar desconto prod de tb junção
        public virtual Cliente Cliente { get; set; }
        public virtual Pagamento Pagamento { get; set; }
        [NotMapped]
        public ItensVendidos ItensVendidos { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }
        public IEnumerable<VwItems> Items { get; set; }
    }
}