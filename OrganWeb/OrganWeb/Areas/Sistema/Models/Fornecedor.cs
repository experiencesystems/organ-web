using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Fornecedor : Repository<Fornecedor>
    {
        [Key]
        public int FornecedorID { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }

        //FK
        public int TelefoneID { get; set; }

        public string Site { get; set; }
        public string Email { get; set; }
        public int LocalizacaoID { get; set; }

        //TODO: Verificar se Status é FK
        public int Status { get; set; }

        //Compra - 1-n
        //Telefone - 1-n
    }
}