using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Telefone
{
    [Table("tbTipoTel")]
    public class TipoTel : OrganRepository<TipoTel>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tipo: casa, fixo, celular, etc")]
        [StringLength(15, MinimumLength = 1)]
        public string Tipo { get; set; }
    }
}