using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models
{
    public class Maquina : Repository<Maquina>
    {
        [Key]
        public int IDMaquina { get; set; }

        public string Nome { get; set; }

        public string Montadora { get; set; }

        public string Descricao { get; set; }

        public double  ValorInicial { get; set; }

        public double VidaUtil { get; set; }

        public double ValorAno { get; set; }

        public double ValorMes { get; set; }

        //TODO: Verificar FKs Máquina
    }
}