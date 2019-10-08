using OrganWeb.Areas.Sistema.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace OrganWeb.Models.Banco
{
    public class Repository<T> : IDisposable where T : class
    {
        public BancoContext _context;

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

        public List<T> GetFew()
        {
            return DbSet.Take(10).ToList();
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
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + ": " + x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
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