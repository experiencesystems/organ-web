using OrganWeb.Models.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models
{
    [Table("tbComentario")]
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        public int Like { get; set; }
        public int Deslike { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "Digite um comentário de até 100 caracteres!")]
        public string Valor { get; set; }

        [Required]
        [ForeignKey("Anuncio")]
        public int IdAnuncio { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public string IdUsuario { get; set; }

        public virtual Anuncio Anuncio { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
        public virtual List<Resposta> Respostas { get; set; }
    }
}