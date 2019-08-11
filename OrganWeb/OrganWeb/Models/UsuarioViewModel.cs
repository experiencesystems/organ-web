using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Models
{
    public class RegistroViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome é requerido")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Sobrenome é requerido")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email é requerido")]
        [DataType(DataType.EmailAddress)]
        //TODO: verificação de caractere de email
        public string EmailID { get; set; }

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Senha")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A senha não foi preenchida")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Password { get; set; }

        [Display(Name = "Confirmar senha")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A confirmação da senha não foi preenchida")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senhas estão diferentes")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        public string EmailID { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}