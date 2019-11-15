using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace OrganWeb.Areas.Ecommerce.Models.zRepositories
{
    public class AnuncioRepository : EcommerceRepository<Anuncio>
    {
        private Avaliacao Avaliacao = new Avaliacao();
        private List<Avaliacao> Avaliacoes = new List<Avaliacao>();
        private List<Anuncio> Anuncios = new List<Anuncio>();
        private List<int> Notas = new List<int>();

        public async Task<int> GetMediaEstrelas(Anuncio anuncio)
        {
            Notas = await GetNotas(anuncio);
            if (Notas.Count == 0)
                return 0;
            int estrelas = 0;
            foreach (var item in Notas)
            {
                estrelas += item;
            }
            return estrelas / Notas.Count;
        }

        public async Task<List<int>> GetNotas(Anuncio anuncio)
        {
            Avaliacoes = await Avaliacao.GetAvaliacoes(anuncio);
            if (Avaliacoes.Count != 0)
            {
                return Avaliacoes.AsQueryable().Select(x => x.Nota).ToList();
            }
            return new List<int>();
        }

        public async Task<List<Anuncio>> GetAnuncios()
        {
            var listafinal = new List<Anuncio>();
            Anuncios = await GetAll();
            foreach (var anuncio in Anuncios)
            {
                anuncio.Estrelas = await GetMediaEstrelas(anuncio);
                listafinal.Add(anuncio);
            }
            return listafinal;
        }
    }
}