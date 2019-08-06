using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Compra : Repository<Compra>
    {
        [Key]
        public int CompraID { get; set; }
        public int ProdutoID { get; set; }
        public int FornecedorID { get; set; }
        public string Descricao { get; set; }
        public int PagamentoID { get; set; }

        //Produto - n
        //Fornecedor - 1
        //Pagamento - 1
        //Movimentação - 1
    }
}