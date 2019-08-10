using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Controle : Repository<Controle>
    {
        [Key]
        public int ControleID { get; set; }
        public string Status { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }

        [ForeignKey("Estadio")]
        public int EstadioID { get; set; }
        public decimal Eficiencia { get; set; }
        public int NumLiberacoes { get; set; }

        public Estadio Estadio { get; set; }
        public List<Praga> Pragas { get; set; }
        //public List<Maquina> Maquinas { get; set; }
        //public List<Area> Areas { get; set; }
        //public List<Item> Itens { get; set; }

        //Estádio - 1-n
        //Praga - n-n
        //Maquina - n-n
        //Área - n-n
        //Item - n-n
    }
}