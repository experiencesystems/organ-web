using OrganWeb.Areas.Sistema.Models;
using OrganWeb.Areas.Sistema.Models.zBanco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Telefone
{
    [Table("tbTelefone")]
    public class Telefone : OrganRepository<Telefone>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Numero { get; set; }
        
        [Required]
        [ForeignKey("TipoTel")]
        public int IdTipo { get; set; }

        [Required]
        [ForeignKey("DDD")]
        public int IdDDD { get; set; }

        public virtual TipoTel TipoTel { get; set; }
        public virtual DDD DDD { get; set; }
    }
}