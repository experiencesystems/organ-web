using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Models.Banco;
using OrganWeb.Areas.Sistema.Models.API;

namespace OrganWeb.Areas.Sistema.Models.Ferramentas
{
    [Table("tbMaquina")]
    public class Maquina : Repository<Maquina>
    {
        [Key]
        [ForeignKey("Estoque")]
        public int IdEstoque { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Nome { get; set; }

        [Required]
        public int Tipo { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(300, MinimumLength = 10)]
        public string Desc { get; set; }
        
        [Display(Name = "Vida útil")]
        public int VidaUtil { get; set; }

        [Required]
        [Display(Name = "Valor inicial")]
        public double ValorInicial { get; set; }
        
        [Display(Name = "Depreciação por mês")]
        public double DeprMes { get; set; }
        
        [Display(Name = "Depreciação por ano")]
        public double DeprAno { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de cadastro")]
        public DateTime DataCadastro { get; set; }
        
        public virtual Estoque Estoque { get; set; }

        [NotMapped]
        public UnidadeCadastro Unini { get; set; }
    }
}