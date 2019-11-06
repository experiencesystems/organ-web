using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Models.Banco;
using System.Threading.Tasks;
using OrganWeb.Areas.Sistema.Models.Administrativo;

namespace OrganWeb.Areas.Sistema.Models
{
    public class PlantioRepository : Repository<Plantio>
    {
        public async Task<List<Plantio>> GetPlantios()
        {
            var plantios = await GetAll();
            var areap = await new AreaPlantio().GetAll();
            return plantios.Select((p) => new Plantio
            {
                Porcentagem = ProgressoPlantio(p),
                Id = p.Id,
                Nome = p.Nome,
                Sistema = p.Sistema,
                TipoPlantio = p.TipoPlantio,
                DataInicio = p.DataInicio,
                DataColheita = p.DataColheita,
                NomeAreas = GetAreaPlantios(p, areap)
            }).ToList();
        }

        public async Task<List<Plantio>> GetPlantiosIncompletos()
        {
            var plantios = await GetPlantios();
            var colheitas = await new Colheita().GetAll();
            plantios.RemoveAll(p => colheitas.Any(c => c.IdPlantio == p.Id));
            return plantios;
        }

        public string GetAreaPlantios(Plantio plantio, List<AreaPlantio> areaPlantios)
        {
            List<string> nomes = areaPlantios.Where(x => x.Plantio.Id == plantio.Id).Select(a => a.Area.Nome).ToList();
            return string.Join(", ", nomes.Select(x => x.ToString()).ToArray());
        }

        private double ProgressoPlantio(Plantio plantio)
        {
            DateTime hoje = DateTime.Today;
            int diasAgoracomeco = 0;
            try
            {
                //agora - começo
                if (hoje > plantio.DataInicio)
                    diasAgoracomeco = hoje.Subtract(plantio.DataInicio).Days;

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