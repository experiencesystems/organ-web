using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Endereco
{
    [Table("tbEstado")]
    public class Estado : EcommerceRepository<Estado>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Nome { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string UF { get; set; }
    }
}