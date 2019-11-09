using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrganWeb.Models;
using OrganWeb.Models.Banco;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbCategoria")]
    public class Categoria : Repository<Categoria>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        [StringLength(15, MinimumLength = 1)]
        public string Nome { get; set; }
    }
}