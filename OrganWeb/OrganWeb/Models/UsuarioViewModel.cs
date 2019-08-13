using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Models
{
    public class RegistroViewModel
    {
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email é requerido")]
        [DataType(DataType.EmailAddress)]
        //TODO: verificação de caractere de email
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A senha não foi preenchida")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; }

        [Display(Name = "Confirmar senha")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A confirmação da senha não foi preenchida")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "Senhas estão diferentes")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email não pode ser vazio")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Senha não pode ser vazio")]
        public string Senha { get; set; }

        [Display(Name = "Manter conectado")]
        public bool RememberMe { get; set; }
    }

    public class LoginRegistroViewModel
    {
        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email é requerido")]
        [DataType(DataType.EmailAddress)]
        //TODO: verificação de caractere de email
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A senha não foi preenchida")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; }

        [Display(Name = "Confirmar senha")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A confirmação da senha não foi preenchida")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "Senhas estão diferentes")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Manter conectado")]
        public bool RememberMe { get; set; }
    }
}