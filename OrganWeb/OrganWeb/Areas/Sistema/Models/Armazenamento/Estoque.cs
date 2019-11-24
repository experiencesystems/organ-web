using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Safras;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.zRepositories;
using OrganWeb.Areas.Sistema.Models.API;
using OrganWeb.Areas.Sistema.Models.zBanco;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbEstoque")]
    public class Estoque : OrganRepository<Estoque>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public double Qtd { get; set; }

        [Required]
        [StringLength(6)]
        [ForeignKey("UnidadeMedida")]
        [Display(Name = "Unidade de medida")]
        public string UM { get; set; }
        
        [ForeignKey("Fornecedor")]
        public int? IdFornecedor { get; set; }

        public virtual UnidadeCadastro UnidadeMedida { get; set; }

        [ForeignKey("Fornecedor")]
        public virtual Fornecedor Fornecedor { get; set; }

        [NotMapped]
        public List<UnidadeCadastro> Unidades { get; set; }

        [NotMapped]
        [ForeignKey("Fornecedor")]
        public List<Fornecedor> Fornecedores { get; set; }
    }

}