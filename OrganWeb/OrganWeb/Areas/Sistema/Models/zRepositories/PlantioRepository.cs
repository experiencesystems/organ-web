using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganWeb.Models;
using OrganWeb.Areas.Sistema.Models.Safras;
using System.Threading.Tasks;
using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.zBanco;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Estoque;

namespace OrganWeb.Areas.Sistema.Models
{
    public class PlantioRepository : OrganRepository<Plantio>
    {
        private List<VwPlantio> Plantios = new List<VwPlantio>();

        public async Task<List<Plantio>> GetPlantios()
        {
            Plantios = await new VwPlantio().GetAll();
            return Plantios.Select((p) => new Plantio
            {
                Porcentagem = ProgressoPlantio(p),
                Id = p.Id,
                Nome = p.Plantio,
                Sistema = p.Sistema,
                TipoPlantio = p.Tipo,
                DataInicio = p.Inicio,
                DataColheita = p.Colheita,
                NomeAreas = p.Areas
            }).ToList();
        }

        private double ProgressoPlantio(VwPlantio plantio)
        {
            DateTime hoje = DateTime.Today;
            int diasAgoracomeco = 0;
            try
            {
                //agora - começo
                if (hoje > plantio.Inicio)
                    diasAgoracomeco = hoje.Subtract(plantio.Inicio).Days;

                //fim - começo
                int diasFimcomeco = plantio.Colheita.Subtract(plantio.Colheita).Days;

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