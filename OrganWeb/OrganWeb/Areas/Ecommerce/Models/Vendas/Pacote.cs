using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.Vendas
{
    [Table("tbPacote")]
    public class Pacote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Entrega")]
        public int IdEntrega { get; set; }

        [Required]
        public double Peso { get; set; }

        [Required]
        [Display(Name = "Altura")]
        public double Alt { get; set; }

        [Required]
        public double Largura { get; set; }

        [Required]
        [Display(Name = "Diâmetro")]
        public double Diametro { get; set; }

        [Required]
        [Display(Name = "Comprimento")]
        public double Comp { get; set; }

        [Required]
        [Display(Name = "É Frágil?")]
        public bool EFragil { get; set; }

        public virtual Entrega Entrega { get; set; }
    }
}