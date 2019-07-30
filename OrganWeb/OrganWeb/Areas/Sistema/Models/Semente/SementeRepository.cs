using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrganWeb.Areas.Sistema.Models.Semente
{
    public class SementeRepository : ISementeRepository
    {
        private dborganwebEntities _context;

        public SementeRepository(dborganwebEntities sementeContext)
        {
            this._context = sementeContext;
        }

        public void DeleteSemente(int sementeID)
        {
            tbsemente semente = _context.Sementes.Find(sementeID);
            _context.Sementes.Remove(semente);
        }

        public tbsemente GetSementeByID(int sementeId)
        {
            return _context.Sementes.Find(sementeId);
        }

        public IEnumerable<tbsemente> GetSementes()
        {
            return _context.Sementes.ToList();
        }

        public void InsertSemente(tbsemente semente)
        {
            _context.Sementes.Add(semente);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateSemente(tbsemente semente)
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