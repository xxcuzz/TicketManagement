using System;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.DataAccess.Context
{
    public class TicketDbContextSeed
    {
        private readonly TicketDbContext _ticketDbContext;

        public TicketDbContextSeed(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task Seed()
        {
            if (!_ticketDbContext.Venue.Any())
            {
                _ticketDbContext.Venue.AddRange(new Venue[]
                {
                    new Venue
                    {
                        Description = "RitzCinema",
                        Address = "33 Travis, st.",
                        Phone = "23-52-23",
                    },
                    new Venue
                    {
                        Description = "MiniCinema",
                        Address = "Torr Top St, New Mills, High Peak SK22 4BS, Great Britain",
                        Phone = "+44 1663 742132",
                    },
                });
                _ticketDbContext.SaveChanges();
            }

            if (!_ticketDbContext.Layout.Any())
            {
                var venueDto = _ticketDbContext.Venue.SingleOrDefault(a => a.Description.Equals("RitzCinema"));
                var venueDto2 = _ticketDbContext.Venue.SingleOrDefault(a => a.Description.Equals("MiniCinema"));
                _ticketDbContext.Layout.AddRange(new Layout[]
                {
                    new Layout
                    {
                        VenueId = venueDto.Id,
                        Description = "Second Layout",
                    },
                    new Layout
                    {
                        VenueId= venueDto2.Id,
                        Description = "MiniLayout",
                    },
                });
                _ticketDbContext.SaveChanges();
            }

            if (!_ticketDbContext.Area.Any())
            {
                var layoutDto2 = _ticketDbContext.Layout.SingleOrDefault(a => a.Description.Equals("Second Layout"));
                var layoutDto3 = _ticketDbContext.Layout.SingleOrDefault(a => a.Description.Equals("MiniLayout"));

                _ticketDbContext.Area.AddRange(new Area[]
                {
                    new Area
                    {
                        Description = "Seat bags",
                        LayoutId = layoutDto2.Id,
                        CoordX = 0,
                        CoordY = 0,
                    },
                    new Area
                    {
                        Description = "Love seats",
                        LayoutId = layoutDto2.Id,
                        CoordX = 1,
                        CoordY = 2,
                    },
                    new Area
                    {
                        Description = "Default seats mini",
                        LayoutId = layoutDto3.Id,
                        CoordX = 0,
                        CoordY = 0,
                    },
                    new Area
                    {
                        Description = "Seat bags mini",
                        LayoutId = layoutDto3.Id,
                        CoordX = 2,
                        CoordY = 2,
                    },
                    new Area
                    {
                        Description = "Love seats mini",
                        LayoutId = layoutDto3.Id,
                        CoordX = 1,
                        CoordY = 4,
                    },
                });
                _ticketDbContext.SaveChanges();
            }

            if (!_ticketDbContext.Seat.Any())
            {
                var areaDto2 = _ticketDbContext.Area.SingleOrDefault(a => a.Description.Equals("Seat bags"));
                var areaDto3 = _ticketDbContext.Area.SingleOrDefault(a => a.Description.Equals("Love seats"));

                var areaDto4 = _ticketDbContext.Area.SingleOrDefault(a => a.Description.Equals("Default seats mini"));
                var areaDto5 = _ticketDbContext.Area.SingleOrDefault(a => a.Description.Equals("Seat bags mini"));
                var areaDto6 = _ticketDbContext.Area.SingleOrDefault(a => a.Description.Equals("Love seats mini"));

                _ticketDbContext.Seat.AddRange(new Seat[]
                {
                    new Seat { AreaId = areaDto2.Id, Row = 1, Number = 1 },
                    new Seat { AreaId = areaDto2.Id, Row = 1, Number = 2 },
                    new Seat { AreaId = areaDto2.Id, Row = 1, Number = 3 },
                    new Seat { AreaId = areaDto2.Id, Row = 1, Number = 4 },
                    new Seat { AreaId = areaDto2.Id, Row = 2, Number = 1 },
                    new Seat { AreaId = areaDto2.Id, Row = 2, Number = 2 },
                    new Seat { AreaId = areaDto2.Id, Row = 2, Number = 3 },

                    new Seat { AreaId = areaDto3.Id, Row = 1, Number = 1 },
                    new Seat { AreaId = areaDto3.Id, Row = 1, Number = 2 },
                    new Seat { AreaId = areaDto3.Id, Row = 1, Number = 3 },
                    new Seat { AreaId = areaDto3.Id, Row = 1, Number = 4 },
                    new Seat { AreaId = areaDto3.Id, Row = 1, Number = 5 },

                    new Seat { AreaId = areaDto4.Id, Row = 1, Number = 1 },
                    new Seat { AreaId = areaDto4.Id, Row = 1, Number = 2 },
                    new Seat { AreaId = areaDto4.Id, Row = 1, Number = 3 },
                    new Seat { AreaId = areaDto4.Id, Row = 1, Number = 4 },
                    new Seat { AreaId = areaDto4.Id, Row = 2, Number = 1 },
                    new Seat { AreaId = areaDto4.Id, Row = 2, Number = 2 },
                    new Seat { AreaId = areaDto4.Id, Row = 2, Number = 3 },
                    new Seat { AreaId = areaDto4.Id, Row = 2, Number = 4 },
                    new Seat { AreaId = areaDto4.Id, Row = 3, Number = 1 },
                    new Seat { AreaId = areaDto4.Id, Row = 4, Number = 1 },

                    new Seat { AreaId = areaDto5.Id, Row = 1, Number = 1 },
                    new Seat { AreaId = areaDto5.Id, Row = 1, Number = 2 },
                    new Seat { AreaId = areaDto5.Id, Row = 2, Number = 1 },
                    new Seat { AreaId = areaDto5.Id, Row = 2, Number = 2 },

                    new Seat { AreaId = areaDto6.Id, Row = 1, Number = 1 },
                    new Seat { AreaId = areaDto6.Id, Row = 1, Number = 2 },
                });
                _ticketDbContext.SaveChanges();
            }

            if (!_ticketDbContext.Event.Any())
            {
                var layoutDto1 = _ticketDbContext.Layout.SingleOrDefault(a => a.Description.Equals("Second Layout"));
                var layoutDto2 = _ticketDbContext.Layout.SingleOrDefault(a => a.Description.Equals("MiniLayout"));

                _ticketDbContext.Event.AddRange(new Event[]
                {
                    new Event
                    {
                        Description = "Joker",
                        LayoutId = layoutDto1.Id,
                        Name = "Cinema",
                        EventStart = DateTime.Now,
                        EventEnd = DateTime.Now.AddHours(2),
                    },
                    new Event
                    {
                        Description = "Inception",
                        LayoutId = layoutDto2.Id,
                        Name = "Cinema",
                        EventStart = DateTime.Now.AddDays(2),
                        EventEnd = DateTime.Now.AddHours(2).AddDays(2),
                    },
                });
                _ticketDbContext.SaveChanges();
            }

            if (!_ticketDbContext.EventArea.Any())
            {
                var event1 = _ticketDbContext.Event.SingleOrDefault(e => e.Description.Equals("Joker"));
                var event2 = _ticketDbContext.Event.SingleOrDefault(e => e.Description.Equals("Inception"));

                _ticketDbContext.EventArea.AddRange(new EventArea[]
                {
                    new EventArea
                    {
                        Description = "Seat bags",
                        Price = 5.5M,
                        CoordX = 0,
                        CoordY = 0,
                        EventId = event1.Id,
                    },
                    new EventArea
                    {
                        Description = "Love seats",
                        Price = 7.0M,
                        CoordX = 1,
                        CoordY = 2,
                        EventId = event1.Id,
                    },

                    new EventArea
                    {
                        Description = "Default seats mini",
                        Price = 3.0M,
                        CoordX = 0,
                        CoordY = 0,
                        EventId = event2.Id,
                    },
                    new EventArea
                    {
                        Description = "Seat bags mini",
                        Price = 5.0M,
                        CoordX = 2,
                        CoordY = 2,
                        EventId = event2.Id,
                    },
                    new EventArea
                    {
                        Description = "Love seats mini",
                        Price = 6.5M,
                        CoordX = 1,
                        CoordY = 4,
                        EventId = event2.Id,
                    },
                });
                _ticketDbContext.SaveChanges();
            }

            if (!_ticketDbContext.EventSeat.Any())
            {
                var areaSeatDto2 = _ticketDbContext.EventArea.SingleOrDefault(a => a.Description.Equals("Seat bags"));
                var areaSeatDto3 = _ticketDbContext.EventArea.SingleOrDefault(a => a.Description.Equals("Love seats"));

                var areaSeatDto4 = _ticketDbContext.EventArea.SingleOrDefault(a => a.Description.Equals("Default seats mini"));
                var areaSeatDto5 = _ticketDbContext.EventArea.SingleOrDefault(a => a.Description.Equals("Seat bags mini"));
                var areaSeatDto6 = _ticketDbContext.EventArea.SingleOrDefault(a => a.Description.Equals("Love seats mini"));

                _ticketDbContext.EventSeat.AddRange(new EventSeat[]
                {
                    new EventSeat { EventAreaId = areaSeatDto2.Id, Row = 1, Number = 1, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto2.Id, Row = 1, Number = 2, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto2.Id, Row = 1, Number = 3, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto2.Id, Row = 1, Number = 4, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto2.Id, Row = 2, Number = 1, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto2.Id, Row = 2, Number = 2, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto2.Id, Row = 2, Number = 3, State = 0 },

                    new EventSeat { EventAreaId = areaSeatDto3.Id, Row = 1, Number = 1, State = 1 },
                    new EventSeat { EventAreaId = areaSeatDto3.Id, Row = 1, Number = 2, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto3.Id, Row = 1, Number = 3, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto3.Id, Row = 1, Number = 4, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto3.Id, Row = 1, Number = 5, State = 0 },

                    new EventSeat { EventAreaId = areaSeatDto4.Id, Row = 1, Number = 1, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto4.Id, Row = 1, Number = 2, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto4.Id, Row = 1, Number = 3, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto4.Id, Row = 1, Number = 4, State = 1 },
                    new EventSeat { EventAreaId = areaSeatDto4.Id, Row = 2, Number = 1, State = 1 },
                    new EventSeat { EventAreaId = areaSeatDto4.Id, Row = 2, Number = 2, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto4.Id, Row = 2, Number = 3, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto4.Id, Row = 2, Number = 4, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto4.Id, Row = 3, Number = 1, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto4.Id, Row = 4, Number = 1, State = 0 },

                    new EventSeat { EventAreaId = areaSeatDto5.Id, Row = 1, Number = 1, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto5.Id, Row = 1, Number = 2, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto5.Id, Row = 2, Number = 1, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto5.Id, Row = 2, Number = 2, State = 0 },

                    new EventSeat { EventAreaId = areaSeatDto6.Id, Row = 1, Number = 1, State = 0 },
                    new EventSeat { EventAreaId = areaSeatDto6.Id, Row = 1, Number = 2, State = 0 },
                });
                _ticketDbContext.SaveChanges();
            }

            await _ticketDbContext.SaveChangesAsync();
        }
    }
}
