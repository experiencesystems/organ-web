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
        public int IdComentario { get; set; }
        
        public int IdResposta { get; set; }

        public virtual Comentario Comentario { get; set; }
    }
}