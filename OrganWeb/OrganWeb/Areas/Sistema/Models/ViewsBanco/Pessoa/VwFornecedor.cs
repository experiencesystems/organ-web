using OrganWeb.Areas.Sistema.Models.zBanco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa
{
    [Table("vwFornecedor")]
    public class VwFornecedor : OrganRepository<VwFornecedor>
    {
        [Key]
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Telefones { get; set; }
    }
}