using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models.Banco;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.zRepositories;
using OrganWeb.Areas.Sistema.Models.API;

namespace OrganWeb.Areas.Sistema.Models.Armazenamento
{
    [Table("tbEstoque")]
    public class Estoque : Repository<Estoque>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public double Qtd { get; set; }

        [Required]
        [Display(Name = "Unidade de medida")]
        public string UM { get; set; }

        [Required]
        [Display(Name = "Valor unitário")]
        public double ValorUnit { get; set; }

        [NotMapped]
        public List<UnidadeCadastro> Unidades { get; set; }
    }

}