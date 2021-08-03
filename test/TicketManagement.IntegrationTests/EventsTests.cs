using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using TicketManagement.DataAccess.AdoRepositories;
using TicketManagement.DataAccess.DataBase;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.IntegrationTests
{
    public class EventsTests
    {
        private static string _connectionString = Configurator.GetTestConnString();
        private EventRepository _eventRepo;
        private VenueRepository _venueRepo;
        private AreaRepository _areaRepo;
        private LayoutRepository _layoutRepo;
        private SeatRepository _seatRepo;
        private EventAreaRepository _eventAreaRepo;
        private EventSeatRepository _eventSeatRepo;

        [SetUp]
        public void Setup()
        {
            _eventRepo = new EventRepository(_connectionString);
            _venueRepo = new VenueRepository(_connectionString);
            _areaRepo = new AreaRepository(_connectionString);
            _layoutRepo = new LayoutRepository(_connectionString);
            _seatRepo = new SeatRepository(_connectionString);
            _eventAreaRepo = new EventAreaRepository(_connectionString);
            _eventSeatRepo = new EventSeatRepository(_connectionString);
            ClearDb();
            InitializeFields().Wait();
        }

        [TearDown]
        public void TearDown()
        {
            ClearDb();
        }

        private static void ReseedTable(string tableName)
        {
            var createCommand = $"DBCC CHECKIDENT (@table, RESEED, 0)";
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(createCommand, connection);
            command.Parameters.AddWithValue("@table", tableName);
            command.ExecuteNonQuery();
        }

        private void ClearDb()
        {
            var seats = _seatRepo.GetAll();
            foreach (var seat in seats)
            {
                _ = _seatRepo.DeleteAsync(seat).Result;
            }

            ReseedTable("Seat");

            var areas = _areaRepo.GetAll();
            foreach (var area in areas)
            {
                _ = _areaRepo.DeleteAsync(area).Result;
            }

            ReseedTable("Area");

            var eventSeats = _eventSeatRepo.GetAll();
            foreach (var eventSeat in eventSeats)
            {
                _ = _eventSeatRepo.DeleteAsync(eventSeat).Result;
            }

            ReseedTable("EventSeat");

            var eventAreas = _eventAreaRepo.GetAll();
            foreach (var eventArea in eventAreas)
            {
                _ = _eventAreaRepo.DeleteAsync(eventArea).Result;
            }

            ReseedTable("EventArea");

            var events = _eventRepo.GetAll();
            foreach (var event1 in events)
            {
                _ = _eventRepo.DeleteAsync(event1).Result;
            }

            ReseedTable("Event");

            var layouts = _layoutRepo.GetAll();
            foreach (var layout in layouts)
            {
                _ = _layoutRepo.DeleteAsync(layout).Result;
            }

            ReseedTable("Layout");

            var venues = _venueRepo.GetAll();
            foreach (var venue in venues)
            {
                _ = _venueRepo.DeleteAsync(venue).Result;
            }

            ReseedTable("Venue");
        }

        private async Task InitializeFields()
        {
            await _venueRepo.AddAsync(new Venue
            {
                Description = "RitzCinemaIntegrationTest",
                Address = "33 Travis, st.",
                Phone = "23-52-23",
            });
            var venue1 = _venueRepo.GetAll().SingleOrDefault(v => v.Description == "RitzCinemaIntegrationTest");

            await _layoutRepo.AddAsync(new Layout
            {
                VenueId = venue1.Id,
                Description = "Standart Layout",
            });

            var layout1 = _layoutRepo.GetAll().SingleOrDefault(l => l.Description == "Standart Layout");
            await _areaRepo.AddAsync(new Area
            {
                Description = "Standart area",
                LayoutId = layout1.Id,
                CoordX = 0,
                CoordY = 0,
            });

            var area1 = _areaRepo.GetAll().SingleOrDefault(a => a.Description == "Standart area" && a.LayoutId == layout1.Id);
            await _seatRepo.AddAsync(new Seat
            {
                AreaId = area1.Id,
                Row = 1,
                Number = 1,
            });

            await _areaRepo.AddAsync(new Area
            {
                Description = "Empty area",
                LayoutId = layout1.Id,
                CoordX = 0,
                CoordY = 0,
            });

            await _eventRepo.AddAsync(new Event
            {
                Description = "Joker",
                LayoutId = layout1.Id,
                Name = "Cinema",
                EventStart = DateTime.Now,
                EventEnd = DateTime.Now.AddHours(2),
                Image = "noImage",
            });

            await _layoutRepo.AddAsync(new Layout
            {
                VenueId = venue1.Id,
                Description = "Covid Layout",
            });

            var layout2 = _layoutRepo.GetAll().SingleOrDefault(l => l.Description == "Covid Layout");
            await _areaRepo.AddAsync(new Area
            {
                Description = "Mems area",
                LayoutId = layout2.Id,
                CoordX = 0,
                CoordY = 0,
            });

            var area3 = _areaRepo.GetAll().SingleOrDefault(a => a.Description == "Mems area" && a.LayoutId == layout2.Id);
            await _seatRepo.AddAsync(new Seat { AreaId = area3.Id, Row = 2, Number = 1 });
            await _seatRepo.AddAsync(new Seat { AreaId = area3.Id, Row = 2, Number = 2 });
            await _seatRepo.AddAsync(new Seat { AreaId = area3.Id, Row = 2, Number = 3 });
            await _seatRepo.AddAsync(new Seat { AreaId = area3.Id, Row = 2, Number = 4 });
            await _seatRepo.AddAsync(new Seat { AreaId = area3.Id, Row = 2, Number = 5 });
            await _seatRepo.AddAsync(new Seat { AreaId = area3.Id, Row = 2, Number = 6 });
            await _seatRepo.AddAsync(new Seat { AreaId = area3.Id, Row = 2, Number = 7 });

            await _areaRepo.AddAsync(new Area
            {
                Description = "LoveSeat area",
                LayoutId = layout2.Id,
                CoordX = 2,
                CoordY = 2,
            });

            var area4 = _areaRepo.GetAll().SingleOrDefault(a => a.Description == "LoveSeat area" && a.LayoutId == layout2.Id);
            await _seatRepo.AddAsync(new Seat { AreaId = area4.Id, Row = 3, Number = 1 });
            await _seatRepo.AddAsync(new Seat { AreaId = area4.Id, Row = 3, Number = 2 });
            await _seatRepo.AddAsync(new Seat { AreaId = area4.Id, Row = 3, Number = 3 });
            await _seatRepo.AddAsync(new Seat { AreaId = area4.Id, Row = 3, Number = 4 });
            await _seatRepo.AddAsync(new Seat { AreaId = area4.Id, Row = 3, Number = 5 });

            await _eventRepo.AddAsync(new Event
            {
                Description = "Inception",
                LayoutId = layout2.Id,
                Name = "Cinema",
                EventStart = DateTime.Now.AddDays(2),
                EventEnd = DateTime.Now.AddDays(2).AddHours(3),
                Image = "noImage",
            });

            await _layoutRepo.AddAsync(new Layout
            {
                VenueId = venue1.Id,
                Description = "Empty Layout",
            });

            var event1 = new Event
            {
                Id = 3,
                LayoutId = 1,
                Description = "Joker",
                Name = "Cinema",
                EventStart = new DateTime(2019, 05, 09, 09, 15, 00),
                EventEnd = new DateTime(2019, 05, 09, 11, 15, 00),
                Image = "no image",
            };

            await _eventRepo.AddAsync(event1);
            await _eventRepo.DeleteAsync(event1);

            await _eventRepo.AddAsync(new Event
            {
                Description = "NoUpdated",
                LayoutId = layout2.Id,
                Name = "Cinema",
                EventStart = DateTime.Now.AddDays(12),
                EventEnd = DateTime.Now.AddDays(12).AddHours(3),
                Image = "noImage",
            });

            var event2 = new Event
            {
                Id = 4,
                Description = "Updated",
                LayoutId = layout1.Id,
                Name = "Cinema",
                EventStart = DateTime.Now.AddDays(12),
                EventEnd = DateTime.Now.AddDays(12).AddHours(3),
                Image = "noImage",
            };

            await _eventRepo.UpdateAsync(event2);
        }

        [Test]
        public void UpdateEvent_ShouldReturn_UpdatedEventDescription()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand($"SELECT Description FROM Event WHERE Id = 4", connection);
            var eventDescriptions = new List<string>();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                eventDescriptions.Add(Convert.ToString(reader["Description"]));
            }

            Assert.AreEqual("Updated", eventDescriptions[0]);
        }

        [Test]
        public void UpdateEvent_ShouldReturn_RightCountOfEventSeats()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand($"SELECT Count(Id) FROM EventSeat WHERE EventAreaId = 4", connection);
            var result = (int)command.ExecuteScalar();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void DeleteEvent_ShouldReturn_True()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand($"SELECT Count(Id) FROM Event WHERE Id = 3", connection);

            int eventExist = (int)command.ExecuteScalar();

            Assert.AreEqual(0, eventExist);
        }

        [Test]
        public void AddEvent_ShouldReturn_RightCountOfEventAreas()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand($"SELECT Count(Id) FROM EventArea WHERE EventId = 1", connection);

            var result = (int)command.ExecuteScalar();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void AddEvent_ShouldReturn_RightCountOfEventSeat()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand($"SELECT Id, EventAreaId, Row, Number, State FROM EventSeat WHERE EventAreaId = 1 OR EventAreaId = 2 OR EventAreaId = 3 OR EventAreaId = 4", connection);

            var reader = command.ExecuteReader();
            var eventSeats = new List<EventSeat>();
            var counter = 0;
            while (reader.Read())
            {
                counter++;
                eventSeats.Add(new EventSeat
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    EventAreaId = Convert.ToInt32(reader["EventAreaId"]),
                    Row = Convert.ToInt32(reader["Row"]),
                    Number = Convert.ToInt32(reader["Number"]),
                    State = Convert.ToInt32(reader["State"]),
                });
            }

            Assert.AreEqual(13, counter);
        }

        [Test]
        public void AddEvent_ShouldReturn_RightEventAreaIdInEventSeats()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand($"SELECT Id, EventAreaId, Row, Number, State FROM EventSeat", connection);

            var reader = command.ExecuteReader();
            var eventSeats = new List<EventSeat>();
            var counter = 0;
            while (reader.Read())
            {
                counter++;
                eventSeats.Add(new EventSeat
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    EventAreaId = Convert.ToInt32(reader["EventAreaId"]),
                    Row = Convert.ToInt32(reader["Row"]),
                    Number = Convert.ToInt32(reader["Number"]),
                    State = Convert.ToInt32(reader["State"]),
                });
            }

            Assert.AreEqual(3, eventSeats[1].EventAreaId);
        }
    }
}