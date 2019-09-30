using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Endereco
{
    [Table("tbCidade")]
    public class Cidade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Nome { get; set; }

        [Required]
        [ForeignKey("Estado")]
        public Int16 IdEstado { get; set; }

        public virtual Estado Estado { get; set; }
    }
}