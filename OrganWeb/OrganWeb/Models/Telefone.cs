using OrganWeb.Areas.Sistema.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models
{
    [Table("tbTelefone")]
    public class Telefone : Repository<Telefone>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2)]
        [MinLength(2)]
        public int DDD { get; set; }

        [Required]
        [MaxLength(9)]
        public Int64 Numero { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string Tipo { get; set; }
    }
}