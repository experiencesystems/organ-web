using OrganWeb.Areas.Sistema.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrganWeb.Models
{
    public class Repository<T> : IDisposable where T : class
    {
        private BancoContext _context;

        protected DbSet<T> DbSet { get; set; }

        public Repository()
        {
            _context = new BancoContext();
            DbSet = _context.Set<T>();
        }

        public Repository(BancoContext context)
        {
            this._context = context;
        }

        public void Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T GetByID(int? id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
        }

        public List<Plantio> GetPlantios()
        {
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

             */

            var select = _context.Plantios
                        .ToList()
                        .Select(p => new Plantio {
                            Porcentagem = p.ProgressoPlantio(p),
                            Semente = new Semente
                            {
                                Nome = p.Semente.Nome
                            }
                        })
                        .ToList();
            return select;

            //return (from p in _context.Plantios.AsEnumerable()
            //select new PlantioDTO { Porcentagem = p.ProgressoPlantio(p) }).ToList();
        }

        public double ProgressoPlantio(Plantio plantio)
        {
            DateTime hoje = DateTime.Today;

            //agora - começo
            TimeSpan agoracomeco = (hoje.Subtract(plantio.DataInício));
            int diasAgoracomeco = agoracomeco.Days;

            //fim - começo
            TimeSpan fimcomeco = (plantio.DataColheita.Subtract(plantio.DataInício));
            int diasFimcomeco = fimcomeco.Days;

            int progresso = ((100 * diasAgoracomeco) / diasFimcomeco);

            if (progresso>100) {
                return 100;
            }
            else
            {
                return progresso;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}