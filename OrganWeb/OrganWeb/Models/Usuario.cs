using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models
{
    [Table("Users")]
    public partial class Usuario
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public System.Guid ActivationCode { get; set; }
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