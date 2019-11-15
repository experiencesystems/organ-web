using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Models.Praga_e_Doenca
{
    [Table("tbPragaOrDoenca")]
    public class PragaOrDoenca : OrganRepository<PragaOrDoenca>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Nome { get; set; }
        
        //praga = true ou doenca = false
        public bool PD { get; set; }

        [Required]
        [NotMapped]
        public string PragDoe { get; set; }

        [NotMapped]
        public IEnumerable<Area> Areas { get; set; }
    }
}