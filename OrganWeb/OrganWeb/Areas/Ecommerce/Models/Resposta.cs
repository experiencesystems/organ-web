using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models
{
    [Table("tbResposta")]
    public class Resposta
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Comentario")]
        public int IdComentario { get; set; }

        [Key, Column(Order = 2)] //todo: autoincrement
        public int IdResposta { get; set; }

        public virtual Comentario Comentario { get; set; }
    }
}