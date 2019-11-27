using OrganWeb.Areas.Sistema.Models.zBanco;
using OrganWeb.Areas.Sistema.Models.zRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque
{
    [Table("vwControle")]
    public class VwControle : ControleRepository
    {
        [Key]
        public int Id { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Eficiência")]
        public double Eficiencia { get; set; }
        [Display(Name = "Liberações")]
        public int Liberacoes { get; set; }
        public int IdPD { get; set; }
        public string Nome { get; set; }
        public string Itens { get; set; }
    }
}