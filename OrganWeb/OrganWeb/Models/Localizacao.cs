using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbLocalizacao")]
    public class Localizacao : Repository<Localizacao>
    {
        [Key]
        [Column(Order = 1)]
        public string CEP { get; set; }

        [Key]
        [Column(Order = 2)]
        public int Numero { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 10)]
        public string Endereco { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Complemento { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string UF { get; set; }

        //public virtual Fazenda Fazenda { get; set; }
    }
}