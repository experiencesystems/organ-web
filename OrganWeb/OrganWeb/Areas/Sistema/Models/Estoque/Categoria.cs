using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Estoque
{
    [Table("tbCategoria")]
    public class Categoria : Repository<Categoria>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public string Nome { get; set; }
    }
}