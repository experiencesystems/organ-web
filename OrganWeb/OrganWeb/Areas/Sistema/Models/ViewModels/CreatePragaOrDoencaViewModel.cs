using System;
using System.Collections.Generic;
using OrganWeb.Areas.Sistema.Models.Controles;
using OrganWeb.Areas.Sistema.Models.Praga_e_Doenca;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class CreatePragaOrDoencaViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Praga ou Doença?")]
        public bool PD { get; set; }

        //eu fiz esse mas so me toquei dps de que nao precisava risos


        public IEnumerable<Controle> Controles { get; set; }
        public IEnumerable<ControlePD> ControlePDs { get; set; }
        public IEnumerable<PragaOrDoenca> PragaOrDoencas { get; set; }
    }
}