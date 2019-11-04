using OrganWeb.Models.Endereco;
using OrganWeb.Models.Financeiro;
using OrganWeb.Models.Telefone;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace OrganWeb.Models.Usuario
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar deste navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O/A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nome completo")]
        [StringLength(100, MinimumLength = 5)]
        public string Nome { get; set; }

        [Required]
        public int DDD { get; set; }

        [Required]
        public int Telefone { get; set; }

        [Required]
        [Display(Name = "Tipo do telefone (Casa, Trabalho, etc.)")]
        [StringLength(20, MinimumLength = 2)]
        public string TipoTelefone { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)]
        [RegularExpression(@"[0-9]{5}[\d]{3}", ErrorMessage = "Digite um CEP válido somente com números")]
        public string CEP { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Rua { get; set; }

        [Required]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string Complemento { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Cidade { get; set; }

        [Required]
        public int Estado { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string NomeTitular { get; set; }

        [Required]
        [Display(Name = "Número do cartão")]
        public Int64 NumCartao { get; set; }

        [Required]
        public int Banco { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Validade { get; set; }

        [Required]
        [Range(1, 9999)]
        public int CVV { get; set; }

        public IEnumerable<SelectListItem> Bancos { get; set; }
        public IEnumerable<Estado> Estados { get; set; }
        public IEnumerable<DDD> DDDs { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O/A {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não coincidem.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class LoginRegisterViewModel
    {
        public RegisterViewModel Registro { get; set; }
        public LoginViewModel Login { get; set; }
    }
}
