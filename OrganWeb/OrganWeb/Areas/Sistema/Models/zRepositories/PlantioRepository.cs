using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models.Banco;

namespace OrganWeb.Areas.Sistema.Models
{
    public class PlantioRepository : Repository<Plantio>
    { 
         public List<Plantio> GetPlantios()
        {
            var select = _context.Plantios
                        .AsEnumerable()
                        .Select(p => new Plantio
                        {
                            Porcentagem = ProgressoPlantio(p),
                            Id = p.Id,
                            Nome = p.Nome,
                            Sistema = p.Sistema,
                            TipoPlantio = p.TipoPlantio,
                            DataInicio = p.DataInicio,
                            DataColheita = p.DataColheita
                        })
                        .ToList();
            return select;
        }

        public List<Plantio> GetPlantiosIncompletos()
        {
            var plantios = GetPlantios();
            var colheitas = new Colheita().GetAll();
            plantios.RemoveAll(p => colheitas.Any(c => c.IdPlantio == p.Id));
            return plantios;
        }

        public double ProgressoPlantio(Plantio plantio)
        {
            DateTime hoje = DateTime.Today;
            try
            {
                //agora - começo
                TimeSpan agoracomeco = (hoje.Subtract(plantio.DataInicio));
                int diasAgoracomeco = agoracomeco.Days;

                //fim - começo
                TimeSpan fimcomeco = (plantio.DataColheita.Subtract(plantio.DataInicio));
                int diasFimcomeco = fimcomeco.Days;

                int progresso = ((100 * diasAgoracomeco) / diasFimcomeco);

                if (progresso > 100)
                {
                    return 100;
                }
                else
                {
                    return progresso;
                }
            }
            catch
            {
                return 100;
            }
        }
    }
}