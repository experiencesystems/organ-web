using MySql.Data.MySqlClient;
using OrganWeb.Areas.Ecommerce.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrganWeb.Areas.Ecommerce.Models.zBanco
{
    public class EcommerceRepository<T> : IDisposable where T : class
    {
        protected readonly EcommerceContext _context;
        protected DbSet<T> DbSet { get; set; }

        public EcommerceRepository()
        {
            _context = new EcommerceContext();
            DbSet = _context.Set<T>();
        }

        public EcommerceRepository(EcommerceContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            if (_context.Entry(DbSet.Find(id)).State == EntityState.Detached)
            {
                DbSet.Attach(DbSet.Find(id));
            }
            DbSet.Remove(DbSet.Find(id));
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                return await DbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetByID(int? id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public async Task Save()
        {
            try
            {
                _context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + ": " + x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
            catch (Exception ex)
            {
                UpdateException updateException = (UpdateException)ex.InnerException;
                MySqlException sqlException = (MySqlException)updateException.InnerException;
            }
        }

        public void SaveSync()
        {
            try
            {
                _context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch(Exception exx)
            {

            }
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void AttachToContext(string id)
        {
            var usuario = _context.Users.Local.SingleOrDefault(x => x.Id == id);
            if (usuario == null)
            {
                usuario = new ApplicationUser { Id = id };
                _context.Users.Attach(usuario);
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