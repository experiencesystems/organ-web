using OrganWeb.Areas.Ecommerce.Models.Vendas;
using OrganWeb.Areas.Ecommerce.Models.zBanco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PagedList;
using PagedList.EntityFramework;
using System.Web;
using Microsoft.AspNet.Identity;

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

        public async Task<Anuncio> GetAnuncio(int? id)
        {
            var anuncio = await GetByID(id);
            Avaliacoes = await Avaliacao.GetAvaliacoes(anuncio);
            anuncio.NumAvaliacoes = Avaliacoes.Count;
            anuncio.Estrelas = await GetMediaEstrelas(anuncio);
            return anuncio;
        }

        public IPagedList<Anuncio> GetAnunciosRecentes(int page, List<Anuncio> anuncios)
        {
            return anuncios.OrderByDescending(p => p.Data).Take(40).ToPagedList(page, 12);
        }

        public IPagedList<Anuncio> GetAnunciosEmPromocao(int page, List<Anuncio> anuncios)
        {
            return anuncios.Where(x => x.Desconto > 0).Take(40).OrderByDescending(p => p.DataDesc).ToPagedList(page, 12);
        }

        // Filtros

        public IPagedList<Anuncio> GetAnunciosCategoria(int page, int categoria, List<Anuncio> anuncios)
        {
            return anuncios.Where(x => x.Produto.Categoria == categoria).Take(40).OrderByDescending(p => p.DataDesc).ToPagedList(page, 20);
        }

        public IPagedList<Anuncio> GetAnunciosCategoriaFiltro(int page, int categoria, string filtro, List<Anuncio> anuncios)
        {
            return anuncios.Where(x => x.Produto.Categoria == categoria && (x.Nome.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0)).Take(40).OrderByDescending(p => p.DataDesc).ToPagedList(page, 20);
        }

        public IPagedList<Anuncio> GetAnunciosFiltro(int page, string filtro, List<Anuncio> anuncios)
        {
            return anuncios.Where(x => x.Nome.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0).Take(40).OrderByDescending(p => p.DataDesc).ToPagedList(page, 20);
        }

        public IPagedList<Anuncio> GetMeusAnuncios(int page, List<Anuncio> anuncios)
        {
            string id = HttpContext.Current.User.Identity.GetUserId();
            return anuncios.Where(x => x.IdAnunciante == id).OrderBy(p => p.Id).ToPagedList(page, 20);
        }
    }
}