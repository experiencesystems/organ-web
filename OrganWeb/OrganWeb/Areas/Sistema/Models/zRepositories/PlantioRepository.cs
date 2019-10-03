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
                        .ToList()
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

/*
 _context.Plantios.AsEnumerable()
    .Select(x => new Plantio
    {
        Porcentagem = x.ProgressoPlantio(x),
        IdSemente = x.IdSemente
    })
    .ToList();

(from plantio in _context.Plantios
             join semente in _context.Sementes
             on plantio.IdSemente equals semente.Id
             select new 
             {
                 Porcentagem = plantio.ProgressoPlantio(plantio),
                 semente.Nome
             }).ToList();

var query = database.Posts    // your starting point - table in the "from" statement
.Join(database.Post_Metas, // the source table of the inner join
post => post.ID,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
meta => meta.Post_ID,   // Select the foreign key (the second part of the "on" clause)
(post, meta) => new { Post = post, Meta = meta }) // selection
.Where(postAndMeta => postAndMeta.Post.ID == id);    // where statement


            //return (from p in _context.Plantios.AsEnumerable()
            //select new PlantioDTO { Porcentagem = p.ProgressoPlantio(p) }).ToList();
 */
