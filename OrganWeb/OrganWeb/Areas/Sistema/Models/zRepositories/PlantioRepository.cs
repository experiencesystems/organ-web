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
            try
            {
                Plantios = await new VwPlantio().GetAll();
            }
            catch (Exception e)
            {

            }
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

        public async Task<List<Plantio>> GetPlantiosAtivos()
        {
            Plantios = await new VwPlantio().GetAtivos();
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

        public async Task<List<Plantio>> GetPlantiosFinalizados()
        {
            Plantios = await new VwPlantio().GetFinalizados();
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
                int diasFimcomeco = plantio.Colheita.Subtract(plantio.Inicio).Days;

                int progresso = (100 * diasAgoracomeco / diasFimcomeco);

                if (progresso > 100 || progresso < 0)
                    return 100;

                return progresso;
            }
            catch (Exception e)
            {
                return 100;
            }
        }
    }
}