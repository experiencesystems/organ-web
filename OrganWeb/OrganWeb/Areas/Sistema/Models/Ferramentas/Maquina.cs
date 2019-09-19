using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Estoque;

namespace OrganWeb.Areas.Sistema.Models.Ferramentas
{
    [Table("tbMaquina")]
    public class Maquina : Repository<Maquina>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 3)]
        public string Nome { get; set; }

        [StringLength(300, MinimumLength = 10)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data de cadastro")]
        public DateTime DataCadastro { get; set; }

        [Required]
        [Range(0.01, 99999.99)]
        [Display(Name = "Valor na data do cadastro")]
        public double ValorCadastro { get; set; }

        [Required]
        [Range(0.01, 99.99)]
        [Display(Name = "Vida útil")]
        public double VidaUtil { get; set; }

        [Required]
        [Range(0.01, 99.99)]
        [Display(Name = "Depreciação por ano")]
        public double DepreciacaoAno { get; set; }

        [Required]
        [Range(0.01, 99.99)]
        [Display(Name = "Depreciação por mês")]
        public double DepreciacaoMes { get; set; }

        //TODO: Verificar montadora
        [Required]
        [ForeignKey("Fornecedor")]
        public int Montadora { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
    }
}