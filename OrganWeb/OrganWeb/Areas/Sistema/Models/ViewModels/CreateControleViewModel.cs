using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Areas.Sistema.Models.Praga_e_Doenca;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class CreateControleViewModel
    {
        [Display(Name = "Descrição")]
        [StringLength(300, MinimumLength = 10)]
        public string Desc { get; set; }
        
        public bool Status { get; set; }

        [Required]
        [Display(Name = "Eficiência")]
        [Range(0, 999.99, ErrorMessage = "Esse campo deve conter valores até 999,99")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "Digite um valor válido com até duas casas decimais, como 99,99")]
        public double Efic { get; set; }

        [Required]
        [Display(Name = "Número de liberações")]
        public int NumLiberacoes { get; set; }
        
        [Required]
        [Display(Name = "Itens utilizados")]
        public int[] IdEstoque { get; set; }

        [Required]
        [Display(Name = "Quantidade usada")]
        public double[] QtdUsada { get; set; }
        
        [Required]
        [Display(Name = "Funcionários participantes")]
        public int[] IdFunc { get; set; }
        
        [Required]
        [Display(Name = "Pragas ou doenças controladas")]
        public int[] IdPD { get; set; }
        
        public IEnumerable<VwItems> VwItems { get; set; }
        public IEnumerable<Funcionario> Funcionarios { get; set; }
        public IEnumerable<PragaOrDoenca> PragaOrDoencas { get; set; }
    }
}