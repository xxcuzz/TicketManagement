using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketManagement.DataAccess.Context;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.InterfacesRepository;

namespace TicketManagement.DataAccess.EFRepositories
{
    public class EventEfRepository : IRepository<Event>
    {
        private readonly TicketDbContext _db;

        public EventEfRepository(TicketDbContext context)
        {
            _db = context;
        }

        public async Task<bool> AddAsync(Event item)
        {
            try
            {
                await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.AddEvent {item.Name}, {item.Description}, {item.LayoutId}, {item.EventStart}, {item.EventEnd}, {item.Image}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Event item)
        {
            try
            {
                await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.RemoveEvent {item.Id}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IQueryable<Event> GetAll()
        {
            return _db.Event.AsNoTracking().AsQueryable();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _db.Event.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Event item)
        {
            try
            {
                await _db.Database.ExecuteSqlInterpolatedAsync($"EXEC dbo.UpdateEvent {item.Id}, {item.Name}, {item.Description}, {item.LayoutId}, {item.EventStart}, {item.EventEnd}, {item.Image}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
