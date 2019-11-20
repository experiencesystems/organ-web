using OrganWeb.Areas.Ecommerce.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Vendas
{
    [Table("tbAnunciante")]
    public class Anunciante
    {
        [Key, ForeignKey("Usuario")]
        public string IdUsuario { get; set; }

        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string NomeFazenda { get; set; }

        [StringLength(50)]
        public string NomeBanco { get; set; }

        [Required]
        public int NumEnd { get; set; }

        [StringLength(50)]
        public string CompEnd { get; set; }

        [Required]
        [ForeignKey("Endereco")]
        [RegularExpression(@"[0-9]{5}[\d]{3}", ErrorMessage = "Digite um CEP válido somente com números")]
        [StringLength(8, MinimumLength = 8)]
        public string CEP { get; set; }

        public virtual ApplicationUser Usuario { get; set; }
        public virtual Endereco.Endereco Endereco { get; set; }
    }
}