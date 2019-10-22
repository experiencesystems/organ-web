using OrganWeb.Models.Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Praga_e_Doenca
{
    [Table("tbPragaOrDoenca")]
    public class PragaOrDoenca : Repository<PragaOrDoenca>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        //praga ou doenca 
        public bool PD { get; set; }
    }
}