using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Models
{
    public class Usuario
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
    }
}