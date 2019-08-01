using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models.Semente
{
    public class SementeRepository : ISementeRepository
    {
        private BancoContext _context;

        public SementeRepository(BancoContext sementeContext)
        {
            this._context = sementeContext;
        }

        public void DeleteSemente(int sementeID)
        {
            Semente semente = _context.Sementes.Find(sementeID);
            _context.Sementes.Remove(semente);
        }

        public Semente GetSementeByID(int sementeId)
        {
            return _context.Sementes.Find(sementeId);
        }

        public IEnumerable<Semente> GetSementes()
        {
            return _context.Sementes.ToList();
        }

        public void InsertSemente(Semente semente)
        {
            _context.Sementes.Add(semente);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateSemente(Semente semente)
        {
            _context.Entry(semente).State = EntityState.Modified;
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