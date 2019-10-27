using OrganWeb.Areas.Sistema.Models.Administrativo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewModels
{
    public class CreatePragaOrDoencaViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        public bool PD { get; set; }

        public int[] IdArea { get; set; }

        public IEnumerable<Area> Areas { get; set; }
    }
}