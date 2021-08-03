using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.DataAccess.Context;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.DataAccess.EFRepositories
{
    public class EfRepository<T> : IRepository<T>, IDisposable
        where T : class
    {
        private readonly TicketDbContext _db;

        public EfRepository(TicketDbContext context)
        {
            _db = context;
        }

        public async Task<bool> AddAsync(T item)
        {
            try
            {
                _db.Set<T>().Add(item);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T item)
        {
            try
            {
                _db.Set<T>().Update(item);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(T item)
        {
            try
            {
                _db.Set<T>().Remove(item);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<T> GetAll()
        {
            return _db.Set<T>().AsQueryable().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db?.Dispose();
            }
        }
    }
}
