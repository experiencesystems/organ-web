using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    [Table("tbControle")]
    public class Controle : Repository<Controle>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 3)]
        public string Tipo { get; set; }

        [StringLength(300, MinimumLength = 10)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Range(0.01, 100.00)]
        [Display(Name = "Eficiência")]
        public double Eficiencia { get; set; }

        [Required]
        [Display(Name = "Número de liberações")]
        public int NumeroLiberacoes { get; set; }

        [Required]
        [ForeignKey("Estadio")]
        public int IdEstadio { get; set; }

        public virtual Estadio Estadio { get; set; }
    }

    [Table("tbControlePraga")]
    public class ControlePraga
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Praga")]
        public int IdPraga { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Controle")]
        public int IdControle { get; set; }

        public virtual Praga Praga { get; set; }
        public virtual Controle Controle { get; set; }
    }

    [Table("tbControleDoenca")]
    public class ControleDoenca
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Doenca")]
        public int IdDoenca { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Controle")]
        public int IdControle { get; set; }

        public virtual Doenca Doenca { get; set; }
        public virtual Controle Controle { get; set; }
    }

    [Table("tbControleMaquina")]
    public class ControleMaquina
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Maquina")]
        public int IdMaquina { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Controle")]
        public int IdControle { get; set; }

        public virtual Maquina Maquina { get; set; }
        public virtual Controle Controle { get; set; }
    }

    [Table("tbControleArea")]
    public class ControleArea
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Area")]
        public int IdArea { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Controle")]
        public int IdControle { get; set; }

        public virtual Area Area { get; set; }
        public virtual Controle Controle { get; set; }
    }

    [Table("tbControleItem")]
    public class ControleItem
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Item")]
        public int IdItem { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Controle")]
        public int IdControle { get; set; }

        public virtual Item Item { get; set; }
        public virtual Controle Controle { get; set; }
    }
}