using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Models._Semente
{
    public class SementeRepository : IRepository<Semente>, IDisposable
    {
        private BancoContext _context;

        public SementeRepository(BancoContext sementeContext)
        {
            this._context = sementeContext;
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
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

        public IEnumerable<Semente> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public Semente GetByID(int? Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Semente t)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Semente t)
        {
            throw new NotImplementedException();
        }
    }
}