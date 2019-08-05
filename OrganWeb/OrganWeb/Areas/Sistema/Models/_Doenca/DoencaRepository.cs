using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models._Doenca
{
    public class DoencaRepository : IRepository<Doenca>, IDisposable
    {
        private BancoContext _context;

        public DoencaRepository(BancoContext sementeContext)
        {
            this._context = sementeContext;
        }

        public void Delete(int Id)
        {
            Doenca doenca = _context.Doencas.Find(Id);
            _context.Doencas.Remove(doenca);
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

        public IEnumerable<Doenca> GetAllItems()
        {
            return _context.Doencas.ToList();
        }

        public Doenca GetByID(int? Id)
        {
            return _context.Doencas.Find(Id);
        }

        public void Insert(Doenca t)
        {
            _context.Doencas.Add(t);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Doenca t)
        {
            _context.Entry(t).State = EntityState.Modified;
        }
    }
}