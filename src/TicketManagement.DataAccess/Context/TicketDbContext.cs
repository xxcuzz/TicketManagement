using Microsoft.EntityFrameworkCore;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.Context
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options)
               : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Venue> Venue { get; set; }

        public DbSet<Layout> Layout { get; set; }

        public DbSet<Event> Event { get; set; }

        public DbSet<Area> Area { get; set; }

        public DbSet<Seat> Seat { get; set; }

        public DbSet<EventArea> EventArea { get; set; }

        public DbSet<EventSeat> EventSeat { get; set; }

        public DbSet<UserTicket> UserTicket { get; set; }
    }
}
