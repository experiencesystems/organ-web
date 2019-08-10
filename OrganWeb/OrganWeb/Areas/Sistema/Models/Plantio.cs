using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Plantio : Repository<Plantio>
    {
        [Key]
        public int PlantioID { get; set; }
        public string Nome { get; set; }

        //TODO: Tirar comentário quando inserir classe Cultura
        //[ForeignKey("Cultura")]
        public int CulturaID { get; set; }
        public string Tipo { get; set; }
        public double Densidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataColheita { get; set; }
        
        [ForeignKey("Semente")]
        public int SementeID { get; set; }
        public int QuantidadeSemente { get; set; }
        public string Epoca { get; set; }
        public double QntHectare { get; set; }

        //public Cultura Cultura { get; set; }
        public Semente Semente { get; set; }
        //public List<Area> Areas { get; set; }
    }
}