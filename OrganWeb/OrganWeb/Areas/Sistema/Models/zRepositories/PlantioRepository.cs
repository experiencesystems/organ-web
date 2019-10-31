using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models.Banco;
using System.Threading.Tasks;

namespace OrganWeb.Areas.Sistema.Models
{
    public class PlantioRepository : Repository<Plantio>
    {
        public async Task<List<Plantio>> GetPlantios()
        {
            var plantios = await GetAll();
            return plantios.Select(p => new Plantio
            {
                Porcentagem = ProgressoPlantio(p),
                Id = p.Id,
                Nome = p.Nome,
                Sistema = p.Sistema,
                TipoPlantio = p.TipoPlantio,
                DataInicio = p.DataInicio,
                DataColheita = p.DataColheita
            }).ToList();
        }

        public async Task<List<Plantio>> GetPlantiosIncompletos()
        {
            var plantios = await GetPlantios();
            var colheitas = await new Colheita().GetAll();
            plantios.RemoveAll(p => colheitas.Any(c => c.IdPlantio == p.Id));
            return plantios;
        }

        private double ProgressoPlantio(Plantio plantio)
        {
            DateTime hoje = DateTime.Today;
            try
            {
                //agora - começo
                int diasAgoracomeco = hoje.Subtract(plantio.DataInicio).Days;

                //fim - começo
                int diasFimcomeco = plantio.DataColheita.Subtract(plantio.DataInicio).Days;

                int progresso = ((100 * diasAgoracomeco) / diasFimcomeco);

                if (progresso > 100)
                    return 100;

                return progresso;
            }
            catch
            {
                return 100;
            }
        }
    }
}