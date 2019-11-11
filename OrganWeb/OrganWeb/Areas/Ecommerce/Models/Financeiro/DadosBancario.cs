using OrganWeb.Areas.Ecommerce.Models.Usuarios;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Financeiro
{
    [Table("tbDadosBancarios")]
    public class DadosBancario : EcommerceRepository<DadosBancario>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string NomeTitular { get; set; }

        [Required]
        [Range(1, 9999)]
        public int CVV { get; set; }

        [Required]
        public int Banco { get; set; }

        [Required]
        [Display(Name = "Número do cartão")]
        public long NumCartao { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Validade { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public string IdUsuario { get; set; }

        public virtual ApplicationUser Usuario { get; set; }
    }
}