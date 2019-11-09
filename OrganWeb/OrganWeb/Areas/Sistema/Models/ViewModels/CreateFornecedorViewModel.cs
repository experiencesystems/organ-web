using System;
using System.Collections.Generic;
using System.Linq;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using OrganWeb.Areas.Sistema.Models.Telefone;
using OrganWeb.Areas.Ecommerce.Models.Endereco;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class CreateFornecedorViewModel
    {
        [Required]
        [Display(Name = "Nome Fantasia")]
        [StringLength(100, MinimumLength = 5)]
        public string NomeFantasia { get; set; }
        
        [Required]
        [Display(Name = "Razão Social")]
        [StringLength(100, MinimumLength = 10)]
        public string RazaoSocial { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{2}\.?[0-9]{3}\.?[0-9]{3}\/?[0-9]{4}\-?[0-9]{2}", ErrorMessage = "Digite um CPNJ válido somente com números")]
        public Int64 CNPJ { get; set; }

        [Required]
        [Display(Name = "Inscrição Estadual")]
        public Int64 IE { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        public int DDD { get; set; }

        [Required]
        public int Telefone { get; set; }

        [Required]
        [Display(Name = "Tipo do telefone (Principal, WhatsApp, etc.)")]
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
        
        public IEnumerable<Estado> Estados { get; set; }
       
        public IEnumerable<DDD> DDDs { get; set; }
        
        public IEnumerable<Fornecedor> Fornecedor { get; set; }
    }
}