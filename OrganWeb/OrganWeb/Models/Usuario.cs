using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models
{
    [Table("tbUsuario")]
    public partial class Usuario
    {
        [Key]
        public int ID { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public bool Confirmacao { get; set; }
        public bool Assinatura { get; set; }
        public bool CliOrFunc { get; set; }
        public DateTime DataCadastro { get; set; }
        public System.Guid CodigoAtivacao { get; set; }
    }

    /*public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public int AtivacaoUsuario { get; set; }
        public string Email { get; set; }

        //Talvez seja FK
        public int Assinatura { get; set; }

        public int TipoUsuario { get; set; }
    }*/
}