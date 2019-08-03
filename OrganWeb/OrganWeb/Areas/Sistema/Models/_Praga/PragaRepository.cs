using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models._Praga
{
    public class PragaRepository : IRepository<Praga>,IDisposable
    {
        private BancoContext _context;

        public void Delete(int Id)
        {
            Praga praga = _context.Pragas.Find(Id);
            _context.Pragas.Remove(praga);
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

        public IEnumerable<Praga> GetAllItems()
        {
            return _context.Pragas.ToList();
        }

        public Praga GetByID(int? Id)
        {
            return _context.Pragas.Find(Id);
        }

        public void Insert(Praga t)
        {
            _context.Pragas.Add(t);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Praga t)
        {
            _context.Entry(t).State = EntityState.Modified;
        }
    }
}